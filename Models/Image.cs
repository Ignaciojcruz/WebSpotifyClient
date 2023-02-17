using SpotifyAPI.Web;

namespace WebSpotifyClient.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string IdSpotify { get; set; }
        public int Height { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public ImageTipo Tipo { get; set; }

    }
}
