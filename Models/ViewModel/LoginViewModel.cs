using System.ComponentModel.DataAnnotations;

namespace FastMechanical.Models.ViewModel {
    public class LoginViewModel {
        [Required(ErrorMessage = "Digite o cpf")]
        [Display(Name = "Cpf")]
        public string User { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}
