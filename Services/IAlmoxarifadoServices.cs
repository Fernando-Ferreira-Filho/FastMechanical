using FastMechanical.Models;
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

    }
}
