using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
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
            try {
                return await _context.Agenda.Include(p => p.Cliente).Include(v => v.Veiculo).Include(s => s.Servico).Include(m => m.Mecanico).Where(a => a.Status != Models.Enums.AgendaStatus.Finalizado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task SalvarAgendaAsync(Agenda agenda) {
            try {
                await _context.Agenda.AddAsync(agenda);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Agenda> BuscarAgendaPorIdAsync(int id) {
            try {
                return await _context.Agenda.Include(p => p.Cliente).Include(v => v.Veiculo).Include(s => s.Servico).Include(m => m.Mecanico).FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task ExcluirAgendaAsync(Agenda agenda) {
            try {
                _context.Agenda.Remove(agenda);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

    }
}
