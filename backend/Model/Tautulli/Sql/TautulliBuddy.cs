using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli.Sql
{
    public class TautulliBuddy
    {
        [JsonPropertyName("buddy")]
        public string BuddyUsername { get; set; }
    }
}
