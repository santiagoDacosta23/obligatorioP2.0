using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    public class AdministradorController : Controller
    {

        
        Sistema sistema = Sistema.Instancia;
            [AdminFilter]
            public IActionResult Index()
            {
                return View();
            }

            public IActionResult VerClientes()
            {
                ViewBag.Clientes = sistema.ObtenerClientes();
                return View();
            }

            [HttpGet]
            public IActionResult EditarCliente(string correo, string mensaje = null)
            {
                ViewBag.Mensaje = mensaje;

                Cliente cliente = sistema.BusarUsuario(correo) as Cliente;
                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado");
                }

                return View(cliente);
            }

            [HttpPost]
            public IActionResult EditarCliente(Cliente clienteEditado)
            {
                try
                {
                    Cliente original = sistema.BusarUsuario(clienteEditado.Correo) as Cliente;
                    if (original == null)
                    {
                        return RedirectToAction("EditarClientes", new { mensaje = "Cliente no encontrado." });
                    }

                    if (original is Premium && clienteEditado is Premium)
                    {
                        ((Premium)original).ModificarPuntos(((Premium)clienteEditado).Puntos);
                    }
                    else if (original is Ocasional && clienteEditado is Ocasional)
                    {
                        ((Ocasional)original).CambiarElegibilidad();
                    }

                    return RedirectToAction("EditarClientes", new { mensaje = "Cliente actualizado correctamente." });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("EditarCliente", new { correo = clienteEditado.Correo, mensaje = ex.Message });
                }
            }

            // NUEVO: Para editar desde la tabla de VerClientes
            [HttpPost]
            public IActionResult ActualizarCliente(string Documento, int? Puntos, bool? Elegible)
            {
                Cliente cliente = sistema.BuscarClientePorDocumento(Documento);

                if (cliente == null)
                {
                    ViewBag.Mensaje = "Cliente no encontrado.";
                }
                else if (cliente is Premium && Puntos.HasValue)
                {
                    ((Premium)cliente).ModificarPuntos(Puntos.Value);
                    ViewBag.Mensaje = "Puntos actualizados correctamente.";
                }
                else if (cliente is Ocasional && Elegible.HasValue)
                {
                    ((Ocasional)cliente).elegido = Elegible.Value;
                    ViewBag.Mensaje = "Estado de elegibilidad actualizado correctamente.";
                }

                ViewBag.Clientes = sistema.ObtenerClientes();
                return View("VerClientes");
            }
        
    }
}
