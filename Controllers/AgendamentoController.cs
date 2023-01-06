using FastMechanical.Filters;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FastMechanical.Controllers {
    [PaginaParaAdminEVendedor]
    public class AgendamentoController : Controller {

        private readonly IPessoaServices _personService;
        private readonly IAlmoxarifadoServices _almoxarifadoServices;
        private readonly IAgendaServices _agendaServices;
        private readonly IVeiculoServices _veiculoServices;
        private readonly IServicosServices _servicosServices;

        public AgendamentoController(IPessoaServices personService, IAlmoxarifadoServices almoxarifadoServices, IAgendaServices agendaServices, IVeiculoServices veiculoServices, IServicosServices servicosServices) {
            _personService = personService;
            _almoxarifadoServices = almoxarifadoServices;
            _agendaServices = agendaServices;
            _veiculoServices = veiculoServices;
            _servicosServices = servicosServices;
        }

        public async Task<IActionResult> Index() {
            try {
                var agenda = new AgendaViewModel { ListaAgenda = await _agendaServices.ListarTodasAgendasAtivasAsync(), ListaPessoa = await _personService.TodasPessoasAtivasAsync() };
                return View(agenda);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> Excluir(int? id) {
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

                if (agenda.Status == AgendaStatus.Em_atendimento || agenda.Status == AgendaStatus.Finalizado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View("Excluir", agenda);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agenda(AgendaViewModel agenda) {
            try {
                agenda.Veiculos = await _veiculoServices.BuscarVeiculoPorClienteId(agenda.Cliente.Id);
                agenda.Servicos = await _servicosServices.TodosServicosAtivosAsync();
                agenda.Mecanicos = await _personService.TodosMecanicosAtivosAsync();
                return View(agenda);
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarAgenda(AgendaViewModel agenda) {
            try {

                if (agenda.Veiculo == null || agenda.Servico == null || agenda.Mecanico == null) {
                    TempData["ErrorMessage"] = "Todos os campos devem estar preenchidos";
                    return RedirectToAction("Index");
                }
                Pessoa mecanico = await _personService.BuscarMecanicoPorIdAsync(agenda.Mecanico.Id);
                if (mecanico == null) {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Veiculo veiculo = await _veiculoServices.EncontrarVeiculoPorIdAsync(agenda.Veiculo.Id);
                if (veiculo == null) {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(veiculo.Pessoa.Id);
                if (cliente == null) {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }

                Servicos servico = await _servicosServices.EncontrarServicosPorIdAsync(agenda.Servico.Id);
                if (servico == null) {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Agenda agendaDb = new Agenda { Cliente = cliente, Veiculo = veiculo, Servico = servico, Mecanico = mecanico, Status = Models.Enums.AgendaStatus.Aguardando, DataInicial = DateTime.Now, DataFinal = null };
                await _agendaServices.SalvarAgendaAsync(agendaDb);

                TempData["SuccessMessage"] = "Agenda marcada com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Agenda agenda) {
            try {
                var agendaDb = await _agendaServices.BuscarAgendaPorIdAsync(agenda.Id);
                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agenda.Status == AgendaStatus.Em_atendimento) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agenda.Status == AgendaStatus.Finalizado) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                await _agendaServices.ExcluirAgendaAsync(agendaDb);

                TempData["SuccessMessage"] = "Agenda excluida com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro) {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }
    }
}
