using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli.Sql;

public class TautulliThumbnail
{
    [JsonPropertyName("thumbnail")]
    public string ThumbnailUrl { get; set; }

    public string GetPlexUrl(AccessToken plexToken, string plexHost)
    {
        return plexHost + ThumbnailUrl + "?X-Plex-Token=" + plexToken.Value;
    }
}