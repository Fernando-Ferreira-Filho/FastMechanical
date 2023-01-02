using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Services;
using FastMechanical.Models.Enums;
using FastMechanical.Models;
using FastMechanical.Filters;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    public class ServicosController : Controller {


        private readonly IServicosServices _servicosService;

        public ServicosController(IServicosServices servicosServices) {
            _servicosService = servicosServices;

        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de serviços ativos";
                var list = await _servicosService.TodosServicosAtivosAsync();
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
                ViewData["Title"] = "Listagen de serviços inativos.";
                var list = await _servicosService.TodosServicosDesativadosAsync();
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id.Value);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(servicos);
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id.Value);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (servicos.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(servicos);
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id.Value);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (servicos.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View("Disable", servicos);
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id.Value);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(servicos);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Servicos servicos) {
            try {
                if (!ModelState.IsValid) {
                    return View(servicos);
                }
                servicos.Status = Status.Ativado;

                servicos = await _servicosService.TransformCaptalizeAsync(servicos);

                await _servicosService.SalvarServicosAsync(servicos);
                TempData["SuccessMessage"] = "Serviço cadastrado com sucesso";
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (servicos.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                servicos.Status = Status.Desativado;
                await _servicosService.AtualizarServicosAsync(servicos);
                TempData["SuccessMessage"] = "Serviço desativado com sucesso";
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
                Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id);
                if (servicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (servicos.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                servicos.Status = Status.Ativado;
                await _servicosService.AtualizarServicosAsync(servicos);
                TempData["SuccessMessage"] = "Serviço ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Servicos servicos) {
            try {
                if (!ModelState.IsValid) {
                    return View(servicos);
                }
                int id = (int)servicos.Id;
                Servicos dbServicos = await _servicosService.EncontrarServicosPorIdAsync(id);
                if (dbServicos == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbServicos.Nome = servicos.Nome;
                dbServicos.Valor = servicos.Valor;
                dbServicos = await _servicosService.TransformCaptalizeAsync(dbServicos);
                await _servicosService.AtualizarServicosAsync(dbServicos);
                TempData["SuccessMessage"] = "Serviço alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}