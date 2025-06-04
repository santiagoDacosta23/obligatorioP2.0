using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PasajeController : Controller
    { Sistema sistema = Sistema.Instancia;
       public IActionResult Index()
       {
         return View();
       }


        public IActionResult VerPasajes()
        {
            List<Pasaje> pasajes = sistema.ListarPasajesOrdenadosPorFecha();
            ViewBag.listaPasajes = pasajes;
            return View();
        }







    }
}
