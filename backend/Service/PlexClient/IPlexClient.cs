using System.Threading.Tasks;

namespace Your2020.Service.PlexClient
{
    public interface IPlexClient
    {
        ServerIdentifier GetLocalServerIdentifier(string path = "Preferences.xml");
        Task<AccessTier> GetAccessTier(ServerIdentifier serverId, PlexToken token);
        Task<User> GetUserInfo(PlexToken token);
    }
}

