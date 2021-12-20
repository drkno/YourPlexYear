using System;
using System.IO;
using System.Reflection;

namespace YourPlexYear.Model.Tautulli.Sql
{
    public static class Query
    {
        private static readonly string UserByEmailQuery = ReadSqlQuery("UserByEmail.sql");
        private static readonly string BrowserUsageQuery = ReadSqlQuery("BrowserUsage.sql");
        private static readonly string TvWatchHistoryQuery = ReadSqlQuery("WatchHistoryTv.sql");
        private static readonly string MoviesWatchHistoryQuery = ReadSqlQuery("WatchHistoryMovies.sql");
        private static readonly string MostPopular = ReadSqlQuery("MostPopular.sql");
        private static readonly string TvBuddy = ReadSqlQuery("TvBuddy.sql");
        private static readonly string Thumbnail = ReadSqlQuery("Thumbnail.sql");
        private static readonly string ViewingDay = ReadSqlQuery("ViewingDay.sql");

        public static string GetUserByEmailQuery(string email)
        {
            return string.Format(UserByEmailQuery, email);
        }

        public static string GetBrowserUsageQuery(ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(BrowserUsageQuery, started, stopped);
        }

        public static string GetTvWatchHistoryQuery(long userId, ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(TvWatchHistoryQuery, userId, started, stopped);
        }

        public static string GetMoviesWatchHistoryQuery(long userId, ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(MoviesWatchHistoryQuery, userId, started, stopped);
        }

        public static string GetMostPopularQuery(ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(MostPopular, started, stopped);
        }

        public static string GetTvBuddyQuery(string title, long userId, ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(TvBuddy, title, userId, started, stopped);
        }

        public static string GetThumbnailQuery(long thumbnailId)
        {
            return string.Format(Query.Thumbnail, thumbnailId);
        }

        public static string GetViewingDayQuery(long userId, ushort year)
        {
            var started = GetBeginningOfYear(year);
            var stopped = GetEndOfYear(year);
            return string.Format(ViewingDay, userId, started, stopped);
        }

        private static long GetBeginningOfYear(ushort year)
        {
            return new DateTimeOffset(year, 1, 1, 0, 0, 0, TimeSpan.Zero)
                .ToUnixTimeSeconds();
        }
    
        private static long GetEndOfYear(ushort year)
        {
            return new DateTimeOffset(year, 12, 31, 23, 59, 59, TimeSpan.Zero)
                .ToUnixTimeSeconds();
        }
        
        private static string ReadSqlQuery(string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "YourPlexYear.Model.Tautulli.Sql." + file;
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream ?? Stream.Null);
            return reader.ReadToEnd();
        }
    }
}
