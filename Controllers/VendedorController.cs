using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Models;

namespace FastMechanical.Controllers {
    public class VendedorController : Controller {


        private readonly IPersonServices _personService;



        public VendedorController(IPersonServices personService) {
            _personService = personService;

        }

        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de vendedores ativos";
            var list = await _personService.TodosVendedoresAtivosAsync();
            return View(list);
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

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
            if (vendedor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        public async Task<IActionResult> Disable(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
            if (vendedor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }


            if (vendedor.Status == Status.Desativado) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }


        public async Task<IActionResult> Enabled(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
            if (vendedor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }

            if (vendedor.Status == Status.Ativado) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", vendedor);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id.Value);
            if (vendedor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Person vendedor) {
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
                Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id);
                if (vendedor == null) {
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
                Person vendedor = await _personService.BuscarVendedoresPorIdAsync(id);
                if (vendedor == null) {
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
        public async Task<IActionResult> Edit(Person vendedor) {
            try {
                if (!ModelState.IsValid) {
                    return View(vendedor);
                }
                int id = (int)vendedor.Id;
                Person dbPessoa = await _personService.BuscarVendedoresPorIdAsync(id);
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
    }
}