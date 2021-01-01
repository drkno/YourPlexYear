namespace Your2020.Service.Config
{
    public interface IConfigurationService
    {
        YourYearConfig GetConfig();
        string GetConfigurationDirectory();
        string GetOmbiUrl();
        string GetTautulliUrl();
        string GetTautulliApiKey();
        string GetMoviesLibraryId();
        string GetTvShowsLibraryId();
        string GetPlexUrl();
    }
}
