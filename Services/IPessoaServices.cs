using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FastMechanical.Models;

namespace FastMechanical.Services {
    public interface IPessoaServices {
        public Task<List<Pessoa>> TodosClientesAtivosAsync();
        public Task<List<Pessoa>> TodosMecanicosAtivosAsync();
        public Task<List<Pessoa>> TodosVendedoresAtivosAsync();
        public Task<List<Pessoa>> TodosAdminAtivosAsync();
        public Task<List<Pessoa>> TodosClientesDesativadosAsync();

        public Task<List<Pessoa>> TodosMecanicosDesativadosAsync();
        public Task<List<Pessoa>> TodosVendedoresDesativadosAsync();
        public Task<List<Pessoa>> TodosAdminDesativadosAsync();
        public Task<Pessoa> BuscarClientePorIdAsync(int id);
        public Task<Pessoa> BuscarMecanicoPorIdAsync(int id);
        public Task<Pessoa> BuscarVendedoresPorIdAsync(int id);
        public Task<Pessoa> BuscarAdminPorIdAsync(int id);
        public Task<Pessoa> BuscarPessoaPorIdAsync(int id);
        public Task<Pessoa> TransformCaptalizeAsync(Pessoa pessoa);
        public Task SalvarAsync(Pessoa pessoa);
        public Task AtualizarAsync(Pessoa pessoa);
    }
}
