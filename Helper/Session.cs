using FastMechanical.Helper;
using FastMechanical.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace FastMechanical.Helper {
    public class Session : ISessionUser {

        private readonly IHttpContextAccessor _httpContext;

        public Session(IHttpContextAccessor httpContext) {
            _httpContext = httpContext;
        }

        public void createSessionUser(Pessoa pessoa) {
            string value = JsonConvert.SerializeObject(pessoa);
            _httpContext.HttpContext.Session.SetString("sessionLoggedUser", value);
        }

        public Pessoa FindSessionUser() {
            string sessionUser = _httpContext.HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;
            return JsonConvert.DeserializeObject<Pessoa>(sessionUser);
        }

        public void removeSessionUser() {
            _httpContext.HttpContext.Session.Remove("sessionLoggedUser");
        }
    }
}
