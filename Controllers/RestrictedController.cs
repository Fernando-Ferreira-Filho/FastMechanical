using FastMechanical.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    public class RestrictedController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
