using System;
using System.Collections.Generic;

namespace Your2020.Model
{
    public class StatsResponse
    {
        public string Username{ get; set; }
        public string Tautulli{ get; set; }
        public string Ombi{ get; set; }
        public MediaLibrary Tv{ get; set; }
        public MediaLibrary Movies{ get; set; }
        public List<Browser> GlobalBrowsers{ get; set; }
        public List<Browser> YourBrowsers{ get; set; }
    }

    public class MediaLibrary
    {
        public long Total { get; set; }
        public List<MediaItem> Top10 { get; set; }
        public decimal FinishedPercent { get; set; }
        public long TotalWatchTime { get; set; }
        public MediaItem Oldest { get; set; }
        public MediaItem MostPaused { get; set; }
        public MediaItem Popular { get; set; }
        public string TopBuddy { get; set; }
        public long? TotalEpisodes { get; set; }
    }


    public class MediaItem : IComparable<MediaItem>
    {
        public string Title { get; set; }
        public long Duration { get; set; }
        public long Plays { get; set; }
        public short? Year { get; set; }
        public long PausedDuration { get; set; }
        public string Thumbnail { get; set; }
        public int CompareTo(MediaItem? other)
        {
            if (other == null)
            {
                return 0;
            }

            var year = Year.GetValueOrDefault(0);
            var otherYear = Year.GetValueOrDefault(0);
            if (year != otherYear)
            {
                return otherYear - year;
            }

            return (int) (other.Duration - Duration);
        }
    }

    public class Browser
    {
        public string Name{ get; set; }
        public long Value{ get; set; }
    }
}
