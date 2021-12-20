using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli
{
    public class GetHistory
    {
        [JsonPropertyName("recordsFiltered")]
        public int RecordsFiltered { get; }
        [JsonPropertyName("recordsTotal")]
        public int RecordsTotal { get; }
        [JsonPropertyName("data")]
        public HistoryItem[] History { get; }
        [JsonPropertyName("draw")]
        public int Draw { get; }
        [JsonPropertyName("filter_duration")]
        public string FilterDuration { get; }
        [JsonPropertyName("total_duration")]
        public string TotalDuration { get; }

        public class HistoryItem
        {
            [JsonPropertyName("reference_id")]
            public int ReferenceId { get; }
            [JsonPropertyName("row_id")]
            public int RowId { get; }
            [JsonPropertyName("id")]
            public int Id { get; }
            [JsonPropertyName("date")]
            public int Date { get; }
            [JsonPropertyName("started")]
            public int Started { get; }
            [JsonPropertyName("stopped")]
            public int Stopped { get; }
            [JsonPropertyName("duration")]
            public int Duration { get; }
            [JsonPropertyName("paused_counter")]
            public int PausedCounter { get; }
            [JsonPropertyName("user_id")]
            public int UserId { get; }
            [JsonPropertyName("user")]
            public string User { get; }
            [JsonPropertyName("friendly_name")]
            public string FriendlyName { get; }
            [JsonPropertyName("platform")]
            public string Platform { get; }
            [JsonPropertyName("product")]
            public string Product { get; }
            [JsonPropertyName("player")]
            public string Player { get; }
            [JsonPropertyName("ip_address")]
            public string IpAddress { get; }
            [JsonPropertyName("live")]
            public int Live { get; }
            [JsonPropertyName("machine_id")]
            public string MachineId { get; }
            [JsonPropertyName("media_type")]
            public string MediaType { get; }
            [JsonPropertyName("rating_key")]
            public int RatingKey { get; }
            [JsonPropertyName("parent_rating_key")]
            public string ParentRatingKey { get; }
            [JsonPropertyName("grandparent_rating_key")]
            public string GrandparentRatingKey { get; }
            [JsonPropertyName("full_title")]
            public string FullTitle { get; }
            [JsonPropertyName("title")]
            public string Title { get; }
            [JsonPropertyName("parent_title")]
            public string ParentTitle { get; }
            [JsonPropertyName("grandparent_title")]
            public string GrandparentTitle { get; }
            [JsonPropertyName("original_title")]
            public string OriginalTitle { get; }
            [JsonPropertyName("year")]
            public int Year { get; }
            [JsonPropertyName("media_index")]
            public string MediaIndex { get; }
            [JsonPropertyName("parent_media_index")]
            public string ParentMediaIndex { get; }
            [JsonPropertyName("thumb")]
            public string Thumb { get; }
            [JsonPropertyName("originally_available_at")]
            public string OriginallyAvailableAt { get; }
            [JsonPropertyName("guid")]
            public string Guid { get; }
            [JsonPropertyName("transcode_decision")]
            public string TranscodeDecision { get; }
            [JsonPropertyName("percent_complete")]
            public int PercentComplete { get; }
            [JsonPropertyName("watched_status")]
            public int WatchedStatus { get; }
            [JsonPropertyName("group_count")]
            public int GroupCount { get; }
            [JsonPropertyName("group_ids")]
            public string GroupIds { get; }
            [JsonPropertyName("state")]
            public object State { get; }
            [JsonPropertyName("session_key")]
            public object SessionKey { get; }
        }
    }
}
