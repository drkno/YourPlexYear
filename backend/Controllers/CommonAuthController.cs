using Microsoft.AspNetCore.Mvc;
using YourPlexYear.Model;

namespace YourPlexYear.Controllers
{
    public abstract class CommonAuthController : ControllerBase
    {
        private UserIdentity _identity;

        protected UserIdentity Identity
        {
            get { return _identity ??= new UserIdentity(User.Claims); }
        }
    }
}
