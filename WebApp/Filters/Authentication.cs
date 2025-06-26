using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string logueado = context.HttpContext.Session.GetString("correo");
            if (string.IsNullOrWhiteSpace(logueado))
            {
                context.Result = new RedirectToActionResult("Login", "Usuario", new { mensaje = "Debe iniciar sesion" });
            }
            base.OnActionExecuting(context);
        }
    }
}