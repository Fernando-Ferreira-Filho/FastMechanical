using FastMechanical.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace FastMechanical.Filters {
    public class PaginaParaUsuarioLogado : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {

            string sessionUser = context.HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
            }
            else {
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);
                if (pessoa == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "login" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
