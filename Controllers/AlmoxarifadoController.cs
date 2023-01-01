using FastMechanical.Models;
using FastMechanical.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Models.Enums;

namespace FastMechanical.Controllers {
    public class AlmoxarifadoController : Controller {
        private readonly IAlmoxarifadoServices _almoxarifadoServices;

        public AlmoxarifadoController(IAlmoxarifadoServices almoxarifadoServices) {
            _almoxarifadoServices = almoxarifadoServices;

        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de materiais ativos";
                var list = await _almoxarifadoServices.ListarTodosMateriaisAtivos();
                return View(list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> MateriaisDesativados() {
            try {
                ViewData["Title"] = "Listagem de materiais desativados";
                var list = await _almoxarifadoServices.ListarTodosMateriaisDesativados();
                return View("Index", list);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public IActionResult NovoMaterial() {
            return View();
        }

        public async Task<IActionResult> EditarMaterial(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorId(id.Value);
                if (material == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(material);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }

        public async Task<IActionResult> DesabilitarMaterial(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorId(id.Value);
                if (material == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (material.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(material);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> HabilitarMaterial(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorId(id.Value);
                if (material == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (material.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View("DesabilitarMaterial", material);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }

        public async Task<IActionResult> DetalhesMaterial(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorId(id.Value);
                if (material == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(material);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> New(Servicos servicos) {
        //    try {
        //        if (!ModelState.IsValid) {
        //            return View(servicos);
        //        }
        //        servicos.Status = Status.Ativado;

        //        servicos = await _servicosService.TransformCaptalizeAsync(servicos);

        //        await _servicosService.SalvarServicosAsync(servicos);
        //        TempData["SuccessMessage"] = "Serviço cadastrado com sucesso";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e) {
        //        TempData["ErrorMessage"] = e.Message;
        //        return RedirectToAction("Index");
        //    }

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Disable(int id) {

        //    try {
        //        Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id);
        //        if (servicos == null) {
        //            TempData["ErrorMessage"] = "ID não encontrado";
        //            return RedirectToAction("Index");
        //        }
        //        if (servicos.Status == Status.Desativado) {
        //            TempData["ErrorMessage"] = "ID não encontrado";
        //            return RedirectToAction("Index");
        //        }
        //        servicos.Status = Status.Desativado;
        //        await _servicosService.AtualizarServicosAsync(servicos);
        //        TempData["SuccessMessage"] = "Serviço desativado com sucesso";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e) {
        //        TempData["ErrorMessage"] = e.Message;
        //        return RedirectToAction("Index");
        //    }


        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Enable(int id) {

        //    try {
        //        Servicos servicos = await _servicosService.EncontrarServicosPorIdAsync(id);
        //        if (servicos == null) {
        //            TempData["ErrorMessage"] = "ID não encontrado";
        //            return RedirectToAction("Index");
        //        }
        //        if (servicos.Status == Status.Ativado) {
        //            TempData["ErrorMessage"] = "ID não encontrado";
        //            return RedirectToAction("Index");
        //        }


        //        servicos.Status = Status.Ativado;
        //        await _servicosService.AtualizarServicosAsync(servicos);
        //        TempData["SuccessMessage"] = "Serviço ativado com sucesso";
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e) {
        //        TempData["ErrorMessage"] = e.Message;
        //        return RedirectToAction("Index");
        //    }


        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Servicos servicos) {
        //    try {
        //        if (!ModelState.IsValid) {
        //            return View(servicos);
        //        }
        //        int id = (int)servicos.Id;
        //        Servicos dbServicos = await _servicosService.EncontrarServicosPorIdAsync(id);
        //        if (dbServicos == null) {
        //            TempData["ErrorMessage"] = "ID não encontrado";
        //            return RedirectToAction("Index");
        //        }

        //        dbServicos.Nome = servicos.Nome;
        //        dbServicos.Valor = servicos.Valor;
        //        dbServicos = await _servicosService.TransformCaptalizeAsync(dbServicos);
        //        await _servicosService.AtualizarServicosAsync(dbServicos);
        //        TempData["SuccessMessage"] = "Serviço alterado com sucesso";

        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e) {
        //        TempData["ErrorMessage"] = e.Message;
        //        return RedirectToAction("Index");
        //    }
        //}

    }
}
