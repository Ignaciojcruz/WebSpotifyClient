using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioRelationPlayListTrack
    {
        Task Crear(RelationPlayListTrack model);
        Task<RelationPlayListTrack> Obtener(string idSpotify);
    }
}
