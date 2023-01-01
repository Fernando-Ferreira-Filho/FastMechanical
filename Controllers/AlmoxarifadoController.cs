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
                var list = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync();
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
                var list = await _almoxarifadoServices.ListarTodosMateriaisDesativadosAsync();
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
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(id.Value);
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
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(id.Value);
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
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(id.Value);
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
                Materiais material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(id.Value);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoMaterial(Materiais materiais) {
            try {
                if (!ModelState.IsValid) {
                    return View(materiais);
                }
                materiais.Status = Status.Ativado;
                materiais.Quantidade = 0;
                string str = $"{materiais.PorcentagemLucro}";
                if (!str.Contains(".") || !str.Contains(",")) {
                    materiais.PorcentagemLucro /= 100;
                }
                materiais = await _almoxarifadoServices.TransformCaptalizeAsync(materiais);

                await _almoxarifadoServices.SalvarMaterialAsync(materiais);
                TempData["SuccessMessage"] = "Material cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesabilitarMaterial(Materiais materiais) {

            try {
                materiais = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(materiais.Id);
                if (materiais == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (materiais.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (materiais.Quantidade != 0) {
                    TempData["ErrorMessage"] = "Para desativar um material o estoque deve estar zerado";
                    return RedirectToAction("Index");
                }
                materiais.Status = Status.Desativado;
                await _almoxarifadoServices.AtualizarMaterialAsync(materiais);
                TempData["SuccessMessage"] = "Material desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HabilitarMaterial(Materiais materiais) {

            try {
                materiais = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(materiais.Id);
                if (materiais == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (materiais.Status == Status.Ativado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                materiais.Status = Status.Ativado;
                await _almoxarifadoServices.AtualizarMaterialAsync(materiais);
                TempData["SuccessMessage"] = "Material ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarMaterial(Materiais materiais) {
            try {
                if (!ModelState.IsValid) {
                    return View(materiais);
                }

                Materiais dbMateriais = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(materiais.Id);
                if (dbMateriais == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                string str = $"{materiais.PorcentagemLucro}";
                if (!str.Contains(".") || !str.Contains(",")) {
                    materiais.PorcentagemLucro /= 100;
                }
                dbMateriais.Nome = materiais.Nome;
                dbMateriais.PorcentagemLucro = materiais.PorcentagemLucro;
                dbMateriais.UnidadeMedidade = materiais.UnidadeMedidade;
                dbMateriais.Descricao = materiais.Descricao;
                dbMateriais.ValorCusto = materiais.ValorCusto;

                dbMateriais = await _almoxarifadoServices.TransformCaptalizeAsync(dbMateriais);
                await _almoxarifadoServices.AtualizarMaterialAsync(dbMateriais);
                TempData["SuccessMessage"] = "Material alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
