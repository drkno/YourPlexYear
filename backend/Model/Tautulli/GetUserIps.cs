using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli
{
    public class UserIps
    {
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; }
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; }
        [JsonPropertyName("data")]
        public HistoryRow[] History { get; }
        [JsonPropertyName("draw")]
        public int Draw { get; }

        public class HistoryRow
        {
            [JsonPropertyName("history_row_id")]
            public int HistoryRowId { get; }
            [JsonPropertyName("last_seen")]
            public int LastSeen { get; }
            [JsonPropertyName("first_seen")]
            public int FirstSeen { get; }
            [JsonPropertyName("ip_address")]
            public string IpAddress { get; }
            [JsonPropertyName("play_count")]
            public int PlayCount { get; }
            [JsonPropertyName("platform")]
            public string Platform { get; }
            [JsonPropertyName("player")]
            public string Player { get; }
            [JsonPropertyName("last_played")]
            public string LastPlayed { get; }
            [JsonPropertyName("rating_key")]
            public int RatingKey { get; }
            [JsonPropertyName("thumb")]
            public string Thumb { get; }
            [JsonPropertyName("media_type")]
            public string MediaType { get; }
            [JsonPropertyName("parent_title")]
            public string ParentTitle { get; }
            [JsonPropertyName("year")]
            public object Year { get; }
            [JsonPropertyName("media_index")]
            public object MediaIndex { get; }
            [JsonPropertyName("parent_media_index")]
            public object ParentMediaIndex { get; }
            [JsonPropertyName("live")]
            public int Live { get; }
            [JsonPropertyName("originally_available_at")]
            public string OriginallyAvailableAt { get; }
            [JsonPropertyName("guid")]
            public string Guid { get; }
            [JsonPropertyName("transcode_decision")]
            public string TranscodeDecision { get; }
            [JsonPropertyName("friendly_name")]
            public string FriendlyName { get; }
            [JsonPropertyName("user_id")]
            public int UserId { get; }
        }
    }
}
