using FastMechanical.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;


namespace FastMechanical.Models.ViewModel {
    public class TrocarSenhaViewModel {
        [PasswordStrongValidation(ErrorMessage = "Senha muito fraca, use letras Maisculas, minusculas e caracteres especiais.")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string Password { get; set; }


        [PasswordStrongValidation(ErrorMessage = "Senha muito fraca, use letras Maisculas, minusculas e caracteres especiais.")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string ConfirmPassword { get; set; }
    }
}