namespace WebSpotifyClient.Models
{
    public class PlayListView
    {
        public int IdArtist { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
        public string Album { get; set; }
        public string AlbumImageUrl { get; set; }
        public bool Revisado { get; set; }
        public bool Like { get; set; }                
        public DateTime AddedAt { get; set; }
        public string idArtistSpotify { get; set; }
    }
}
