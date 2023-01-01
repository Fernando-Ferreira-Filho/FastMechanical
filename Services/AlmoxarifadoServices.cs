using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class AlmoxarifadoServices : IAlmoxarifadoServices {
        private readonly BancoContext _context;

        public AlmoxarifadoServices(BancoContext context) {
            _context = context;
        }
        //try {
        //     return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Ativado).ToListAsync();
        // }
        // catch (Exception ex) {
        //     throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
        // }

        public async Task<List<Materiais>> ListarTodosMateriaisAtivos() {

            try {
                return await _context.Materiais.Where(m => m.Status == Models.Enums.Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Materiais>> ListarTodosMateriaisDesativados() {

            try {
                return await _context.Materiais.Where(m => m.Status == Models.Enums.Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Materiais> EncontrarMaterialPorId(int id) {

            try {
                return await _context.Materiais.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task AtualizarMaterial(Materiais material) {

            try {
                _context.Materiais.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }
    }
}
