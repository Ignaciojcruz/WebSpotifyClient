using SpotifyAPI.Web;
using System.Diagnostics.CodeAnalysis;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace WebSpotifyClient.Servicios
{
    
    public class RepositorioSpotify : IRepositorioSpotify
    {
        private readonly IRepositorioPlayList _repositorioPlayList;
        private readonly IRepositorioTrack _repositorioTrack;
        private readonly IRepositorioArtist _repositorioArtist;
        private readonly IRepositorioAlbum _repositorioAlbum;
        private readonly IRepositorioImage _repositorioImage;
        private readonly IRepositorioRelationTrackArtist _repositorioRelationTrackArtist;
        private readonly IRepositorioRelationPlayListTrack _repositorioRelationPlayListTrack;
        private readonly IRepositorioRelationArtistAlbum _repositorioRelationArtistAlbum;
        private readonly IRepositorioRelationAlbumTrack _repositorioRelationAlbumTrack;
        private string clientId;
        private string clientSecret;

        public RepositorioSpotify(IRepositorioPlayList repositorioPlayList
                                , IRepositorioTrack repositorioTrack
                                , IRepositorioArtist repositorioArtist
                                , IRepositorioAlbum repositorioAlbum
                                , IRepositorioImage repositorioImage
                                , IRepositorioRelationTrackArtist repositorioRelationTrackArtist
                                , IRepositorioRelationPlayListTrack repositorioRelationPlayListTrack
                                , IRepositorioRelationArtistAlbum repositorioRelationArtistAlbum
                                , IRepositorioRelationAlbumTrack repositorioRelationAlbumTrack
                                , IConfiguration configuration
                                    ) 
        {
            _repositorioPlayList = repositorioPlayList;
            _repositorioTrack = repositorioTrack;
            _repositorioArtist = repositorioArtist;
            _repositorioAlbum = repositorioAlbum;
            _repositorioImage = repositorioImage;
            _repositorioRelationTrackArtist = repositorioRelationTrackArtist;
            _repositorioRelationPlayListTrack = repositorioRelationPlayListTrack;
            _repositorioRelationArtistAlbum = repositorioRelationArtistAlbum;
            _repositorioRelationAlbumTrack = repositorioRelationAlbumTrack;
            clientId = configuration.GetValue<string>("CLIENT_ID");
            clientSecret = configuration.GetValue<string>("CLIENT_SECRET");
            
        }
        private async Task<SpotifyClient> Conecta()
        {            
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(clientId, clientSecret));

            
            return new SpotifyClient(config);
        }

        public async Task ProcesoDescarga()
        {
            
            try
            {
                var spotify = await Conecta();
                                
                var respuestaSpotify = await spotify.Playlists.Get("37i9dQZEVXcO1hUz9lSwNT");
                                
                FullPlaylist fullPlaylist = respuestaSpotify;

                //Insert-Update PlayList
                PlayList playlist = new PlayList();
                playlist.Name = fullPlaylist.Name;
                playlist.IdSpotify = fullPlaylist.Id;
                playlist.ExternalUrl = fullPlaylist.ExternalUrls["spotify"].ToString();

                await _repositorioPlayList.Crear(playlist);

                //Image playlist
                foreach (var img in fullPlaylist.Images)
                {
                    Models.Image image = new Models.Image();
                    image.IdSpotify = fullPlaylist.Id;
                    image.Height = img.Height;
                    image.Url = img.Url;
                    image.Width = img.Width;
                    image.Tipo = ImageTipo.PlayList;

                    await _repositorioImage.Crear(image);
                }

                foreach (var item in fullPlaylist.Tracks.Items)
                {
                    FullTrack fullTrack = (FullTrack)item.Track;
                                                            
                    //Track
                    Track track= new Track();
                    track.IdSpotify=fullTrack.Id;
                    track.Name = fullTrack.Name;
                    track.ExternalUrl = fullTrack.ExternalUrls["spotify"].ToString();
                    await _repositorioTrack.Crear(track);

                    //Album
                    Album album = new Album();
                    album.IdSpotify = fullTrack.Album.Id;
                    album.Name = fullTrack.Album.Name;
                    album.ExternalUrl = fullTrack.Album.ExternalUrls["spotify"].ToString();
                    await _repositorioAlbum.Crear(album);

                    //RelationAlbumTrack
                    RelationAlbumTrack relAlbumTrack = new RelationAlbumTrack();
                    relAlbumTrack.IdAlbum = fullTrack.Album.Id;
                    relAlbumTrack.IdTrack = fullTrack.Id;
                    await _repositorioRelationAlbumTrack.Crear(relAlbumTrack);                    

                    //Artists
                    foreach (SimpleArtist art in fullTrack.Artists)
                    {
                        //insertar artista
                        Artist artist = new Artist();
                        artist.IdSpotify=art.Id;
                        artist.Name = art.Name;
                        artist.ExternalUrl = art.ExternalUrls["spotify"].ToString();                                                
                        await _repositorioArtist.Crear(artist);

                        //RelationTrackArtist
                        RelationTrackArtist relationTrackArtist = new RelationTrackArtist();
                        relationTrackArtist.IdTrack = fullTrack.Id;
                        relationTrackArtist.IdArtist = art.Id;
                        await _repositorioRelationTrackArtist.Crear(relationTrackArtist);

                        //relationArtistAlbum
                        RelationArtistAlbum relationArtistAlbum = new RelationArtistAlbum();
                        relationArtistAlbum.IdArtist = art.Id;
                        relationArtistAlbum.IdAlbum = fullTrack.Album.Id;
                        await _repositorioRelationArtistAlbum.Crear(relationArtistAlbum);

                        

                    }

                    

                    //Image album
                    foreach (var img in fullTrack.Album.Images)
                    {
                        Models.Image image = new Models.Image();
                        image.IdSpotify = fullTrack.Album.Id;
                        image.Height = img.Height;
                        image.Url = img.Url;
                        image.Width = img.Width;
                        image.Tipo = ImageTipo.Album;

                        await _repositorioImage.Crear(image);
                    }

                    //RelationPlayListTrack
                    RelationPlayListTrack relationPlayListTrack = new RelationPlayListTrack();
                    relationPlayListTrack.IdTrack = fullTrack.Id; ;
                    relationPlayListTrack.idPlayList = fullPlaylist.Id;
                    relationPlayListTrack.AddedAt = Convert.ToDateTime(item.AddedAt);
                    await _repositorioRelationPlayListTrack.Crear(relationPlayListTrack);

                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            

        }

        public async Task<List<Album>> GetAlbums(string idArtist)
        {
                        
            //conecta a spotify
            var spotify = await Conecta(); 

            //descargar listado de albumes asociados al artista
            ArtistsAlbumsRequest albumsRequest = new ArtistsAlbumsRequest();
            albumsRequest.Limit = 50;
            var response = spotify.Artists.GetAlbums(idArtist, albumsRequest);

            Paging<SimpleAlbum> albumes = await response;
            List<Album> listaAlbum = new List<Album>();

            foreach (var item in albumes.Items)
            {
                Album album = new Album();

                //llenar objeto
                album.Name = item.Name + " - " + item.ReleaseDate.Substring(0, 4);
                album.Year = Convert.ToInt32(item.ReleaseDate.Substring(0,4));
                album.ExternalUrl = item.ExternalUrls["spotify"];

                //obtiene url imagen
                foreach (var img in item.Images)
                {
                    if (img.Height > 600)
                    {
                        album.UrlImage = img.Url;
                        break;
                    }
                }

                //obtiene listado artista
                List<Artist> listaArtistas = new List<Artist>();
                foreach (var art in item.Artists)
                {
                    Artist artist = new Artist();
                    artist.IdSpotify = art.Id;
                    artist.Name = art.Name;

                    listaArtistas.Add(artist);
                }
                album.Artists = listaArtistas;

                //recuperar listado de tracks
                List<Track> listaTracks = new List<Track>();
                var respoonseTracks = await spotify.Albums.GetTracks(item.Id);
                Paging<SimpleTrack> tracks = respoonseTracks;
                //listaTracks.Add("{[");
                foreach (var track in tracks.Items)
                {
                    Track t = new Track();
                    t.Name = track.Name;
                    t.TrackNumber = track.TrackNumber;
                    t.IdSpotify = track.Id;

                    listaTracks.Add(t);                    
                }
                //listaTracks.Add("}");

                album.Tracks =  listaTracks;

                listaAlbum.Add(album);
            }

            return listaAlbum;

            
        }
    }
}
