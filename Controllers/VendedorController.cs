using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;
using FastMechanical.Models.Enums;

namespace FastMechanical.Controllers {
    public class VendedorController : Controller {

        private readonly IVendedorService _vendedorService;


        public VendedorController(IVendedorService vendedorService) {
            _vendedorService = vendedorService;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Listagem de vendedores ativos";
            var list = await _vendedorService.FindAllActiveAsync();
            return View(list);
        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> Inativos()
        {
            try
            {
                ViewData["Title"] = "Listagen de vendedores inativos.";
                var list = await _vendedorService.FindAllDisableAsync();
                return View("Index", list);
            }
            catch (Exception erro)
            {
                TempData[""] = erro.Message;
                return View("ErrorMessage");
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Vendedor vendedor = await _vendedorService.FindByIdAsync(id.Value);
            if (vendedor == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        public async Task<IActionResult> Disable(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Vendedor vendedor = await _vendedorService.FindByIdAsync(id.Value);
            if (vendedor == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }


        public async Task<IActionResult> Enabled(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Vendedor vendedor = await _vendedorService.FindByIdAsync(id.Value);
            if (vendedor == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", vendedor);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Vendedor vendedor = await _vendedorService.FindByIdAsync(id.Value);
            if (vendedor == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Vendedor vendedor)
        {
            try
            {
                if (!ModelState.IsValid){
                    return View(vendedor);
                }
                vendedor.Status = Status.Ativado;
                string str = vendedor.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                vendedor.Cpf = str;
                vendedor = _vendedorService.TransformUpperCase(vendedor);
                await _vendedorService.InsertAsync(vendedor);
                TempData["SuccessMessage"] = "Usuario cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id)
        {

            try
            {
                Vendedor vendedor = await _vendedorService.FindByIdAsync(id);
                if (vendedor == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                vendedor.Status = Status.Desativado;
                await _vendedorService.UpdateAsync(vendedor);
                TempData["SuccessMessage"] = "Usuário desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id)
        {

            try
            {
                Vendedor vendedor = await _vendedorService.FindByIdAsync(id);
                if (vendedor == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                vendedor.Status = Status.Ativado;
                await _vendedorService.UpdateAsync(vendedor);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Vendedor vendedor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(vendedor);
                }
                int id = (int)vendedor.Id;
                Vendedor dbPessoa = await _vendedorService.FindByIdAsync(id);
                if (dbPessoa == null)
                {
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
                dbPessoa = _vendedorService.TransformUpperCase(dbPessoa);
                await _vendedorService.UpdateAsync(dbPessoa);
                TempData["SuccessMessage"] = "Usuario alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}