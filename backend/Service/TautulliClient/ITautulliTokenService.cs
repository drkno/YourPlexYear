using System.Collections.Generic;
using System.Threading.Tasks;
using Your2020.Model;
using Your2020.Model.Tautulli;

namespace Your2020.Service.TautulliClient
{
    public interface ITautulliClient
    {
        Task<TautulliToken> GetTautulliToken(AccessToken accessToken);

        Task<GetUsers[]> GetAllTautulliUsers();

        Task<GetHistory> GetTautulliHistory(int userId, int sectionId, MediaType mediaType = MediaType.Any,
            int length = -1, string search = null);

        Task<List<T>> ExecuteSqlQuery<T>(string sql);
    }
}
