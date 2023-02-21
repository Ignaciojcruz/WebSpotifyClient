namespace WebSpotifyClient.Interfaces
{
    public interface IRepositorioSpotify
    {
        Task<List<string>> GetAlbumImages(string idArtist);
        Task ProcesoDescarga();
    }
}
