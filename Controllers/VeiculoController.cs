using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using FastMechanical.Models.Enums;

namespace FastMechanical.Controllers
{
    public class VeiculoController : Controller
    {

        private readonly IVeiculoService _veiculoService;
        private readonly IClienteService _clienteService;


        public VeiculoController(IVeiculoService veiculoService, IClienteService clienteService)
        {
            _veiculoService = veiculoService;
            _clienteService = clienteService;

        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Listagem de veículos ativos";
            var list = await _veiculoService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> New()
        {
            VeiculoViewModel veiculo = new VeiculoViewModel { Clientes = await _clienteService.FindAllActiveAsync() };
            return View(veiculo);
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
            if (veiculo.Pessoa.Status == Status.Desativado)
            {
                TempData["ErrorMessage"] = $"Proprietário do veiculo desativado";
                return RedirectToAction("Index");
            }
            var list = await _clienteService.FindAllActiveAsync();
            VeiculoViewModel veiculoViewModel = new VeiculoViewModel
            {
                Renavam = veiculo.Renavam,
                Cor = veiculo.Cor,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Placa = veiculo.Placa,
                Year = veiculo.AnoDeFabricacao.Year,
                Id = veiculo.Id,
                Pessoa = veiculo.Pessoa,
                AnoDeFabricacao = veiculo.AnoDeFabricacao,
                Clientes = list,
                PessoaId = veiculo.Pessoa.Id
            };
            return View(veiculoViewModel);
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
        public async Task<IActionResult> New(VeiculoViewModel veiculoViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    veiculoViewModel.Clientes = await _clienteService.FindAllActiveAsync();
                    return View(veiculoViewModel);
                }
                string str = veiculoViewModel.Renavam;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                veiculoViewModel.Renavam = str;
                Cliente cliente = new Cliente();
                cliente = await _clienteService.FindByIdAsync(veiculoViewModel.PessoaId);
                Veiculo veiculoDb = new Veiculo { Renavam = veiculoViewModel.Renavam, Placa = veiculoViewModel.Placa, Modelo = veiculoViewModel.Modelo, Cor = veiculoViewModel.Cor, AnoDeFabricacao = new DateTime(veiculoViewModel.Year, 01, 01), Marca = veiculoViewModel.Marca, Pessoa = cliente, };
                veiculoDb = _veiculoService.TransformUpperCase(veiculoDb);
                await _veiculoService.InsertAsync(veiculoDb);
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
        public async Task<IActionResult> Edit(VeiculoViewModel veiculoViewModel)
        {
            try
            {
                veiculoViewModel.Clientes = await _clienteService.FindAllActiveAsync();
                if (!ModelState.IsValid)

                {
                    return View(veiculoViewModel);
                }

                Veiculo dbVeiculo = await _veiculoService.FindByIdAsync(veiculoViewModel.Id);
                if (dbVeiculo == null)
                {
                    TempData["ErrorMessage"] = $"ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (dbVeiculo.Pessoa.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = $"Dono do veiculo desativado";
                    return RedirectToAction("Index");
                }
                dbVeiculo.AnoDeFabricacao = new DateTime(veiculoViewModel.Year, 01, 01);
                dbVeiculo.Renavam = veiculoViewModel.Renavam;
                dbVeiculo.Placa = veiculoViewModel.Placa;
                dbVeiculo.Modelo = veiculoViewModel.Modelo;
                dbVeiculo.Cor = veiculoViewModel.Cor;
                dbVeiculo.Marca = veiculoViewModel.Marca;
                dbVeiculo.Pessoa = await _clienteService.FindByIdAsync(veiculoViewModel.PessoaId);
                dbVeiculo = _veiculoService.TransformUpperCase(dbVeiculo);
                await _veiculoService.UpdateAsync(dbVeiculo);
                TempData["SuccessMessage"] = $"Veículo alterado com sucesso";

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
