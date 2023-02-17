using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioTrack
    {
        Task Crear(Track track);
        Task<Track> Obtener(string idSpotify);
    }
}
