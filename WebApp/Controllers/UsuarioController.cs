using Dominio;
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    
    public class UsuarioController : Controller
    {
        private Sistema sistema = Sistema.Instancia;

           // registro controller: 
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Ocasional ocasioanal)
        {
            try
            {
                if (string.IsNullOrEmpty(ocasioanal.Correo) || string.IsNullOrEmpty(ocasioanal.Contrasenia)
                    || string.IsNullOrEmpty(ocasioanal.Nombre)
                    || string.IsNullOrEmpty(ocasioanal.Documento) || string.IsNullOrEmpty(ocasioanal.Nacionalidad))
                {
                    ViewBag.Mensaje = "Debe completar todos los campos";
                    return View();
                }

                if (ocasioanal.Contrasenia.Length < 8 || !EsAlfanumerica(ocasioanal.Contrasenia))
                {
                    ViewBag.Mensaje = "La contraseña debe ser alfanumérica y tener al menos 8 caracteres.";
                    return View();
                }

                bool agregado = sistema.RegistrarCliente(ocasioanal);
                if (!agregado)
                {
                    ViewBag.Mensaje = "El correo ya está registrado.";
                    return View();
                }

                // Login automático
                HttpContext.Session.SetString("correo", ocasioanal.Correo);
                HttpContext.Session.SetString("rol", "Ocasional");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        private bool EsAlfanumerica(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
            {
                if (!char.IsLetterOrDigit(texto[i]))
                {
                    return false;
                }
            }
            return true;
        }
        [HttpGet]
        public IActionResult Login(string mensaje)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string contrasenia)
        {
            try
            {
                if (string.IsNullOrEmpty(correo) && string.IsNullOrEmpty(contrasenia))
                {
                    ViewBag.Mensaje = "Ingrese datos";
                }
                else
                {
                    Usuario usuario = sistema.BusarUsuario(correo);
                    if (usuario != null)
                    {
                        if (usuario.Contrasenia == contrasenia)
                        {
                            string nombreTipo = usuario.GetType().Name; //name (devuelve como una cadena string:)
                            HttpContext.Session.SetString("correo", usuario.Correo);
                            HttpContext.Session.SetString("rol", usuario.GetType().Name);

                        }
                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [Authentication]
        // Ver el perfil del cliente: 
        public IActionResult VerPerfil()
        {
            string correo = HttpContext.Session.GetString("correo");

            if (string.IsNullOrEmpty(correo))
            {
                return RedirectToAction("Login", "Acceso");
            }

            Usuario usuario = sistema.BusarUsuario(correo);

            if (usuario == null || !(usuario is Cliente))
            {
                return RedirectToAction("Login", "Acceso");
            }

            return View(usuario as Cliente);
        }
     
    }
}


