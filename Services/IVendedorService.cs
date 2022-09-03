using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IVendedorService {

        public Task InsertAsync(Vendedor vendedor);

        public Task<List<Vendedor>> FindAllActiveAsync();
        public Task<List<Vendedor>> FindAllDisableAsync();

        //public List<Cliente> ListInativos();

        public Task<Vendedor> FindByIdAsync(int id);

        public Task UpdateAsync(Vendedor vendedor);

        public Vendedor TransformUpperCase(Vendedor vendedor);
    }
}
