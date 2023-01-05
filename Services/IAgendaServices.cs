using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IAgendaServices {

        public Task<List<Agenda>> ListarTodasAgendasAtivasAsync();
    }
}
