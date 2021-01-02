using System.Text.Json.Serialization;

namespace Your2020.Model.Tautulli.Sql
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
    }

    public class BrowserResponse
    {
        [JsonPropertyName("user_id")]
        public long UserId { get; set; }
        public string Platform { get; set; }
        public long Count { get; set; }
    }

    public class BuddyResponse
    {
        public string Buddy { get; set; }
    }

    public class ThumbnailResponse
    {
        public string Thumbnail { get; set; }
    }
}
