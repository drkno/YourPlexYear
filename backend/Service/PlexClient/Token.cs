using System;

namespace Your2020.Service.PlexClient
{
    public class PlexToken : ValueType<string>
    {
        public PlexToken(string token) : base(token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("Provided token cannot be null or empty");
            }
        }
    }
}
