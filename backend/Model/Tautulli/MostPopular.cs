namespace YourPlexYear.Model.Tautulli;

public class MostPopular
{
    public MediaItem TvShow { get; }
    public MediaItem Movie { get; }
    
    public MostPopular(MediaItem tvShow, MediaItem movie)
    {
        TvShow = tvShow;
        Movie = movie;
    }
}