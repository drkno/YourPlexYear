using System.Collections.Generic;
using System.Linq;
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

        public StatsController(ILogger<StatsController> logger,
                               IPlexClient plexClient,
                               ITautulliClient tautulliClient,
                               IConfigurationService configuration)
        {
            _tautulliClient = tautulliClient;
            _configuration = configuration;
        }

        [HttpGet("2020")]
        public async Task<StatsResponse> GetStats()
        {
            // var identity = Identity;
            // var username = identity.Username;

            // var tautulliUser = await GetTautulliUser(identity.Email);


            var userResponse = await _tautulliClient.ExecuteSqlQuery<UserResponse>(string.Format(Query.UserByEmailQuery, "matthew@makereti.co.nz"));
            var username = new Username(userResponse[0].Username);

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

            return new StatsResponse()
            {
                Username = username.Value,
                Tautulli = _configuration.GetTautulliUrl(),
                Ombi = _configuration.GetOmbiUrl(),
                Tv = new MediaLibrary
                {
                    Total = tvResponse.Count,
                    Top10 = tvResponse.GetRange(0, 10),
                    TotalWatchTime = tvResponse.Select(tv => tv.Duration).Sum(),
                    Oldest = tvResponse.Min(),

                },
                // Movies,
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


        // private async Task<GetUsers> GetTautulliUser(Email email)
        // {
        //     var users = await _tautulliClient.GetAllTautulliUsers();
        //     return users.FirstOrDefault(user => user.Email == email.Value);
        // }
        //
        // public async Task<string> tautulli_get_name(int id)
        // {
        //     var response = await GetTautulliResource<GetUsers>("get_user_ips", new Dictionary<string, string>
        //     {
        //         {"user_id", id.ToString()}
        //     });
        //
        //     return response.Value.FriendlyName;
        // }
        //
        // public async void tautulli_get_user_movies(int id)
        // {
        //     var library_id_movies = _configurationService.GetMoviesLibraryId();
        //     var response = await GetTautulliResource<GetHistory>("get_history", new Dictionary<string, string>
        //     {
        //         {"user_id", id.ToString()},
        //         {"section_id", library_id_movies},
        //         {"order_column", "date"},
        //         {"order_dir", "desc"},
        //         {"include_activity", "0"},
        //         {"length", config.tautulli_length},
        //     });
        //
        //     var array = response.Value.History;
        //     var movies = new HashSet<Play>();
        //     var movies_percent_complete = new List<int>();
        //
        //     foreach (var historyItem in response.Value.History)
        //     {
        //         if (historyItem.Date > config.wrapped_end)
        //         {
        //             continue;
        //         }
        //         else if (historyItem.Date < config.wrapped_start)
        //         {
        //             break;
        //         }
        //
        //         var duration = historyItem.duration;
        //
        //         if (duration > 300)
        //         {
        //             movies_percent_complete.Add(historyItem.percent_complete);
        //         }
        //
        //         var title = historyItem.FullTitle;
        //         var year = historyItem.Year;
        //         var percent_complete = historyItem.percent_complete;
        //         var paused_counter = historyItem.paused_counter;
        //
        //         var found = false;
        //
        //         var play = new Play(title, year, 1, duration, paused_counter);
        //         if (movies.TryGetValue(play, out var existing))
        //         {
        //             existing.PlayCount += 1;
        //             existing.Duration += duration;
        //         }
        //         else
        //         {
        //             movies.Add(play);
        //         }
        //     }
        //
        //     if (movies.Count > 0)
        //     {
        //         movie_most_paused = array("title" => movies[0]["title"], "year" => movies[0]["year"], "plays" => movies[0]["plays"], "duration" => movies[0]["duration"], "paused_counter" => movies[0]["paused_counter"]);
        //         var oldestMovie = movies.Aggregate(null, (current, next) => current == null || current.Year < next.Year ? current : next);
        //     }
        //     else
        //     {
        //         movie_most_paused = array("title" => "No movies watched", "year" => 0, "plays" => 0, "duration" => 0, "paused_counter" => 0);
        //         movie_oldest = array("title" => "No movies watched", "year" => 0, "plays" => 0, "duration" => 0, "paused_counter" => 0);
        //     }
        //
        //     // Sort movies for longest pause
        //     paused_counter = array_column(movies, 'paused_counter');
        //     array_multisort(paused_counter, SORT_DESC, movies);
        //     if (movies.Count > 0)
        //     {
        //
        //     }
        //     else
        //     {
        //
        //     }
        //
        //     // Sort movies for oldest movie
        //
        //     if (movies.Count > 0)
        //     {
        //         movie_oldest = array("title" => movies[0]["title"], "year" => movies[0]["year"], "plays" => movies[0]["plays"], "duration" => movies[0]["duration"], "paused_counter" => movies[0]["paused_counter"]);
        //     }
        //     else
        //     {
        //
        //     }
        //
        //     // Sort movies by longest duration
        //     var duration = movies.Aggregate(null, (current, next) => current == null || current.Duration < next.Duration ? current : next);
        //
        //
        //     // Calculate average movie finishing percentage
        //     var sum = movies_percent_complete.Aggregate(0, (current, next) => current + next);
        //     var movie_percent_average = 0;
        //     if (movies_percent_complete.Count > 0)
        //     {
        //         movie_percent_average = sum / movies_percent_complete.Count;
        //     }
        //     else
        //     {
        //         movie_percent_average = 0;
        //     }
        //
        //     return array("movies" => movies, "user_movie_most_paused" => movie_most_paused, "user_movie_finishing_percent" => movie_percent_average, "user_movie_oldest" => movie_oldest);
        // }
        //
        // public async void tautulli_get_user_shows(int id)
        // {
        //     var library_id_tv = _configurationService.GetTvShowsLibraryId();
        //     var response = await GetTautulliResource<GetHistory>("get_history", new Dictionary<string, string>
        //     {
        //         {"user_id", id.ToString()},
        //         {"section_id", library_id_tv},
        //         {"order_column", "date"},
        //         {"order_dir", "desc"},
        //         {"include_activity", "0"},
        //         {"length", config.tautulli_length},
        //     });
        //
        //     var historyItems = response.response.data.data;
        //     var shows = new HashSet<Play>();
        //
        //     foreach (var tvShow in historyItems)
        //     {
        //         if (tvShow.date > config.wrapped_end)
        //         {
        //             continue;
        //         }
        //         else if (tvShow.date < config.wrapped_start)
        //         {
        //             break;
        //         }
        //
        //         var title = tvShow.grandparent_title;
        //         var duration = tvShow.duration;
        //
        //         var play = new Play(title, 0, 1, duration, 0);
        //         if (shows.TryGetValue(play, out var existingValue))
        //         {
        //             existingValue.Duration += duration;
        //             existingValue.PlayCount++;
        //         }
        //         else
        //         {
        //             shows.Add(new Play(title, 0, 1, duration, 0));
        //         }
        //     }
        //
        //     //Sort shows by duration
        //     var duration = array_column(shows, 'duration');
        //     array_multisort(duration, SORT_DESC, shows);
        //
        //     return historyItems("shows" => shows);
        // }
        //
        // public async void tautulli_get_user_show_buddy(int id, Play[] shows)
        // {
        //     var library_id_tv = _configurationService.GetTvShowsLibraryId();
        //     var response = await GetTautulliResource<GetHistory>("get_history", new Dictionary<string, string>
        //     {
        //         {"user_id", id.ToString()},
        //         {"section_id", library_id_tv},
        //         {"order_column", "date"},
        //         {"media_type", "episode"},
        //         {"order_dir", "desc"},
        //         {"include_activity", "0"},
        //         {"length", config.tautulli_length},
        //         {"search", Uri.EscapeDataString(shows[0].Title)},
        //     });
        //
        //
        //     var array = response.response.data.data;
        //     var users = new Dictionary<string, int>();
        //
        //     for (var i = 0; i < count(array); i++)
        //     {
        //         var user = array[i].friendly_name;
        //         var duration = array[i].duration;
        //
        //         if (array[i].date > config.wrapped_end || array[i].grandparent_title != shows[0].Title)
        //         {
        //             continue;
        //         }
        //         else if (array[i].date < config.wrapped_start)
        //         {
        //             break;
        //         }
        //
        //         users[user] = users.GetValueOrDefault(user, 0) + duration;
        //     }
        //
        //     // Sort show-buddies by duration
        //     duration = array_column(top_show_users, 'duration');
        //     array_multisort(duration, SORT_DESC, top_show_users);
        //
        //     var index = 0;
        //     if (count(top_show_users) > 1)
        //     {
        //         for (var i = 0; i < count(top_show_users); i++)
        //         {
        //             if (top_show_users[i]["user"] == name)
        //             {
        //                 index = i;
        //             }
        //         }
        //
        //         if (((index == 0) || (index % 2 == 0)) AND(index < count(top_show_users) - 1)) {
        //             buddy = array("user" => top_show_users[index + 1]["user"], "duration" => top_show_users[index + 1]["duration"], "found" => true, "watched_relative_to_you" => "less");
        //         } else
        //         {
        //             buddy = array("user" => top_show_users[index - 1]["user"], "duration" => top_show_users[index - 1]["duration"], "found" => true, "watched_relative_to_you" => "more");
        //         }
        //
        //     }
        //     else
        //     {
        //         buddy = array("user" => false, "duration" => 0, found => false, "watched_relative_to_you" => false);
        //     }
        //
        //     return buddy;
        // }
        //
        // public async void tautulli_get_year_stats(int id)
        // {
        //     var library_id_tv = _configurationService.GetTvShowsLibraryId();
        //     var response = await GetTautulliResource<GetHistory>("get_history", new Dictionary<string, string>
        //     {
        //         {"user_id", id.ToString()},
        //         {"section_id", library_id_tv},
        //         {"order_column", "date"},
        //         {"media_type", "movie"},
        //         {"order_dir", "desc"},
        //         {"include_activity", "0"},
        //         {"length", config.tautulli_length},
        //     });
        //
        //     var array = response.response.data.data;
        //     users = array();
        //     movies = array();
        //     shows = array();
        //
        //     for (var i = 0; i < array.Length; i++)
        //     {
        //         if (intval(array[i].date) > config.wrapped_end)
        //         {
        //             continue;
        //         }
        //         else if (intval(array[i].date) < config.wrapped_start)
        //         {
        //             break;
        //         }
        //
        //         var title = array[i].full_title;
        //         var duration = array[i].duration;
        //         var user = array[i].friendly_name;
        //         var user_id = array[i].user_id;
        //         var year = array[i].year;
        //
        //         user_found = false;
        //         movie_found = false;
        //
        //         for (var j = 0; j < count(users); j++)
        //         {
        //             if (users[j]["id"] == user_id)
        //             {
        //                 users[j]["duration_movies"] = intval(users[j]["duration_movies"]) + intval(duration);
        //                 users[j]["duration"] = intval(users[j]["duration"]) + intval(duration);
        //                 users[j]["plays"] = intval(users[j]["plays"]) + 1;
        //                 user_found = true;
        //                 break;
        //             }
        //         }
        //
        //         if (!user_found)
        //         {
        //             array_push(users, array("user" => user, "id" => user_id, "duration" => duration, "duration_movies" => duration, "duration_shows" => 0, "plays" => 1));
        //         }
        //
        //         for (j = 0; j < count(movies); j++)
        //         {
        //             if (movies[j]["title"] == title && movies[j]["year"] == year)
        //             {
        //                 movies[j]["duration"] = intval(movies[j]["duration"]) + intval(duration);
        //                 movies[j]["plays"] = intval(movies[j]["plays"]) + 1;
        //                 movie_found = true;
        //                 break;
        //             }
        //         }
        //
        //         if (!movie_found)
        //         {
        //             array_push(movies, array("title" => title, "year" => year, "duration" => duration, "plays" => 1));
        //         }
        //     }
        //
        //     url = connection. "/api/v2?apikey=".config.tautulli_apikey. "&cmd=get_history&media_type=episode&include_activity=0&order_column=date&order_dir=desc&length=".config.tautulli_length;
        //     response = json_decode(file_get_contents(url));
        //     array = response.response.data.data;
        //
        //     for (var i = 0; i < array.Length; i++)
        //     {
        //         if (intval(array[i].date) > config.wrapped_end)
        //         {
        //             continue;
        //         }
        //         else if (intval(array[i].date) < config.wrapped_start)
        //         {
        //             break;
        //         }
        //
        //         var title = array[i].grandparent_title;
        //         var duration = array[i].duration;
        //         var user = array[i].friendly_name;
        //         var user_id = array[i].user_id;
        //         var year = array[i].year;
        //
        //         user_found = false;
        //         show_found = false;
        //
        //         for (j = 0; j < count(users); j++)
        //         {
        //             if (users[j]["id"] == user_id)
        //             {
        //                 users[j]["duration_shows"] = intval(users[j]["duration_shows"]) + intval(duration);
        //                 users[j]["duration"] = intval(users[j]["duration"]) + intval(duration);
        //                 users[j]["plays"] = intval(users[j]["plays"]) + 1;
        //                 user_found = true;
        //                 break;
        //             }
        //         }
        //
        //         if (!user_found)
        //         {
        //             array_push(users, array("user" => user, "id" => user_id, "duration" => duration, "duration_movies" => 0, "duration_shows" => duration, "plays" => 1));
        //         }
        //
        //         for (var j = 0; j < count(shows); j++)
        //         {
        //             if (shows[j]["title"] == title)
        //             {
        //                 shows[j]["duration"] = intval(shows[j]["duration"]) + intval(duration);
        //                 shows[j]["plays"] = intval(shows[j]["plays"]) + 1;
        //                 show_found = true;
        //                 break;
        //             }
        //         }
        //
        //         if (!show_found)
        //         {
        //             array_push(shows, array("title" => title, "duration" => duration, "plays" => 1));
        //         }
        //     }
        //
        //     // Sort movies by duration
        //     duration = array_column(movies, 'duration');
        //     array_multisort(duration, SORT_DESC, movies);
        //
        //     // Sort movies by duration
        //     duration = array_column(shows, 'duration');
        //     array_multisort(duration, SORT_DESC, shows);
        //
        //     // Sort users by combined duration
        //     duration = array_column(users, 'duration');
        //     array_multisort(duration, SORT_DESC, users);
        //
        //     return array("top_movies" => movies, "users" => users, "top_shows" => shows);
        // }
    }
}
