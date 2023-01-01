using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IAlmoxarifadoServices {

        public Task<List<Materiais>> ListarTodosMateriaisAtivos();
        public Task<List<Materiais>> ListarTodosMateriaisDesativados();
        public Task<Materiais> EncontrarMaterialPorId(int id);
        public Task AtualizarMaterial(Materiais material);

    }
}
