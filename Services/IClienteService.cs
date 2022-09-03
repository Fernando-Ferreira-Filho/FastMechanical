using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IClienteService {

        public Task InsertAsync(Cliente cliente);

        public Task<List<Cliente>> FindAllActiveAsync();
        public Task<List<Cliente>> FindAllDisableAsync();

        //public List<Cliente> ListInativos();

        public Task<Cliente> FindByIdAsync(int id);

        public Task UpdateAsync(Cliente cliente);

        public Cliente TransformUpperCase(Cliente cliente);
    }
}
