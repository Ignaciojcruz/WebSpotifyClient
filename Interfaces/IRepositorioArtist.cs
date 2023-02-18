using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioArtist
    {
        Task Actualizar(int id, bool like, bool revisado);
        Task Crear(Artist artist);
        Task<Artist> Obtener(string idSpotify);
    }
}
