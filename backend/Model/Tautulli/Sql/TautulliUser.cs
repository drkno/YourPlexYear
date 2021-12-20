using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli.Sql;

public class TautulliUser
{
    public string Username { get; set; }
    public string Email { get; set; }
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}