using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli.Sql;

public class WebBrowserUsage
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
    public string Platform { get; set; }
    public long Count { get; set; }
}