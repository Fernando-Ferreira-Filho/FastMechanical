using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<List<Materiais>> ListarTodosMateriaisAtivosAsync() {

            try {
                return await _context.Materiais.Where(m => m.Status == Models.Enums.Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Materiais>> ListarTodosMateriaisDesativadosAsync() {

            try {
                return await _context.Materiais.Where(m => m.Status == Models.Enums.Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Materiais> EncontrarMaterialPorIdAsync(int id) {

            try {
                return await _context.Materiais.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task AtualizarMaterialAsync(Materiais material) {

            try {
                _context.Materiais.Update(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task SalvarMaterialAsync(Materiais material) {

            try {
                await _context.Materiais.AddAsync(material);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(material.Codigo)) {
                    throw new Exception($"Houve um erro ao salvar, codigo duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public async Task SalvarMovimentacaoEstoqueAsync(Estoque estoque) {

            try {
                await _context.Estoque.AddAsync(estoque);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Materiais> TransformCaptalizeAsync(Materiais material) {
            TextInfo myTI = new CultureInfo("pt-BR", false).TextInfo;

            material.Nome = myTI.ToTitleCase(material.Nome).Trim();
            material.Descricao = myTI.ToTitleCase(material.Descricao).Trim();
            material.Codigo = material.Codigo.Trim().ToUpper();

            return material;
        }
    }
}
