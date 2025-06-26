using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Filters
{
    public class AdminFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string rol = context.HttpContext.Session.GetString("rol");
            if (rol!="Administrador")
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Debe iniciar sesion" });
            }
            base.OnActionExecuting(context);
        }
    }
}
