using FastMechanical.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IAlmoxarifadoServices {

        public Task<List<Materiais>> ListarTodosMateriaisAtivosAsync();
        public Task<List<Materiais>> ListarTodosMateriaisDesativadosAsync();
        public Task<Materiais> EncontrarMaterialPorIdAsync(int id);
        public Task AtualizarMaterialAsync(Materiais material);
        public Task SalvarMaterialAsync(Materiais material);
        public Task<Materiais> TransformCaptalizeAsync(Materiais material);
        public Task SalvarMovimentacaoEstoqueAsync(Estoque estoque);
        public Task<List<Estoque>> BuscarBaixaPorDiaAsync(DateTime data);
        public Task<List<Estoque>> BuscarVendaPorDiaAsync(DateTime data);
        public Task<List<Estoque>> BuscarAdicaoPorDiaAsync(DateTime data);
        public Task<List<Estoque>> BuscarExclusaoPorDiaAsync(DateTime data);
        public Task<Estoque> BuscarMovimentacaoPorIdAsync(int id);
        public Task<Estoque> BuscarMovimentacaoPorAgendaIdAsync(int id);
        public Task<List<PecaAtendimento>> BuscarMateriaisPorAgendaIdAsync(int id);
        public Task AtualizarMovimentacaoAsync(Estoque estoque);
        public Task ExcluirMovimentacaoasync(Estoque estoque);
    }
}
