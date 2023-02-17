using Microsoft.AspNetCore.Mvc;
using WebSpotifyClient.Interfaces;

namespace WebSpotifyClient.Controllers
{
    public class ProcesoDescargaController : Controller
    {
        private readonly IRepositorioSpotify _repositorioSpotify;
        
        public ProcesoDescargaController(IRepositorioSpotify repositorioSpotify
            , IRepositorioPlayList repositorioPlayList) 
        {
            this._repositorioSpotify = repositorioSpotify;        
        }

        public async Task<IActionResult> Index()
        {
            //llamada a repositorio de spotify para realizar proceso de descarga
            await _repositorioSpotify.ProcesoDescarga();                        

            return View();
        }

        

    }
}
