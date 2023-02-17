using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioAlbum
    {
        Task Crear(Album album);
        Task<Album> Obtener(string idSpotify);
    }
}
