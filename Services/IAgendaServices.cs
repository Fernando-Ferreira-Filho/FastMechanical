using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IAgendaServices {

        public Task<List<Agenda>> ListarTodasAgendasAtivasAsync();
        public Task SalvarAgendaAsync(Agenda agenda);
        public Task<Agenda> BuscarAgendaPorIdAsync(int id);
        public Task ExcluirAgendaAsync(Agenda agenda);
    }
}
