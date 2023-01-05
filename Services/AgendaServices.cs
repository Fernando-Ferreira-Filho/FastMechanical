using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class AgendaServices : IAgendaServices {

        private readonly BancoContext _context;

        public AgendaServices(BancoContext context) {
            _context = context;
        }

        public async Task<List<Agenda>> ListarTodasAgendasAtivasAsync() {
            return await _context.Agenda.Include(p => p.Cliente).Include(v => v.Veiculo).Include(s => s.Servico).Where(a => a.Status != Models.Enums.AgendaStatus.Finalizado).ToListAsync();
        }
    }
}
