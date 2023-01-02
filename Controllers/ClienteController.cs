using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models.Enums;
using FastMechanical.Models;
using System.Collections.Generic;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    public class ClienteController : Controller {

        private readonly IPessoaServices _personService;
        private readonly IVeiculoServices _veiculoService;

        public ClienteController(IPessoaServices personService, IVeiculoServices veiculoServices) {
            _personService = personService;
            _veiculoService = veiculoServices;
        }

        public async Task<IActionResult> Index() {

            try {
                ViewData["Title"] = "Listagem de clientes ativos";
                var list = await _personService.TodosClientesAtivosAsync();
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
                ViewData["Title"] = "Listagen de clientes inativos.";
                var list = await _personService.TodosClientesDesativadosAsync();
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id.Value);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(cliente);
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id.Value);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.TipoPessoa != TipoPessoa.Cliente) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(cliente);
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id.Value);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.TipoPessoa != TipoPessoa.Cliente) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View("Disable", cliente);
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id.Value);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa cliente) {
            try {
                if (!ModelState.IsValid) {
                    return View(cliente);
                }
                cliente.Status = Status.Ativado;
                string str = cliente.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                cliente.Cpf = str;
                cliente = await _personService.TransformCaptalizeAsync(cliente);
                cliente.Status = Status.Ativado;
                cliente.TipoPessoa = TipoPessoa.Cliente;
                await _personService.SalvarAsync(cliente);
                TempData["SuccessMessage"] = "Usuário cadastrado com sucesso";
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.TipoPessoa != TipoPessoa.Cliente) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                List<Veiculo> veiculos = await _veiculoService.BuscarVeiculoPorClienteId(id);
                cliente.Status = Status.Desativado;
                await _personService.AtualizarAsync(cliente);
                foreach (var item in veiculos) {
                    item.Status = Status.Desativado;
                    await _veiculoService.AtualizarVeiculoAsync(item);
                }
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
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(id);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.TipoPessoa != TipoPessoa.Cliente) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (cliente.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                cliente.Status = Status.Ativado;
                await _personService.AtualizarAsync(cliente);

                List<Veiculo> veiculos = await _veiculoService.BuscarVeiculoPorClienteId(id);
                foreach (var item in veiculos) {
                    item.Status = Status.Ativado;
                    await _veiculoService.AtualizarVeiculoAsync(item);
                }
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
        public async Task<IActionResult> Edit(Pessoa cliente) {
            try {
                if (!ModelState.IsValid) {
                    return View(cliente);
                }
                int id = (int)cliente.Id;
                Pessoa dbPessoa = await _personService.BuscarClientePorIdAsync(id);
                if (dbPessoa == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Telefone = cliente.Telefone;
                dbPessoa.Email = cliente.Email;
                dbPessoa.Rua = cliente.Rua;
                dbPessoa.Bairro = cliente.Bairro;
                dbPessoa.Estado = cliente.Estado;
                dbPessoa.Cidade = cliente.Cidade;
                dbPessoa.Complemento = cliente.Complemento;
                dbPessoa.Numero = cliente.Numero;
                dbPessoa.DataDeNascimento = cliente.DataDeNascimento;
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
