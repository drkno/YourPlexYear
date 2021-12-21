using System.Collections.Generic;
using System.Threading.Tasks;
using YourPlexYear.Model;
using YourPlexYear.Model.Tautulli;

namespace YourPlexYear.Service.Tautulli.Client
{
    public interface ITautulliClient
    {
        Task<TautulliToken> GetTautulliToken(AccessToken accessToken);

        Task<GetUsers[]> GetAllTautulliUsers();

        Task<GetHistory> GetTautulliHistory(int userId, int sectionId, MediaType mediaType = MediaType.Any,
            int length = -1, string search = null);

        Task<List<T>> ExecuteSqlQuery<T>(string sql);
        Task<byte[]> GetImage(long ratingKey);
    }
}
