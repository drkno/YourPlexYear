namespace YourPlexYear.Service.Config
{
    public class YourYearConfig
    {
        public string CookieDomain { get; set; } = ".example.com";
        public string OmbiPublicHostname { get; set; } = "";
        public string TautulliPublicHostname { get; set; } = "";
        public string TautulliApiKey { get; set; } = "";
        public string PlexPublicHostname { get; set; }

        public override string ToString()
        {
            return $"CookieDomain = {CookieDomain}\n" +
                $"OmbiPublicHostname = {OmbiPublicHostname}\n" +
                $"TautulliPublicHostname = {TautulliPublicHostname}\n" +
                $"PlexPublicHostname = {PlexPublicHostname}\n" +
                $"TautulliApiKey = ({TautulliApiKey.Length} chars)";
        }
    }
}
