using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;

namespace FastMechanical.Controllers {
    public class PessoaController : Controller {

        private readonly IPessoaService _pessoaService;


        public PessoaController(IPessoaService pessoaService) {
            _pessoaService = pessoaService;

        }

        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de medicos ativos";
            var list = await _pessoaService.FindAllAsync();
            return View(list);
        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Pessoa cliente = await _pessoaService.FindByIdAsync(id.Value);
            if (cliente == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public async Task<IActionResult> Disable(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Pessoa cliente = await _pessoaService.FindByIdAsync(id.Value);
            if (cliente == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }


        public async Task<IActionResult> Enabled(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Pessoa cliente = await _pessoaService.FindByIdAsync(id.Value);
            if (cliente == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", cliente);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Pessoa cliente = await _pessoaService.FindByIdAsync(id.Value);
            if (cliente == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa cliente) {
            try {
                if (!ModelState.IsValid) {
                    return View(cliente);
                }
                string str = cliente.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                cliente.Cpf = str;
                cliente = _pessoaService.TransformUpperCase(cliente);
                await _pessoaService.InsertAsync(cliente);
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
                Pessoa cliente = await _pessoaService.FindByIdAsync(id);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                await _pessoaService.UpdateAsync(cliente);
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
                Pessoa cliente = await _pessoaService.FindByIdAsync(id);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                await _pessoaService.UpdateAsync(cliente);
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
                Pessoa dbPessoa = await _pessoaService.FindByIdAsync(id);
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
                dbPessoa = _pessoaService.TransformUpperCase(dbPessoa);
                await _pessoaService.UpdateAsync(dbPessoa);
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
