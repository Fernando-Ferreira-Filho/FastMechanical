using FastMechanical.Filters;
using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Controllers {
    [PaginaParaUsuarioLogado]
    [PaginaParaMecanico]
    public class AtendimentoController : Controller {

        private readonly IPessoaServices _personService;
        private readonly IAlmoxarifadoServices _almoxarifadoServices;
        private readonly IAgendaServices _agendaServices;
        private readonly IVeiculoServices _veiculoServices;
        private readonly IServicosServices _servicosServices;

        public AtendimentoController(IPessoaServices personService, IAlmoxarifadoServices almoxarifadoServices, IAgendaServices agendaServices, IVeiculoServices veiculoServices, IServicosServices servicosServices) {
            _personService = personService;
            _almoxarifadoServices = almoxarifadoServices;
            _agendaServices = agendaServices;
            _veiculoServices = veiculoServices;
            _servicosServices = servicosServices;
        }
        public async Task<IActionResult> Index() {

            try {
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                List<Agenda> agenda = await _agendaServices.ListarTodasAgendasEmAtendimentoPorMecanicoAsync(pessoa);

                return View(agenda);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> IniciarAtendimento(int? id) {

            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);
                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                agenda.Status = Models.Enums.AgendaStatus.Em_atendimento;
                await _agendaServices.AtualizarAgendaAsync(agenda);
                return RedirectToAction("Index");
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }



        }

        public async Task<IActionResult> Servicos(int? id) {

            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);
                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                AtendimentoViewModel atendimento = new AtendimentoViewModel { Agenda = agenda, Servicos = await _servicosServices.TodosServicosAtivosAsync(), ServicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id) };

                return View(atendimento);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> ApagarServico(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                ServicoAtendimento servico = await _servicosServices.BuscarServicoAtendimentoPorIdAsync(id.Value);
                if (servico == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);
                if (pessoa.Id != servico.Agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                await _servicosServices.ExcluirServicoAtendimentoAsync(servico);

                AtendimentoViewModel atendimento = new AtendimentoViewModel { Agenda = servico.Agenda, Servicos = await _servicosServices.TodosServicosAtivosAsync(), ServicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(servico.Agenda.Id) };

                return View("Servicos", atendimento);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }
        }


        public async Task<IActionResult> Pecas(int? id) {

            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);
                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                AtendimentoViewModel atendimento = new AtendimentoViewModel { Agenda = agenda, Materiais = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync(), PecaAtendimentos = await _servicosServices.BuscarPecaAtendimentoPorAtendimento(agenda.Id) };
                return View(atendimento);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }
        }

        public async Task<IActionResult> ApagarPeca(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                PecaAtendimento pecaAtendimento = await _servicosServices.BuscarPecaAtendimentoPorIdAsync(id.Value);
                if (pecaAtendimento == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != pecaAtendimento.Agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                AtendimentoViewModel atendimentoViewModel = new AtendimentoViewModel { Agenda = pecaAtendimento.Agenda, Materiais = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync() };
                Materiais materialDb = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(pecaAtendimento.Material.Id);
                materialDb.Quantidade += pecaAtendimento.Quantidade;

                var EstoqueDb = await _almoxarifadoServices.BuscarMovimentacaoPorAgendaIdAsync(pecaAtendimento.Agenda.Id);

                await _almoxarifadoServices.ExcluirMovimentacaoasync(EstoqueDb);
                await _almoxarifadoServices.AtualizarMaterialAsync(materialDb);
                await _servicosServices.ExcluirPecaAtendimentoAsync(pecaAtendimento);
                atendimentoViewModel.PecaAtendimentos = await _servicosServices.BuscarPecaAtendimentoPorAtendimento(pecaAtendimento.Agenda.Id);
                return View("Pecas", atendimentoViewModel);

            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }
        }

        public async Task<IActionResult> Finalizar(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);

                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agenda.Status != Models.Enums.AgendaStatus.Em_atendimento) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                AtendimentoViewModel atendimento = new AtendimentoViewModel { Agenda = agenda, PecaAtendimentos = await _almoxarifadoServices.BuscarMateriaisPorAgendaIdAsync(agenda.Id), ServicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id) };

                return View(atendimento);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AtendimentoViewModel atendimento) {

            try {
                var agenda = await _agendaServices.BuscarAgendaPorIdAsync(atendimento.Agenda.Id);
                var servico = await _servicosServices.EncontrarServicosPorIdAsync(atendimento.Servico.Id);

                ServicoAtendimento servicoAtendimento = new ServicoAtendimento { Agenda = agenda, Servico = servico };
                await _servicosServices.InserirServicoAtendimento(servicoAtendimento);

                AtendimentoViewModel atendimentoViewModel = new AtendimentoViewModel { Agenda = agenda, Servicos = await _servicosServices.TodosServicosAtivosAsync(), ServicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id) };

                return View("Servicos", atendimentoViewModel);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPeca(AtendimentoViewModel atendimento) {
            try {
                var agenda = await _agendaServices.BuscarAgendaPorIdAsync(atendimento.Agenda.Id);
                var material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(atendimento.Material.Id);
                var estoque = material.Quantidade - atendimento.Quantidade;

                PecaAtendimento peca = new PecaAtendimento { Agenda = agenda, Material = material, Quantidade = atendimento.Quantidade };

                AtendimentoViewModel atendimentoViewModel = new AtendimentoViewModel { Agenda = agenda, Materiais = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync() };
                atendimentoViewModel.PecaAtendimentos = await _servicosServices.BuscarPecaAtendimentoPorAtendimento(agenda.Id);

                if (estoque < 0) {
                    TempData["ErrorMessage"] = $"Estoque insuficiente, estoque do material {material.Nome} tem apenas {material.Quantidade} de estoque";
                    return View("Pecas", atendimentoViewModel);
                }
                if (atendimento.Quantidade <= 0) {
                    TempData["ErrorMessage"] = $"Peça não pode ser zerada";
                    return View("Pecas", atendimentoViewModel);
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                Pessoa pessoaDb = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                material.Quantidade = estoque;

                Estoque estoqueDb = new Estoque { Agenda = agenda, Baixa = atendimento.Quantidade, DataBaixa = DateTime.Now, Executor = pessoaDb, TipoMovimentacao = Models.Enums.TipoMovimentacao.Atendimento, Adicao = 0, ChaveAcessoNotaFiscal = null, DataAdicao = null, DataExclusao = null, DataInsercaoExclusao = null, Material = material, NumeroNotaFiscal = null, Observacao = null };

                await _almoxarifadoServices.SalvarMovimentacaoEstoqueAsync(estoqueDb);
                await _almoxarifadoServices.AtualizarMaterialAsync(material);
                await _servicosServices.InserirPecaAtendimento(peca);
                atendimentoViewModel.PecaAtendimentos = await _servicosServices.BuscarPecaAtendimentoPorAtendimento(agenda.Id);
                return View("Pecas", atendimentoViewModel);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View("Pecas");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarAtendimento(AtendimentoViewModel atendimento) {

            try {
                var agenda = await _agendaServices.BuscarAgendaPorIdAsync(atendimento.Agenda.Id);

                if (agenda == null) {
                    TempData["ErrorMessage"] = $"ID não econtrado";
                    return View("Index");
                }
                if (agenda.Status != Models.Enums.AgendaStatus.Em_atendimento) {
                    TempData["ErrorMessage"] = $"ID não econtrado";
                    return View("Index");
                }

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                if (pessoa.Id != agenda.Mecanico.Id) {
                    TempData["ErrorMessage"] = $"ID não econtrado";
                    return View("Index");
                }

                agenda.Status = Models.Enums.AgendaStatus.Aguardando_pagamento;

                await _agendaServices.AtualizarAgendaAsync(agenda);
                TempData["SuccessMessage"] = "Atendimento finalizado";
                return View("Index");
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }
    }
}
