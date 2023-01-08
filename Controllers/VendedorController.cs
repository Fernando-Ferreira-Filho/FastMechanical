using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Models;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    [PaginaParaAdministrador]

    public class VendedorController : Controller {


        private readonly IPessoaServices _personService;

        public VendedorController(IPessoaServices personService) {
            _personService = personService;

        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de vendedores ativos";
                var list = await _personService.TodosVendedoresAtivosAsync();
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
                ViewData["Title"] = "Listagen de vendedores inativos.";
                var list = await _personService.TodosVendedoresDesativadosAsync();
                return View("Index", list);
            }
            catch (Exception erro) {
                TempData[""] = erro.Message;
                return View("ErrorMessage");
            }
        }

        public async Task<IActionResult> Edit(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (vendedor.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (vendedor.TipoPessoa != TipoPessoa.Vendedor) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (vendedor.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (vendedor.TipoPessoa != TipoPessoa.Vendedor) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (vendedor.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }
                return View(vendedor);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa vendedor) {
            try {
                if (!ModelState.IsValid) {
                    return View(vendedor);
                }
                vendedor.Status = Status.Ativado;
                string str = vendedor.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                vendedor.Cpf = str;
                vendedor = await _personService.TransformCaptalizeAsync(vendedor);
                vendedor.TipoPessoa = TipoPessoa.Vendedor;
                vendedor.Password = Pessoa.PasswordGenerate();
                string title = "Senha de acesso so sitema Fastmechanical";
                string body = $"Olá, sua senha de acesso ao sistema fastmechanical é: {vendedor.Password}";
                Pessoa.SendMail(vendedor.Email, body, title);
                vendedor.SetPasswordHash();
                await _personService.SalvarAsync(vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (vendedor.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (vendedor.TipoPessoa != TipoPessoa.Vendedor) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                vendedor.Status = Status.Desativado;
                await _personService.AtualizarAsync(vendedor);
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
                Pessoa vendedor = await _personService.BuscarVendedoresPorIdAsync(id);
                if (vendedor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (vendedor.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (vendedor.TipoPessoa != TipoPessoa.Vendedor) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                vendedor.Status = Status.Ativado;
                await _personService.AtualizarAsync(vendedor);
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
        public async Task<IActionResult> Edit(Pessoa vendedor) {
            try {
                if (!ModelState.IsValid) {
                    return View(vendedor);
                }
                int id = (int)vendedor.Id;
                Pessoa dbPessoa = await _personService.BuscarVendedoresPorIdAsync(id);
                if (dbPessoa == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Telefone = vendedor.Telefone;
                dbPessoa.Email = vendedor.Email;
                dbPessoa.Rua = vendedor.Rua;
                dbPessoa.Bairro = vendedor.Bairro;
                dbPessoa.Estado = vendedor.Estado;
                dbPessoa.Cidade = vendedor.Cidade;
                dbPessoa.Complemento = vendedor.Complemento;
                dbPessoa.Numero = vendedor.Numero;
                dbPessoa.DataDeNascimento = vendedor.DataDeNascimento;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Senha(Pessoa vemdedor) {
            try {

                Pessoa vemdedorDb = await _personService.BuscarVendedoresPorIdAsync(vemdedor.Id);

                if (vemdedorDb == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (vemdedorDb.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }

                vemdedorDb.Password = Pessoa.PasswordGenerate();
                string title = "Nova senha de acesso so sitema FastMechanical";
                string body = $"Olá, sua nova senha de acesso ao sistema presmed é: {vemdedorDb.Password}";
                Pessoa.SendMail(vemdedorDb.Email, body, title);
                vemdedorDb.SetPasswordHash();
                await _personService.AtualizarAsync(vemdedorDb);
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