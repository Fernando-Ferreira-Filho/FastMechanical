using FastMechanical.Data;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class ServicosServices : IServicosServices {
        private readonly BancoContext _context;

        public ServicosServices(BancoContext context) {
            _context = context;
        }

        public async Task<List<Servicos>> TodosServicosAtivosAsync() {

            try {
                return await _context.Servicos.Where(v => v.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Servicos>> TodosServicosDesativadosAsync() {

            try {
                return await _context.Servicos.Where(v => v.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Servicos> EncontrarServicosPorIdAsync(int id) {
            try {
                return await _context.Servicos.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task SalvarServicosAsync(Servicos servicos) {
            try {
                _context.Servicos.Add(servicos);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {

                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public async Task<Servicos> TransformCaptalizeAsync(Servicos servicos) {
            TextInfo myTI = new CultureInfo("pt-BR", false).TextInfo;

            servicos.Nome = myTI.ToTitleCase(servicos.Nome).Trim();

            return servicos;
        }

        public async Task AtualizarServicosAsync(Servicos servicos) {
            try {
                bool hasAny = await _context.Servicos.AnyAsync(x => x.Id == servicos.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Servicos.Update(servicos);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }

    }
}
