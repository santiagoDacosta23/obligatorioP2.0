using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [Authentication]
    public class PasajeController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [AdminFilter]
        public IActionResult VerPasajes()
        {
            string logueado = HttpContext.Session.GetString("correo");
            Usuario usuarioLogueado = sistema.BusarUsuario(logueado);
            List<Pasaje> pasajes = sistema.ListarPasajesOrdenadosPorFecha();
            ViewBag.listaPasajes = pasajes;
            return View();
        }


        [ClienteFilter]
        public IActionResult MisPasajes()
        {
            string correo = HttpContext.Session.GetString("correo");
            Cliente cliente = sistema.BusarUsuario(correo) as Cliente;

            List<Pasaje> pasajes = sistema.ObtenerPasajesDelClientePorPrecio(correo);
            return View(pasajes);
        }

        public IActionResult Add(string numeroVuelo, DateTime fecha, string tipoEquipaje)
        {
            try
            {
                string correo = HttpContext.Session.GetString("correo");
                Usuario usuario = sistema.BusarUsuario(correo);

                Cliente cliente = (Cliente)usuario;

                Vuelo vuelo = sistema.ObtenerVueloPorId(numeroVuelo);
                TipoEquipaje equipaje = (TipoEquipaje)Enum.Parse(typeof(TipoEquipaje), tipoEquipaje);
                double precio = vuelo.CalcularCostoPorAsiento();

                Pasaje nuevo = new Pasaje(vuelo, fecha, cliente, equipaje, precio);
                sistema.AgregarPasaje(nuevo);

                return RedirectToAction("MisPasajes", new { mensaje = "Pasaje comprado correctamente." });
            }
            catch (Exception ex)
            {
                return RedirectToAction("MisPasajes", "Pasaje",
                    new
                    {
                        mensaje = ex.Message
                    }
                );
            }

           


        }
    }
}
