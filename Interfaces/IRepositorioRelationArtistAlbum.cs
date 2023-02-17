using WebSpotifyClient.Models;

namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioRelationArtistAlbum
    {
        Task Crear(RelationArtistAlbum model);
        Task<RelationArtistAlbum> Obtener(string idSpotify);
    }
}
