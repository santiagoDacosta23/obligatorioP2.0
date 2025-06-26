using Dominio; 
using Microsoft.AspNetCore.Mvc;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [Authentication]
    public class HomeController : Controller
    {
        private Sistema _sistema = Sistema.Instancia;
        public IActionResult Index(string mensaje)
        {
            ViewBag.mensaje = mensaje; 
            return View();
        }


        

    }

}
