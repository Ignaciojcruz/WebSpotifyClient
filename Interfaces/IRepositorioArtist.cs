using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioArtist
    {
        Task Crear(Artist artist);
        Task<Artist> Obtener(string idSpotify);
    }
}
