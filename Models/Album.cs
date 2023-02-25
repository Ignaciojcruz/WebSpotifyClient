namespace WebSpotifyClient.Models
{
    public class Album : BaseSpotify
    {
        public int Year { get; set; }
        public string UrlImage { get; set; }
        public List<Artist> Artists { get; set; }
        public List<Track> Tracks { get; set; }

        

    }
}
