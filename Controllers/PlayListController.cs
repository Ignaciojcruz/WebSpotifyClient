using Microsoft.AspNetCore.Mvc;
using WebSpotifyClient.Interfaces;

namespace WebSpotifyClient.Controllers
{
    public class PlayListController : Controller
    {
        private readonly IRepositorioPlayList _repositorioPlayList;

        public PlayListController(IRepositorioPlayList repositorioPlayList) 
        {
            this._repositorioPlayList = repositorioPlayList;
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
        public IActionResult VerAlbum(string url)
        {
            ViewBag.Url = url;  
            
            return View();        
        }
    }
}
