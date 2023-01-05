using FastMechanical.Data;
using FastMechanical.Models;
using FastMechanical.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class LoginService : ILoginService {
        private readonly BancoContext _context;
        public LoginService(BancoContext context) {
            _context = context;
        }

        public async Task ChangePasswordAsync(Pessoa person) {
            _context.Pessoa.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Pessoa> FindByLoginAsync(string cpf) {
            return await _context.Pessoa.FirstOrDefaultAsync(x => x.Cpf == cpf);
        }
    }
}
