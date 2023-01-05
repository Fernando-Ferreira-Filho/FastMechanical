
using FastMechanical.Models;

namespace FastMechanical.Helper {
    public interface ISessionUser {
        void createSessionUser(Pessoa pessoa);
        void removeSessionUser();
        Pessoa FindSessionUser();
    }
}
