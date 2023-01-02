using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    public class VeiculoController : Controller {

        private readonly IVeiculoServices _veiculoService;
        private readonly IPessoaServices _pessoaServices;


        public VeiculoController(IVeiculoServices veiculoService, IPessoaServices pessoaServices) {
            _veiculoService = veiculoService;
            _pessoaServices = pessoaServices;
        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de veículos ativos";
                var list = await _veiculoService.TodosVeiculosAtivosAsync();
                return View(list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> New() {
            try {

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
            VeiculoViewModel veiculo = new VeiculoViewModel { ClienteList = await _pessoaServices.TodosClientesAtivosAsync() };
            return View(veiculo);
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
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
                if (veiculo == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                VeiculoViewModel veiculoViewModel = new VeiculoViewModel { ClienteList = await _pessoaServices.TodosClientesAtivosAsync(), AnoDeFabricacao = veiculo.AnoDeFabricacao, Cor = veiculo.Cor, Marca = veiculo.Marca, Modelo = veiculo.Modelo, Pessoa = veiculo.Pessoa, Placa = veiculo.Placa, Renavam = veiculo.Renavam, Status = veiculo.Status, Id = veiculo.Id };
                return View(veiculoViewModel);
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
                Veiculo veiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id.Value);
                if (veiculo == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(veiculo);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(VeiculoViewModel veiculo) {

            try {
                veiculo.ClienteList = await _pessoaServices.TodosClientesAtivosAsync();
                if (veiculo.AnoDeFabricacao == 0 || veiculo.Modelo == null || veiculo.Cor == null || veiculo.Marca == null || veiculo.Placa == null || veiculo.Renavam == null) {

                    if (!ModelState.IsValid) {
                        return View(veiculo);
                    }
                }

                string str = veiculo.Renavam;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                veiculo.Renavam = str;

                Veiculo dbVeiculo = new Veiculo { AnoDeFabricacao = veiculo.AnoDeFabricacao, Modelo = veiculo.Modelo, Cor = veiculo.Cor, Marca = veiculo.Marca, Pessoa = await _pessoaServices.BuscarClientePorIdAsync(veiculo.Pessoa.Id), Placa = veiculo.Placa, Renavam = veiculo.Renavam, Status = Status.Ativado };

                dbVeiculo = _veiculoService.TransformCaptalizeAsync(dbVeiculo);
                await _veiculoService.SalvarVeiculoAsync(dbVeiculo);
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
        public async Task<IActionResult> Edit(VeiculoViewModel veiculo) {
            try {
                veiculo.ClienteList = await _pessoaServices.TodosClientesAtivosAsync();
                if (veiculo.AnoDeFabricacao == 0 || veiculo.Modelo == null || veiculo.Cor == null || veiculo.Marca == null || veiculo.Placa == null || veiculo.Renavam == null) {

                    if (!ModelState.IsValid) {
                        return View(veiculo);
                    }
                }

                int id = (int)veiculo.Id;
                Veiculo dbVeiculo = await _veiculoService.EncontrarVeiculoPorIdAsync(id);
                if (dbVeiculo == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbVeiculo.Renavam = veiculo.Renavam;
                dbVeiculo.Placa = veiculo.Placa;
                dbVeiculo.Modelo = veiculo.Modelo;
                dbVeiculo.Cor = veiculo.Cor;
                dbVeiculo.Marca = veiculo.Marca;
                dbVeiculo.Pessoa = await _pessoaServices.BuscarClientePorIdAsync(veiculo.Pessoa.Id);
                dbVeiculo = _veiculoService.TransformCaptalizeAsync(dbVeiculo);
                await _veiculoService.AtualizarVeiculoAsync(dbVeiculo);
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
