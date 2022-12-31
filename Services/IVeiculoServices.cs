using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IVeiculoServices {

        public Task SalvarVeiculoAsync(Veiculo veiculo);

        public Task<List<Veiculo>> TodosVeiculosAtivosAsync();
        public Task<List<Veiculo>> TodosVeiculosDesativadosAsync();

        public Task<Veiculo> EncontrarVeiculoPorIdAsync(int id);

        public Task AtualizarVeiculoAsync(Veiculo veiculo);

        public Veiculo TransformCaptalizeAsync(Veiculo veiculo);
        public Task<List<Veiculo>> BuscarVeiculoPorClienteId(int id);
    }
}
