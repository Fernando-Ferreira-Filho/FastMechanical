using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models;
using FastMechanical.Models.ViewModel;

namespace FastMechanical.Controllers {
    public class VeiculoController : Controller {

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
            VeiculoViewModel veiculo = new VeiculoViewModel { Clientes = await _clienteService.FindAllActiveAsync()};
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
        public async Task<IActionResult> New(VeiculoViewModel veiculoViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(veiculoViewModel);
                }
                string str = veiculoViewModel.Veiculo.Renavam;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                veiculoViewModel.Veiculo.Renavam = str;
                Cliente cliente = new Cliente();
                cliente = await _clienteService.FindByIdAsync(veiculoViewModel.PessoaId);
                Veiculo veiculoDb = new Veiculo { Renavam = veiculoViewModel.Veiculo.Renavam, Placa = veiculoViewModel.Veiculo.Placa, Modelo = veiculoViewModel.Veiculo.Modelo, Cor = veiculoViewModel.Veiculo.Cor, Marca = veiculoViewModel.Veiculo.Marca, Pessoa = cliente };
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
