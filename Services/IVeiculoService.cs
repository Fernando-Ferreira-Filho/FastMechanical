using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IVeiculoService {

        public Task InsertAsync(Veiculo veiculo);

        public Task<List<Veiculo>> FindAllAsync();

        public Task<Veiculo> FindByIdAsync(int id);

        public Task UpdateAsync(Veiculo veiculo);

        public Veiculo TransformUpperCase(Veiculo veiculo);
    }
}
