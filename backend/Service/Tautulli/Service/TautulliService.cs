using System.Collections.Generic;
using System.Threading.Tasks;
using YourPlexYear.Model;
using YourPlexYear.Model.Tautulli;
using YourPlexYear.Model.Tautulli.Sql;
using YourPlexYear.Service.Tautulli.Client;

namespace YourPlexYear.Service.Tautulli.Service;

public class TautulliService : ITautulliService
{
    private readonly ITautulliClient _tautulliClient;

    public TautulliService(ITautulliClient tautulliClient)
    {
        _tautulliClient = tautulliClient;
    }

    public async Task<TautulliThumbnail> GetThumbnail(long id)
    {
        var thumbnails = await GetThumbnails(id);
        return thumbnails.GetFirst("There must be at least one thumbnail");
    }
    
    public Task<List<TautulliThumbnail>> GetThumbnails(long id)
    {
        return _tautulliClient.ExecuteSqlQuery<TautulliThumbnail>(Query.GetThumbnailQuery(id));
    }

    public Task<byte[]> GetThumbnailImage(long id)
    {
        return _tautulliClient.GetImage(id);
    }

    public async Task<TautulliUser> GetUserByEmail(string email)
    {
        var users = await GetUsersByEmail(email);
        return users.GetOnly("Email must only refer to a single user");
    }
    
    public Task<List<TautulliUser>> GetUsersByEmail(string email)
    {
        return _tautulliClient.ExecuteSqlQuery<TautulliUser>(Query.GetUserByEmailQuery(email));
    }

    public Task<List<WebBrowserUsage>> GetBrowserUsage(ushort year)
    {
        return _tautulliClient.ExecuteSqlQuery<WebBrowserUsage>(Query.GetBrowserUsageQuery(year));
    }

    public Task<List<MediaItem>> GetTvWatchHistory(long userId, ushort year)
    {
        return _tautulliClient.ExecuteSqlQuery<MediaItem>(Query.GetTvWatchHistoryQuery(userId, year));
    }

    public Task<List<MediaItem>> GetMovieWatchHistory(long userId, ushort year)
    {
        return _tautulliClient.ExecuteSqlQuery<MediaItem>(Query.GetMoviesWatchHistoryQuery(userId, year));
    }

    public async Task<MostPopular> GetMostPopularMedia(ushort year)
    {
        var mostPopularMedia = await _tautulliClient.ExecuteSqlQuery<MediaItem>(Query.GetMostPopularQuery(year));
        var mostPopularTv = mostPopularMedia.Count == 2 ? mostPopularMedia[1] : null;
        var mostPopularMovie = mostPopularMedia.Count >= 1 ? mostPopularMedia[0] : null;
        return new MostPopular(mostPopularTv, mostPopularMovie);
    }

    public async Task<TautulliBuddy> GetTvBuddy(string title, long userId, ushort year)
    {
        var buddies = await GetTvBuddies(title, userId, year);
        return buddies.GetFirstOrNull();
    }
    
    public Task<List<TautulliBuddy>> GetTvBuddies(string title, long userId, ushort year)
    {
        return _tautulliClient.ExecuteSqlQuery<TautulliBuddy>(Query.GetTvBuddyQuery(title, userId, year));
    }

    public Task<List<ViewingDay>> GetViewingDay(long userId, ushort year)
    {
        return _tautulliClient.ExecuteSqlQuery<ViewingDay>(Query.GetViewingDayQuery(userId, year));
    }
}