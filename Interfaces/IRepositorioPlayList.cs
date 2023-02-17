using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioPlayList
    {        
        Task Crear(PlayList playList);
        Task<PlayList> Obtener(string idSpotify);
        Task<IEnumerable<DateTime>> ObtenerFechas();
        Task<IEnumerable<PlayListView>> ObtenerPlaylistView(DateTime addedAt);
    }
}
