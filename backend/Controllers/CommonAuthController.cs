using Microsoft.AspNetCore.Mvc;
using Your2020.Model;

namespace Your2020.Controllers
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
