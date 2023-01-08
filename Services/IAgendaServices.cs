using FastMechanical.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IAgendaServices {

        public Task<List<Agenda>> ListarTodasAgendasAtivasAsync();
        public Task<List<Agenda>> ListarTodasAgendasEmAtendimentoPorMecanicoAsync(Pessoa pessoa);
        public Task SalvarAgendaAsync(Agenda agenda);
        public Task<Agenda> BuscarAgendaPorIdAsync(int id);
        public Task ExcluirAgendaAsync(Agenda agenda);
        public Task AtualizarAgendaAsync(Agenda agenda);
        public Task<List<Agenda>> BuscarAgendaPorDataAsync(DateTime dataInicial, DateTime dataFinal);
    }
}
