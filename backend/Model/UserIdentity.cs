using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace YourPlexYear.Model
{
    public class UserIdentity
    {
        public AccessTier AccessTier { get; }
        public AccessToken AccessToken { get; }
        public ServerIdentifier ServerIdentifier { get; }
        public Username Username { get; }
        public Email Email { get; }
        public Thumbnail Thumbnail { get; }

        public UserIdentity(IEnumerable<Claim> claims)
        {
            AccessTier = GetClaim(claims, Constants.AccessTierClaim, AccessTier.NoAccess, str => (AccessTier)Enum.Parse(typeof(AccessTier), str));
            AccessToken = GetClaim(claims, Constants.AccessTokenClaim, new AccessToken("fixme"), value => new AccessToken(value));
            ServerIdentifier = GetClaim(claims, Constants.ServerIdentifierClaim, null, value => new ServerIdentifier(value));
            Username = GetClaim(claims, Constants.UsernameClaim, new Username("fixme"), value => new Username(value));
            Email = GetClaim(claims, Constants.EmailClaim, null, value => new Email(value));
            Thumbnail = GetClaim(claims, Constants.ThumbnailClaim, null, value => new Thumbnail(value));
        }

        private T GetClaim<T>(IEnumerable<Claim> claims, string claim, T defaultValue, Func<string, T> mappingFunc)
        {
            try
            {
                return claims
                    .Where(x => x.Type == claim)
                    .Select(x => mappingFunc(x.Value))
                    .DefaultIfEmpty(defaultValue)
                    .First();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}