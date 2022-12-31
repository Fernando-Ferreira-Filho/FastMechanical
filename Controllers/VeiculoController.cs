using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;
using FastMechanical.Models.Enums;

namespace FastMechanical.Controllers {
    public class VeiculoController : Controller {

        private readonly IVeiculoServices _veiculoService;


        public VeiculoController(IVeiculoServices veiculoService) {
            _veiculoService = veiculoService;

        }

        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de veículos ativos";
            var list = await _veiculoService.TodosVeiculosAtivosAsync();
            return View(list);
        }

        public IActionResult New() {
            return View();
        }
        public async Task<IActionResult> Inativos() {
            try {
                ViewData["Title"] = "Listagen de vendedores inativos.";
                var list = await _veiculoService.TodosVeiculosDesativadosAsync();
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
            Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
            if (veiculo == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }

        public async Task<IActionResult> Disable(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
            if (veiculo == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }

            if (veiculo.Status == Models.Enums.Status.Desativado) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");

            }

            return View(veiculo);
        }


        public async Task<IActionResult> Enabled(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
            if (veiculo == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            if (veiculo.Status == Models.Enums.Status.Ativado) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");

            }
            return View("Disable", veiculo);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
            if (veiculo == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Veiculo veiculo) {
            try {
                if (!ModelState.IsValid) {
                    return View(veiculo);
                }
                string str = veiculo.Renavam;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                veiculo.Renavam = str;
                veiculo = _veiculoService.TransformCaptalizeAsync(veiculo);
                await _veiculoService.SalvarVeiculoAsync(veiculo);
                TempData["SuccessMessage"] = "Veículo cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Veiculo veiculo) {
            try {
                if (!ModelState.IsValid) {
                    return View(veiculo);
                }
                int id = (int)veiculo.Id;
                Veiculo dbPessoa = await _veiculoService.EncontrarVeiculoPorIdAsync(id);
                if (dbPessoa == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Renavam = veiculo.Renavam;
                dbPessoa.Placa = veiculo.Placa;
                dbPessoa.Modelo = veiculo.Modelo;
                dbPessoa.Cor = veiculo.Cor;
                dbPessoa.Marca = veiculo.Marca;
                dbPessoa = _veiculoService.TransformCaptalizeAsync(dbPessoa);
                await _veiculoService.AtualizarVeiculoAsync(dbPessoa);
                TempData["SuccessMessage"] = "Veículo alterado com sucesso";

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
                Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id);
                if (veiculo == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (veiculo.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                veiculo.Status = Status.Desativado;
                await _veiculoService.AtualizarVeiculoAsync(veiculo);
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
                Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id);
                if (veiculo == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (veiculo.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                veiculo.Status = Status.Ativado;
                await _veiculoService.AtualizarVeiculoAsync(veiculo);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }
    }
}
