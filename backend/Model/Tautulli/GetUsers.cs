using System.Text.Json.Serialization;

namespace Your2020.Model.Tautulli
{
    public class GetUsers
    {
        [JsonPropertyName("row_id")]
        public int RowId { get; }
        [JsonPropertyName("user_id")]
        public int UserId { get; }
        [JsonPropertyName("username")]
        public string Username { get; }
        [JsonPropertyName("friendly_name")]
        public string FriendlyName { get; }
        [JsonPropertyName("thumb")]
        public string Thumb { get; }
        [JsonPropertyName("email")]
        public string Email { get; }
        [JsonPropertyName("is_active")]
        public int IsActive { get; }
        [JsonPropertyName("is_admin")]
        public int IsAdmin { get; }
        [JsonPropertyName("is_home_user")]
        public int? IsHomeUser { get; }
        [JsonPropertyName("is_allow_sync")]
        public int? IsAllowSync { get; }
        [JsonPropertyName("is_restricted")]
        public int? IsRestricted { get; }
        [JsonPropertyName("do_notify")]
        public int DoNotify { get; }
        [JsonPropertyName("keep_history")]
        public int KeepHistory { get; }
        [JsonPropertyName("allow_guest")]
        public int AllowGuest { get; }
        [JsonPropertyName("server_token")]
        public string ServerToken { get; }
        [JsonPropertyName("shared_libraries")]
        public string SharedLibraries { get; }
        [JsonPropertyName("filter_all")]
        public string FilterAll { get; }
        [JsonPropertyName("filter_movies")]
        public string FilterMovies { get; }
        [JsonPropertyName("filter_tv")]
        public string FilterTV { get; }
        [JsonPropertyName("filter_music")]
        public string FilterMusic { get; }
        [JsonPropertyName("filter_photos")]
        public string FilterPhotos { get; }
    }
}
