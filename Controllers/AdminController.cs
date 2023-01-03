using FastMechanical.Models.Enums;
using FastMechanical.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Models;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaAdministrador]
    public class AdminController : Controller {

        private readonly IPessoaServices _personService;

        public AdminController(IPessoaServices personService) {
            _personService = personService;

        }

        public async Task<IActionResult> Index() {

            try {
                ViewData["Title"] = "Listagem de admines ativos";
                var list = await _personService.TodosAdminAtivosAsync();
                return View(list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Inativos() {
            try {
                ViewData["Title"] = "Listagen de admines inativos.";
                var list = await _personService.TodosAdminDesativadosAsync();
                return View("Index", list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int? id) {

            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(admin);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> Disable(int? id) {

            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(admin);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }


        public async Task<IActionResult> Enabled(int? id) {

            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (admin.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", admin);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }

        public async Task<IActionResult> Details(int? id) {

            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(admin);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa admin) {

            try {
                if (!ModelState.IsValid) {
                    return View(admin);
                }
                admin.Status = Status.Ativado;
                string str = admin.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                admin.Cpf = str;
                admin = await _personService.TransformCaptalizeAsync(admin);
                admin.TipoPessoa = TipoPessoa.Administrador;
                await _personService.SalvarAsync(admin);
                TempData["SuccessMessage"] = "Usuario cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id) {

            try {
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (admin.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                admin.Status = Status.Desativado;
                await _personService.AtualizarAsync(admin);
                TempData["SuccessMessage"] = "Usuário desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id) {

            try {
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id);
                if (admin == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (admin.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                admin.Status = Status.Ativado;
                await _personService.AtualizarAsync(admin);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pessoa admin) {
            try {
                if (!ModelState.IsValid) {
                    return View(admin);
                }
                int id = (int)admin.Id;
                Pessoa dbPessoa = await _personService.BuscarAdminPorIdAsync(id);
                if (dbPessoa == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Telefone = admin.Telefone;
                dbPessoa.Email = admin.Email;
                dbPessoa.Rua = admin.Rua;
                dbPessoa.Bairro = admin.Bairro;
                dbPessoa.Estado = admin.Estado;
                dbPessoa.Cidade = admin.Cidade;
                dbPessoa.Complemento = admin.Complemento;
                dbPessoa.Numero = admin.Numero;
                dbPessoa.DataDeNascimento = admin.DataDeNascimento;
                dbPessoa = await _personService.TransformCaptalizeAsync(dbPessoa);
                await _personService.AtualizarAsync(dbPessoa);
                TempData["SuccessMessage"] = "Usuario alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
