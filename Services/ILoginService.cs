using FastMechanical.Models;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public interface ILoginService {

        public Task<Pessoa> FindByLoginAsync(string user);
        public Task ChangePasswordAsync(Pessoa pessoa);

    }
}
