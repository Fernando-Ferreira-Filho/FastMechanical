using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FastMechanical.Models;

namespace FastMechanical.Services {
    public interface IPersonServices {
        public Task<List<Person>> TodosClientesAtivosAsync();
        public Task<List<Person>> TodosMecanicosAtivosAsync();
        public Task<List<Person>> TodosVendedoresAtivosAsync();
        public Task<List<Person>> TodosClientesDesativadosAsync();

        public Task<List<Person>> TodosMecanicosDesativadosAsync();
        public Task<List<Person>> TodosVendedoresDesativadosAsync();
        public Task<Person> BuscarClientePorIdAsync(int id);
        public Task<Person> BuscarMecanicoPorIdAsync(int id);
        public Task<Person> BuscarVendedoresPorIdAsync(int id);
        public Task<Person> TransformCaptalizeAsync(Person person);
        public Task SalvarAsync(Person person);
        public Task AtualizarAsync(Person person);
    }
}
