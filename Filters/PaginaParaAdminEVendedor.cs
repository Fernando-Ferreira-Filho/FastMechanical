﻿using FastMechanical.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace FastMechanical.Filters {
    public class PaginaParaAdminEVendedor : ActionFilterAttribute {

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
                if (pessoa.TipoPessoa != Models.Enums.TipoPessoa.Administrador && pessoa.TipoPessoa != Models.Enums.TipoPessoa.Vendedor) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restricted" }, { "action", "Index" } });
                }

            }

            base.OnActionExecuting(context);
        }
    }
}
