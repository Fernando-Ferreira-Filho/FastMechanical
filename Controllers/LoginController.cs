using FastMechanical.Helper;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FastMechanical.Controllers {
    public class LoginController : Controller {

        private readonly ILoginService _loginService;
        private readonly ISessionUser _session;

        public LoginController(ILoginService loginService, ISessionUser sessionUser) {
            _loginService = loginService;
            _session = sessionUser;
        }

        public IActionResult Index() {
            // se usuario estiver logado redirecionar para a home
            if (_session.FindSessionUser() != null) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Exit() {
            _session.removeSessionUser();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login) {
            try {
                if (!ModelState.IsValid) {
                    return View("Index", login);
                }
                Pessoa pessoa = await _loginService.FindByLoginAsync(login.User);

                if (pessoa != null) {

                    if (pessoa.Status != Status.Ativado) {
                        TempData["ErrorMessage"] = $"Usuário desabilitado, favor procurar o administrador";
                        return View("Index");
                    }

                    if (pessoa.ValidPassword(login.Password)) {
                        _session.createSessionUser(pessoa);
                        return RedirectToAction("Index", "Home");
                    }

                }
                TempData["ErrorMessage"] = $"Usuário ou senha inválido";
                return View("Index");


            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao fazer login, erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
