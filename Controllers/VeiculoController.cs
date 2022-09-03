using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;

namespace FastMechanical.Controllers {
    public class VeiculoController : Controller {

        private readonly IVeiculoService _veiculoService;


        public VeiculoController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Listagem de veículos ativos";
            var list = await _veiculoService.FindAllAsync();
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
            Veiculo veiculo = await _veiculoService.FindByIdAsync(id.Value);
            if (veiculo == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }

        public async Task<IActionResult> Disable(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.FindByIdAsync(id.Value);
            if (veiculo == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }


        public async Task<IActionResult> Enabled(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.FindByIdAsync(id.Value);
            if (veiculo == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", veiculo);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.FindByIdAsync(id.Value);
            if (veiculo == null)
            {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Veiculo veiculo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(veiculo);
                }
                string str = veiculo.Renavam;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                veiculo.Renavam = str;
                veiculo = _veiculoService.TransformUpperCase(veiculo);
                await _veiculoService.InsertAsync(veiculo);
                TempData["SuccessMessage"] = "Veículo cadastrado com sucesso";
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
        public async Task<IActionResult> Edit(Veiculo veiculo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(veiculo);
                }
                int id = (int)veiculo.Id;
                Veiculo dbPessoa = await _veiculoService.FindByIdAsync(id);
                if (dbPessoa == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Renavam = veiculo.Renavam;
                dbPessoa.Placa = veiculo.Placa;
                dbPessoa.Modelo = veiculo.Modelo;
                dbPessoa.Cor = veiculo.Cor;
                dbPessoa.Marca = veiculo.Marca;
                dbPessoa = _veiculoService.TransformUpperCase(dbPessoa);
                await _veiculoService.UpdateAsync(dbPessoa);
                TempData["SuccessMessage"] = "Veículo alterado com sucesso";

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
