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

        public async Task<Estoque> BuscarMovimentacaoPorIdAsync(int id) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Estoque> BuscarMovimentacaoPorAgendaIdAsync(int id) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).FirstOrDefaultAsync(m => m.Agenda.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task AtualizarMovimentacaoAsync(Estoque estoque) {

            try {
                _context.Estoque.Update(estoque);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Estoque>> BuscarBaixaPorDiaAsync(DateTime data) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).Where(m => m.TipoMovimentacao == TipoMovimentacao.Baixa && m.DataBaixa.Value.Date == data.Date && m.DataExclusao == null).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }


        public async Task<List<Estoque>> BuscarVendaPorDiaAsync(DateTime data) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).Where(m => m.TipoMovimentacao == TipoMovimentacao.Venda && m.DataBaixa.Value.Date == data.Date && m.DataExclusao == null).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Estoque>> BuscarAdicaoPorDiaAsync(DateTime data) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).Where(m => m.TipoMovimentacao == TipoMovimentacao.Adicao && m.DataAdicao.Value.Date == data.Date && m.DataExclusao == null).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Estoque>> BuscarExclusaoPorDiaAsync(DateTime data) {

            try {
                return await _context.Estoque.Include(x => x.Executor).Include(x => x.Material).Where(m => m.TipoMovimentacao == TipoMovimentacao.Exclusao && m.DataInsercaoExclusao.Value.Date == data.Date && m.DataExclusao == null).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }


        public async Task ExcluirMovimentacaoasync(Estoque estoque) {

            try {
                _context.Estoque.Remove(estoque);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }
        public async Task<List<PecaAtendimento>> BuscarMateriaisPorAgendaIdAsync(int id) {

            try {
                return await _context.PecaAtendimento.Include(pa => pa.Material).Where(pa => pa.Agenda.Id == id).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Materiais> TransformCaptalizeAsync(Materiais material) {
            TextInfo myTI = new CultureInfo("pt-BR", false).TextInfo;

            material.Nome = myTI.ToTitleCase(material.Nome).Trim();
            material.Descricao = myTI.ToTitleCase(material.Descricao).Trim();
            material.Codigo = material.Codigo.Trim().ToUpper();

            return material;
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public async Task<List<Estoque>> BuscarVendasPorDatasAsync(DateTime dataInicial, DateTime dataFinal) {
            return await _context.Estoque.Include(m => m.Material).Include(m => m.Executor).Include(m => m.Agenda).Where(e => dataInicial.Date <= e.DataBaixa.Value.Date && dataFinal.Date >= e.DataBaixa.Value.Date && e.TipoMovimentacao == TipoMovimentacao.Venda).ToListAsync();
        }
    }
}
