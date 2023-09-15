using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioRelationAlbumTrack
    {
        Task Crear(RelationAlbumTrack model);
    }
}
