using FastMechanical.Filters;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using Microsoft.AspNetCore.Mvc;
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
            var agenda = new AgendaViewModel { ListaAgenda = await _agendaServices.ListarTodasAgendasAtivasAsync(), ListaPessoa = await _personService.TodasPessoasAtivasAsync() };
            return View(agenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agenda(AgendaViewModel agenda) {
            agenda.Veiculos = await _veiculoServices.BuscarVeiculoPorClienteId(agenda.Cliente.Id);
            agenda.Servicos = await _servicosServices.TodosServicosAtivosAsync();
            agenda.Mecanicos = await _personService.TodosMecanicosAtivosAsync();
            return View(agenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarAgenda(AgendaViewModel agenda) {
            agenda.Veiculos = await _veiculoServices.BuscarVeiculoPorClienteId(agenda.Cliente.Id);
            agenda.Servicos = await _servicosServices.TodosServicosAtivosAsync();
            agenda.Mecanicos = await _personService.TodosMecanicosAtivosAsync();
            return View(agenda);
        }
    }
}
