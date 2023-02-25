using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioSpotify
    {        
        Task<List<Album>> GetAlbums(string idArtist);
        Task ProcesoDescarga();
    }
}
