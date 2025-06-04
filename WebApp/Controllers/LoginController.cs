using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        Sistema sistema = Sistema.Instancia;
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo,string contrasenia)
        {
            try
            {
                if(string.IsNullOrEmpty(correo) && string.IsNullOrEmpty(contrasenia))
                {
                    ViewBag.Mensaje = "Ingrese datos";
                }else
                {
                    Usuario usuario = sistema.BusarUsuario(correo);
                    if(usuario != null)
                    {
                        HttpContext.Session.SetString("correo", usuario.Correo);

                        if(usuario is Administrador)
                        {
                            HttpContext.Session.SetString("rol", "Administrador");
                        }else
                            if(usuario is Ocasional)
                        {
                            HttpContext.Session.SetString("rol", "Ocasional");
                        }else if(usuario is Premium)
                        {
                            HttpContext.Session.SetString("rol", "Premium");
                        }
                        return Redirect("/Usuario/Index");
                    }
                }
            }catch (Exception ex)
            {
            ViewBag.Mensaje=ex.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }




    }
}
