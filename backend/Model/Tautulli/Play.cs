using System;

namespace YourPlexYear.Model.Tautulli
{
    public class Play
    {
        public string Title { get; }
        public int Year { get; }
        public int PlayCount { get; set; }
        public int Duration { get; set; }
        public int PausedCounter { get; }

        public Play(in string title, in int year, int playCount, in int duration, in int pausedCounter)
        {
            Title = title;
            Year = year;
            PlayCount = playCount;
            Duration = duration;
            PausedCounter = pausedCounter;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            var other = (Play) obj;
            return Title == other.Title && Year == other.Year;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Year);
        }
    }
}
