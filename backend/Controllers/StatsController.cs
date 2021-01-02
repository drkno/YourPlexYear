using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Your2020.Model;
using Your2020.Model.Tautulli.Sql;
using Your2020.Service.Config;
using Your2020.Service.PlexClient;
using Your2020.Service.TautulliClient;

namespace Your2020.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StatsController : CommonAuthController
    {
        private readonly ITautulliClient _tautulliClient;
        private readonly IConfigurationService _configuration;
        private readonly HttpClient _httpClient;

        public StatsController(ILogger<StatsController> logger,
                               IPlexClient plexClient,
                               ITautulliClient tautulliClient,
                               IConfigurationService configuration,
                               IHttpClientFactory clientFactory)
        {
            _tautulliClient = tautulliClient;
            _configuration = configuration;
            _httpClient = clientFactory.CreateClient();
        }

        [HttpGet("thumbProxy")]
        public async Task<IActionResult> ProxyThumbnail(long id)
        {
            var thumbnailResponse = await _tautulliClient.ExecuteSqlQuery<ThumbnailResponse>(string.Format(Query.Thumbnail, id));
            var url = _configuration.GetPlexUrl() + thumbnailResponse[0].Thumbnail + "?X-Plex-Token=" + Identity.AccessToken.Value;

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return File(response.Content.ReadAsStream(), "image/jpeg");
        }


        [HttpGet("2020")]
        public async Task<StatsResponse> GetStats()
        {
            var identity = Identity;
            var username = identity.Username;

            var userResponse = await _tautulliClient.ExecuteSqlQuery<UserResponse>(string.Format(Query.UserByEmailQuery, identity.Email.Value));

            var browserResponse = await _tautulliClient.ExecuteSqlQuery<BrowserResponse>(Query.BrowserUsageQuery);
            var globalBrowsersDict = new Dictionary<string, long>();
            var userBrowsersDict = new Dictionary<string, long>();
            foreach (var browser in browserResponse)
            {
                globalBrowsersDict[browser.Platform] =
                    globalBrowsersDict.GetValueOrDefault(browser.Platform, 0) + browser.Count;
                if (userResponse[0].UserId == browser.UserId)
                {
                    userBrowsersDict[browser.Platform] =
                        userBrowsersDict.GetValueOrDefault(browser.Platform, 0) + browser.Count;
                }
            }

            var globalBrowsers = globalBrowsersDict.Select(entry => new Browser { Name = entry.Key, Value = entry.Value }).ToList();
            globalBrowsers.Sort((a, b) => (int) (b.Value - a.Value));

            var yourBrowsers = userBrowsersDict.Select(entry => new Browser {Name = entry.Key, Value = entry.Value}).ToList();
            yourBrowsers.Sort((a, b) => (int)(b.Value - a.Value));

            var tvResponse =
                await _tautulliClient.ExecuteSqlQuery<MediaItem>(string.Format(Query.TvWatchHistoryQuery, userResponse[0].UserId));

            var moviesResponse =
                await _tautulliClient.ExecuteSqlQuery<MediaItem>(string.Format(Query.MoviesWatchHistoryQuery, userResponse[0].UserId));

            var popularResponse =
                await _tautulliClient.ExecuteSqlQuery<MediaItem>(Query.MostPopular);

            FixPlexThumbnailUrls(tvResponse);
            FixPlexThumbnailUrls(moviesResponse);
            FixPlexThumbnailUrls(popularResponse);

            var tvBuddy = await _tautulliClient.ExecuteSqlQuery<BuddyResponse>(string.Format(Query.TvBuddy, tvResponse[0].Title, userResponse[0].UserId));

            return new StatsResponse()
            {
                Username = username.Value,
                Tautulli = _configuration.GetTautulliUrl(),
                Ombi = _configuration.GetOmbiUrl(),
                Tv = new MediaLibrary
                {
                    Total = tvResponse.Count,
                    Top10 = tvResponse.GetRange(0, Math.Min(10, tvResponse.Count)),
                    TotalWatchTime = tvResponse.Select(tv => tv.Duration).Sum(),
                    Oldest = tvResponse.Aggregate(tvResponse[0], (acc, curr) => acc.Year < curr.Year ? acc : curr),
                    FinishedPercent = tvResponse.Sum(tv => tv.FinishedPercent) / tvResponse.Count,
                    MostPaused = tvResponse.Aggregate(tvResponse[0], (acc, curr) => acc.PausedDuration > curr.PausedDuration ? acc : curr),
                    Popular = popularResponse[1],
                    TopBuddy = tvBuddy.Count > 0 ? tvBuddy[0].Buddy : null,
                    TotalEpisodes = tvResponse.Select(tv => tv.Episodes).Sum()
                },
                Movies = new MediaLibrary
                {
                    Total = moviesResponse.Count,
                    Top10 = moviesResponse.GetRange(0, Math.Min(10, moviesResponse.Count)),
                    TotalWatchTime = moviesResponse.Select(movie => movie.Duration).Sum(),
                    Oldest = moviesResponse.Aggregate(moviesResponse[0], (acc, curr) => acc.Year < curr.Year ? acc : curr),
                    FinishedPercent = moviesResponse.Sum(movie => movie.FinishedPercent) / tvResponse.Count,
                    MostPaused = moviesResponse.Aggregate(moviesResponse[0], (acc, curr) => acc.PausedDuration > curr.PausedDuration ? acc : curr),
                    Popular = popularResponse[0]
                },
                GlobalBrowsers = globalBrowsers,
                YourBrowsers = yourBrowsers
            };
        }

        class UserResponse
        {
            public string Username { get; set; }
            public string Email { get; set; }
            [JsonPropertyName("user_id")]
            public long UserId { get; set; }
        }

        class BrowserResponse
        {
            [JsonPropertyName("user_id")]
            public long UserId { get; set; }
            public string Platform { get; set; }
            public long Count { get; set; }
        }

        class SessionHistoryResponse
        {
            [JsonPropertyName("user_id")]
            public long UserId { get; set; }
            public long Started { get; set; }
            public long Stopped { get; set; }

        }

        class BuddyResponse
        {
            public string Buddy { get; set; }
        }

        class ThumbnailResponse
        {
            public string Thumbnail { get; set; }
        }

        private void FixPlexThumbnailUrls(IEnumerable<MediaItem> mediaItems)
        {
            foreach (var mediaItem in mediaItems)
            {
                mediaItem.Thumbnail = "/api/v1/stats/thumbProxy?id=" + mediaItem.Id;
            }
        }
    }
}
