using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class VueloController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerVuelos()
        {
            List<Vuelo> vuelos = sistema.ObtenerVuelos();
            ViewBag.Vuelos = vuelos;
            return View();
        }




    }
}
