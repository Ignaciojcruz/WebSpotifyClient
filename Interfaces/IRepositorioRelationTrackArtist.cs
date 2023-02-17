using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioRelationTrackArtist
    {
        Task Crear(RelationTrackArtist model);
        Task<RelationTrackArtist> Obtener(string idSpotify);
    }
}
