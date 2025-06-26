using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [Authentication]
    public class VueloController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }
        [ClienteFilter]
        public IActionResult VerVuelos()
        {
            List<Vuelo> vuelos = sistema.ObtenerVuelos();
            ViewBag.Vuelos = vuelos;
            return View();
        }

        [HttpPost]
        public IActionResult BuscarVueloPorRuta(string iataSalida, string iataLlegada)
        {
            List<Vuelo> vuelos = sistema.ObtenerVuelosPorRuta(iataSalida, iataLlegada);
            ViewBag.Vuelos = vuelos;
            return View("VerVuelos");
        }

        // ver detalle de los Vuelos
        public IActionResult Detalle(string id)
        {
            try
            {
                Vuelo vuelo = sistema.ObtenerVueloPorId(id);
                return View(vuelo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Detalle", new { mensaje = ex.Message }); 
            }

        }
        // tipo de equipaje: 
        public IActionResult Add()
        {
            return View(sistema.Pasajes);
        }
    }
}
