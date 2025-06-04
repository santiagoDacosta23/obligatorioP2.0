using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VerUsuarios()
        {
            List<Usuario> listaUsuario = sistema.Usuarios;
            ViewBag.usuarios = listaUsuario;


            return View();
        }

    }
}
