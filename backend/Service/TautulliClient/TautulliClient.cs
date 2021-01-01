using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Your2020.Model;
using Your2020.Model.Tautulli;
using Your2020.Service.Config;

namespace Your2020.Service.TautulliClient
{
    public class TautulliClient : ITautulliClient
    {
        private const string ACCEPT_HEADER = "application/json";
        private const string USER_AGENT_HEADER = "Your2020/1";

        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;

        public TautulliClient(IHttpClientFactory clientFactory,
                              IConfigurationService configurationService)
        {
            _httpClient = clientFactory.CreateClient();
            _configurationService = configurationService;
        }

        public async Task<TautulliToken> GetTautulliToken(AccessToken accessToken)
        {            
            var request = new HttpRequestMessage(HttpMethod.Post, _configurationService.GetTautulliUrl() + "/auth/signin");
            request.Content = new StringContent("username=&password=&token={accessToken.Value}&remember_me=1", Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Headers.Add("Accept", ACCEPT_HEADER);
            request.Headers.Add("User-Agent", USER_AGENT_HEADER);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tautulliResponse = JsonSerializer.Deserialize<TautulliTokenResponse>(json, new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });

            return string.IsNullOrWhiteSpace(tautulliResponse.Token) || tautulliResponse.Status != "success"
                ? null
                : new TautulliToken(tautulliResponse.UUID, tautulliResponse.Token);
        }

        public async Task<GetUsers[]> GetAllTautulliUsers()
        {
            var response = await GetTautulliResource<GetUsers[]>("get_users");
            return response.Value;
        }

        public async Task<GetHistory> GetTautulliHistory(int userId, int sectionId, MediaType mediaType = MediaType.Any, int length = -1, string search = null)
        {
            var param = new Dictionary<string, string>()
            {
                {"user_id", userId.ToString()},
                {"section_id", sectionId.ToString()},
                {"order_column", "date"},
                {"order_dir", "desc"},
                {"include_activity", "0"}
            };

            if (mediaType != MediaType.Any)
            {
                param.Add("media_type", mediaType.ToString().ToLower());
            }

            if (length >= 0)
            {
                param.Add("length", length.ToString());
            }

            if (search != null)
            {
                param.Add("search", search);
            }

            var response = await GetTautulliResource<GetHistory>("get_history", param);

            return response.Value;
        }

        public async Task<List<T>> ExecuteSqlQuery<T>(string sql)
        {
            var response = GetTautulliResource<List<T>>("sql", new Dictionary<string, string>()
            {
                {"query", sql}
            });
            return (await response).Value;
        }

        private async Task<Response<T>> GetTautulliResource<T>(string cmd, IDictionary<string, string> extraParameters = null)
        {
            var url = _configurationService.GetTautulliUrl() + "/api/v2?apikey=" + _configurationService.GetTautulliApiKey() + "&cmd=" + cmd;
            if (extraParameters != null)
            {
                url = extraParameters.Aggregate(url, (current, pair) => current + ("&" + Uri.EscapeDataString(pair.Key) + "=" + Uri.EscapeDataString(pair.Value)));
            }

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("Accept", USER_AGENT_HEADER);
            request.Headers.Add("User-Agent", USER_AGENT_HEADER);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var parsedResponse = JsonSerializer.Deserialize<Response<T>>(json, new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                
            });

            return parsedResponse;
        }
    }
}