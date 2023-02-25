using Microsoft.AspNetCore.Mvc;
using WebSpotifyClient.Interfaces;
using WebSpotifyClient.Models;

namespace WebSpotifyClient.Controllers
{
    public class PlayListController : Controller
    {
        private readonly IRepositorioPlayList _repositorioPlayList;
        private readonly IRepositorioArtist _repositorioArtist;
        private readonly IRepositorioSpotify _repositorioSpotify;

        public PlayListController(IRepositorioPlayList repositorioPlayList
                                    , IRepositorioArtist repositorioArtist
                                    , IRepositorioSpotify repositorioSpotify) 
        {
            this._repositorioPlayList = repositorioPlayList;
            this._repositorioArtist = repositorioArtist;
            this._repositorioSpotify = repositorioSpotify;
        }
        
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //buscar fechas de playlist almacenadas para poblar combo
            ViewBag.Fechas = await _repositorioPlayList.ObtenerFechas();

            //Buscar datos playlist
            DateTime addedAt = new DateTime(1900, 1, 1);
            var playList = await _repositorioPlayList.ObtenerPlaylistView(addedAt);
            

            return View(playList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime fecha)
        {
            //buscar datos en playlist asociados a la fecha

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> VerAlbumsArtist(string id)
        {

            //con el id ir a buscar todas las imagenes de albumes/artista 
            //List<string> imagenes = await _repositorioSpotify.GetAlbumImages(id);
            //ViewBag.imagenes = imagenes;

            //con el id del artista ir a buscar todas las imagenes de albumes/artista 
            List<Album> modelo = await _repositorioSpotify.GetAlbums(id);

            return View(modelo);        
        }

        [HttpPost]
        public async Task<IActionResult> Actualiza(PlayListView modelo)
        {
            //actualizar artista
            await _repositorioArtist.Actualizar(modelo.IdArtist, modelo.Like, modelo.Revisado);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> VerListaAlbum()
        {
            //Buscar datos playlist
            DateTime addedAt = new DateTime(1900, 1, 1);
            var playList = await _repositorioPlayList.ObtenerPlaylistView(addedAt);

            //List<string> urls = playList.Select(p => p.AlbumImageUrl).ToList();

            //ViewBag.listado = urls;

            return View(playList);

        }


    }
}
