using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioImage
    {
        Task Crear(Image image);
        Task<Image> Obtener(string idSpotify);
    }
}
