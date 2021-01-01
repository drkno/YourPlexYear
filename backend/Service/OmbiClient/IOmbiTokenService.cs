using System.Threading.Tasks;
using Your2020.Service.PlexClient;

namespace Your2020.Service.OmbiClient
{
    public interface IOmbiTokenService
    {
        Task<OmbiToken> GetOmbiToken(PlexToken plexToken);
    }
}
