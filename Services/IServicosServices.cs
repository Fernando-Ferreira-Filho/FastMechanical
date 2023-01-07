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
        public Task InserirServicoAtendimento(ServicoAtendimento servicoAtendimento);
        public Task<List<ServicoAtendimento>> BuscarServicoAtendimentoPorAtendimento(int id);
        public Task<ServicoAtendimento> BuscarServicoAtendimentoPorIdAsync(int id);
        public Task ExcluirServicoAtendimentoAsync(ServicoAtendimento servico);
        public Task InserirPecaAtendimento(PecaAtendimento servicoAtendimento);
        public Task<List<PecaAtendimento>> BuscarPecaAtendimentoPorAtendimento(int id);
        public Task<PecaAtendimento> BuscarPecaAtendimentoPorIdAsync(int id);
        public Task ExcluirPecaAtendimentoAsync(PecaAtendimento servico);
    }
}
