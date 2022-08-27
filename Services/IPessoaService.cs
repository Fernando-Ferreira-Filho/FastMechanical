using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IPessoaService {

        public Task InsertAsync(Pessoa cliente);

        public Task<List<Pessoa>> FindAllAsync();

        public Task<Pessoa> FindByIdAsync(int id);

        public Task UpdateAsync(Pessoa cliente);

        public Pessoa TransformUpperCase(Pessoa cliente);
    }
}
