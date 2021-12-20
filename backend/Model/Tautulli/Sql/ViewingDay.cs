using System.Text.Json.Serialization;

namespace YourPlexYear.Model.Tautulli.Sql;

public class ViewingDay
{
    [JsonPropertyName("num")]
    public ushort DayNumber { get; set; }
    
    [JsonPropertyName("day")]
    public string DayOfWeek { get; set; }
    
    [JsonPropertyName("count")]
    public long NumberOfViews { get; set; }
}