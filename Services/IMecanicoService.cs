using FastMechanical.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface IMecanicoService {

        public Task InsertAsync(Mecanico mecanico);

        public Task<List<Mecanico>> FindAllAsync();

        public Task<Mecanico> FindByIdAsync(int id);

        public Task UpdateAsync(Mecanico mecanico);

        public Mecanico TransformUpperCase(Mecanico mecanico);
    }
}
