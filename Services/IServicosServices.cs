using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IServicosServices {

        public Task SalvarServicosAsync(Servicos servicos);

        public Task<List<Servicos>> TodosServicosAtivosAsync();
        public Task<List<Servicos>> TodosServicosDesativadosAsync();

        public Task<Servicos> EncontrarServicosPorIdAsync(int id);

        public Task AtualizarServicosAsync(Servicos servicos);

        public Task<Servicos> TransformCaptalizeAsync(Servicos servicos);
    }
}
