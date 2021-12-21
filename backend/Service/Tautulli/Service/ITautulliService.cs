using System.Collections.Generic;
using System.Threading.Tasks;
using YourPlexYear.Model;
using YourPlexYear.Model.Tautulli;
using YourPlexYear.Model.Tautulli.Sql;

namespace YourPlexYear.Service.Tautulli.Service;

public interface ITautulliService
{
    Task<TautulliThumbnail> GetThumbnail(long id);
    Task<List<TautulliThumbnail>> GetThumbnails(long id);
    Task<byte[]> GetThumbnailImage(long id);
    Task<TautulliUser> GetUserByEmail(string email);
    Task<List<TautulliUser>> GetUsersByEmail(string email);
    Task<List<WebBrowserUsage>> GetBrowserUsage(ushort year);
    Task<List<MediaItem>> GetTvWatchHistory(long userId, ushort year);
    Task<List<MediaItem>> GetMovieWatchHistory(long userId, ushort year);
    Task<MostPopular> GetMostPopularMedia(ushort year);
    Task<TautulliBuddy> GetTvBuddy(string title, long userId, ushort year);
    Task<List<TautulliBuddy>> GetTvBuddies(string title, long userId, ushort year);
    Task<List<ViewingDay>> GetViewingDay(long userId, ushort year);
}