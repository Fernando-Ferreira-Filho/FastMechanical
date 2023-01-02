using FastMechanical.Filters;
using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    public class HomeController : Controller {

        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService) {
            _loginService = loginService;

        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult TrocarSenha() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TrocarSenha(TrocarSenhaViewModel pwd) {

            if (!ModelState.IsValid) {
                return View(pwd);
            }


            if (pwd.Password != pwd.ConfirmPassword) {
                TempData["ErrorMessage"] = $"As senhas não conferem";
                return View(pwd);
            }


            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

            pessoa.Password = pwd.Password;

            pessoa.SetPasswordHash();

            await _loginService.ChangePasswordAsync(pessoa);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
