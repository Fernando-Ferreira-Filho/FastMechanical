using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;

namespace FastMechanical.Controllers {
    public class MecanicoController : Controller {

        private readonly IMecanicoService _mecanicoService;


        public MecanicoController(IMecanicoService mecanicoService) {
            _mecanicoService = mecanicoService;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Listagem de medicos ativos";
            var list = await _mecanicoService.FindAllAsync();
            return View(list);
        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Mecanico mecanico = await _mecanicoService.FindByIdAsync(id.Value);
            if (mecanico == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(mecanico);
        }

        public async Task<IActionResult> Disable(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Mecanico mecanico = await _mecanicoService.FindByIdAsync(id.Value);
            if (mecanico == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(mecanico);
        }


        public async Task<IActionResult> Enabled(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Mecanico mecanico = await _mecanicoService.FindByIdAsync(id.Value);
            if (mecanico == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", mecanico);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Mecanico mecanico = await _mecanicoService.FindByIdAsync(id.Value);
            if (mecanico == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(mecanico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Mecanico mecanico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(mecanico);
                }
                string str = mecanico.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                mecanico.Cpf = str;
                mecanico = _mecanicoService.TransformUpperCase(mecanico);
                await _mecanicoService.InsertAsync(mecanico);
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
                Mecanico mecanico = await _mecanicoService.FindByIdAsync(id);
                if (mecanico == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                await _mecanicoService.UpdateAsync(mecanico);
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
                Mecanico mecanico = await _mecanicoService.FindByIdAsync(id);
                if (mecanico == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                await _mecanicoService.UpdateAsync(mecanico);
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
        public async Task<IActionResult> Edit(Mecanico mecanico)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(mecanico);
                }
                int id = (int)mecanico.Id;
                Mecanico dbPessoa = await _mecanicoService.FindByIdAsync(id);
                if (dbPessoa == null)
                {
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
                dbPessoa = _mecanicoService.TransformUpperCase(dbPessoa);
                await _mecanicoService.UpdateAsync(dbPessoa);
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