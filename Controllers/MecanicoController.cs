using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models.ViewModel;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    [PaginaParaAdministrador]

    public class MecanicoController : Controller {

        private readonly IPessoaServices _personServices;


        public MecanicoController(IPessoaServices personServices) {
            _personServices = personServices;

        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de mecanicos ativos";
                var list = await _personServices.TodosMecanicosAtivosAsync();
                return View(list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> Inativos() {
            try {
                ViewData["Title"] = "Listagen de mecanicos inativos.";
                var list = await _personServices.TodosMecanicosDesativadosAsync();
                return View("Index", list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id.Value);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(mecanico);
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
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id.Value);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (mecanico.TipoPessoa != TipoPessoa.Mecanico) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(mecanico);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }


        public async Task<IActionResult> Enable(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id.Value);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.TipoPessoa != TipoPessoa.Mecanico) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", mecanico);
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
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id.Value);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(mecanico);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> Senha(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id.Value);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (mecanico.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }
                return View(mecanico);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa mecanico) {
            try {
                if (!ModelState.IsValid) {
                    return View(mecanico);
                }
                string str = mecanico.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                mecanico.Cpf = str;
                mecanico = await _personServices.TransformCaptalizeAsync(mecanico);
                mecanico.TipoPessoa = TipoPessoa.Mecanico;
                mecanico.Status = Status.Ativado;
                mecanico.Password = Pessoa.PasswordGenerate();
                string title = "Senha de acesso so sitema Fastmechanical";
                string body = $"Olá, sua senha de acesso ao sistema fastmechanical é: {mecanico.Password}";
                Pessoa.SendMail(mecanico.Email, body, title);
                mecanico.SetPasswordHash();
                await _personServices.SalvarAsync(mecanico);
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
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (mecanico.TipoPessoa != TipoPessoa.Mecanico) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                mecanico.Status = Status.Desativado;
                await _personServices.AtualizarAsync(mecanico);
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
                Pessoa mecanico = await _personServices.BuscarMecanicoPorIdAsync(id);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.TipoPessoa != TipoPessoa.Mecanico) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (mecanico.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                mecanico.Status = Status.Ativado;
                await _personServices.AtualizarAsync(mecanico);
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
        public async Task<IActionResult> Edit(Pessoa mecanico) {
            try {
                if (!ModelState.IsValid) {
                    return View(mecanico);
                }
                int id = (int)mecanico.Id;
                Pessoa dbPessoa = await _personServices.BuscarMecanicoPorIdAsync(id);
                if (dbPessoa == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Telefone = mecanico.Telefone;
                dbPessoa.Email = mecanico.Email;
                dbPessoa.Rua = mecanico.Rua;
                dbPessoa.Bairro = mecanico.Bairro;
                dbPessoa.Estado = mecanico.Estado;
                dbPessoa.Cidade = mecanico.Cidade;
                dbPessoa.Complemento = mecanico.Complemento;
                dbPessoa.Numero = mecanico.Numero;
                dbPessoa.DataDeNascimento = mecanico.DataDeNascimento;
                dbPessoa = await _personServices.TransformCaptalizeAsync(dbPessoa);
                await _personServices.AtualizarAsync(dbPessoa);
                TempData["SuccessMessage"] = "Usuario alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Senha(Pessoa mecaico) {
            try {

                Pessoa mecaicoDb = await _personServices.BuscarMecanicoPorIdAsync(mecaico.Id);

                if (mecaicoDb == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (mecaicoDb.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }

                mecaicoDb.Password = Pessoa.PasswordGenerate();
                string title = "Nova senha de acesso so sitema FastMechanical";
                string body = $"Olá, sua nova senha de acesso ao sistema presmed é: {mecaicoDb.Password}";
                Pessoa.SendMail(mecaicoDb.Email, body, title);
                mecaicoDb.SetPasswordHash();
                await _personServices.AtualizarAsync(mecaicoDb);
                TempData["SuccessMessage"] = "Senha enviada com sucesso";
                return RedirectToAction("Index");


            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return RedirectToAction("Index");
            }


        }
    }
}