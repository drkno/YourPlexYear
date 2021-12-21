using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourPlexYear.Model;
using YourPlexYear.Model.Tautulli;
using YourPlexYear.Model.Tautulli.Sql;
using YourPlexYear.Service.Config;
using YourPlexYear.Service.Tautulli.Service;

namespace YourPlexYear.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatsController : CommonAuthController
    {
        private static readonly MediaItem Placeholder = new()
        {
            Duration = 0,
            Episodes = 0,
            FinishedPercent = 0,
            Id = 0,
            PausedDuration = 0,
            Plays = 0,
            Thumbnail = "None",
            Title = "None :(",
            Year = 1970
        };
        
        private readonly ITautulliService _tautulliService;
        private readonly IConfigurationService _configuration;
        private readonly HttpClient _httpClient;

        public StatsController(ITautulliService tautulliService,
                               IConfigurationService configuration,
                               IHttpClientFactory clientFactory)
        {
            _tautulliService = tautulliService;
            _configuration = configuration;
            _httpClient = clientFactory.CreateClient();
        }

        [HttpGet("thumbProxy")]
        public async Task<IActionResult> ProxyThumbnail(long id)
        {
            var thumbnailImage = await _tautulliService.GetThumbnailImage(id);
            return File(thumbnailImage, "image/jpeg");
        }

        [HttpGet("{year}")]
        public async Task<StatsResponse> GetStats(ushort year)
        {
            var username = Identity.Username;
            var tautulliUser = await _tautulliService.GetUserByEmail(Identity.Email.Value);
            if (username.Value == "fixme")
            {
                username = new Username(tautulliUser.Username);
            }

            var browserUsage = CalculateBrowserUsage(year, tautulliUser);
            var popularMedia = CalculatePopular(year);
            var tvShows = CalculateTvShows(year, tautulliUser, popularMedia);
            var movies = CalculateMovies(year, tautulliUser, popularMedia);
            var watchDays = _tautulliService.GetViewingDay(tautulliUser.UserId, year);
            
            return new StatsResponse()
            {
                Username = username.Value,
                Tautulli = _configuration.GetTautulliUrl(),
                Ombi = _configuration.GetOmbiUrl(),
                Tv = (await tvShows),
                Movies = (await movies),
                GlobalBrowsers = (await browserUsage).globalBrowsers,
                YourBrowsers = (await browserUsage).yourBrowsers,
                WatchDays = (await watchDays)
            };
        }

        private async Task<MediaLibrary> CalculateMovies(ushort year, TautulliUser user, Task<MostPopular> popularMedia)
        {
            var movies = await _tautulliService.GetMovieWatchHistory(user.UserId, year);

            FixPlexThumbnailUrls(movies);

            if (movies.Count == 0)
            {
                movies.Add(Placeholder);
            }

            return new MediaLibrary
            {
                Total = movies.Count,
                Top10 = movies.GetRange(0, Math.Min(10, movies.Count)),
                TotalWatchTime = movies.Select(movie => movie.Duration).Sum(),
                Oldest = movies.Aggregate(movies[0], (acc, curr) => acc.Year < curr.Year ? acc : curr),
                FinishedPercent = movies.Sum(movie => movie.FinishedPercent) / movies.Count,
                MostPaused = movies.Aggregate(movies[0],
                    (acc, curr) => acc.PausedDuration > curr.PausedDuration ? acc : curr),
                Popular = (await popularMedia).Movie
            };
        }

        private async Task<MediaLibrary> CalculateTvShows(ushort year, TautulliUser user, Task<MostPopular> popularMedia)
        {
            var shows = await _tautulliService.GetTvWatchHistory(user.UserId, year);
            FixPlexThumbnailUrls(shows);

            var tvBuddy = CalculateTvBuddy(shows, user, year);

            if (shows.Count == 0)
            {
                shows.Add(Placeholder);
            }

            return new MediaLibrary
            {
                Total = shows.Count,
                Top10 = shows.GetRange(0, Math.Min(10, shows.Count)),
                TotalWatchTime = shows.Select(tv => tv.Duration).Sum(),
                Oldest = shows.Aggregate(shows[0], (acc, curr) => acc.Year < curr.Year ? acc : curr),
                FinishedPercent = shows.Sum(tv => tv.FinishedPercent) / shows.Count,
                MostPaused = shows.Aggregate(shows[0],
                    (acc, curr) => acc.PausedDuration > curr.PausedDuration ? acc : curr),
                TotalEpisodes = shows.Select(tv => tv.Episodes).Sum(),
                TopBuddy = (await tvBuddy)?.BuddyUsername,
                Popular = (await popularMedia).TvShow
            };
        }

        private Task<TautulliBuddy> CalculateTvBuddy(List<MediaItem> watchHistory, TautulliUser user, ushort year)
        {
            if (watchHistory.Count == 0)
            {
                return Task.FromResult<TautulliBuddy>(null);
            }
            return _tautulliService.GetTvBuddy(watchHistory[0].Title, user.UserId, year);
        }

        private async Task<MostPopular> CalculatePopular(ushort year)
        {
            var popularMedia = await _tautulliService.GetMostPopularMedia(year);
            FixPlexThumbnailUrls(popularMedia.Movie);
            FixPlexThumbnailUrls(popularMedia.TvShow);
            return popularMedia;
        }

        private async Task<(List<Browser> globalBrowsers, List<Browser> yourBrowsers)> CalculateBrowserUsage(ushort year, TautulliUser user)
        {
            var browserUsage = await _tautulliService.GetBrowserUsage(year);
            var globalBrowsersDict = new Dictionary<string, long>();
            var userBrowsersDict = new Dictionary<string, long>();
            foreach (var browser in browserUsage)
            {
                globalBrowsersDict[browser.Platform] =
                    globalBrowsersDict.GetValueOrDefault(browser.Platform, 0) + browser.Count;
                if (user.UserId == browser.UserId)
                {
                    userBrowsersDict[browser.Platform] =
                        userBrowsersDict.GetValueOrDefault(browser.Platform, 0) + browser.Count;
                }
            }

            var globalBrowsers = globalBrowsersDict.Select(entry => new Browser { Name = entry.Key, Value = entry.Value }).ToList();
            globalBrowsers.Sort((a, b) => (int) (b.Value - a.Value));

            var yourBrowsers = userBrowsersDict.Select(entry => new Browser {Name = entry.Key, Value = entry.Value}).ToList();
            yourBrowsers.Sort((a, b) => (int)(b.Value - a.Value));

            return (globalBrowsers, yourBrowsers);
        }

        private void FixPlexThumbnailUrls(IEnumerable<MediaItem> mediaItems)
        {
            foreach (var mediaItem in mediaItems)
            {
                FixPlexThumbnailUrls(mediaItem);
            }
        }

        private void FixPlexThumbnailUrls(MediaItem mediaItem)
        {
            if (mediaItem != null)
            {
                mediaItem.Thumbnail = "/api/v1/stats/thumbProxy?id=" + mediaItem.Id;
            }
        }
    }
}
