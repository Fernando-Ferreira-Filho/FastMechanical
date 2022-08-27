using PresMed.Models.ValidationModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace FastMechanical.Models {
    public class Cliente {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(50, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Campo invalido")]
        [Display(Name = "Telefone")]
        public long? Telefone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Campo invalido")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [CpfValidation(ErrorMessage = "Campo invalido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "Rua")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(40, ErrorMessage = "Campo invalido")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "UF")]
        public string Estado { get; set; }
        [MinLength(4, ErrorMessage = "Campo invalido")]
        [MaxLength(40, ErrorMessage = "Campo invalido")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [MinLength(1, ErrorMessage = "Campo invalido")]
        [MaxLength(7, ErrorMessage = "Campo invalido")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data invalida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter mais de 18 anos e menos de 130 anos")]
        public DateTime? DataDeNascimento { get; set; }

        public Cliente() { }

        public Cliente(string nome, long? telefone, string email, string cpf, string rua, string bairro, string estado, string complemento, string cidade, string numero, DateTime? dataDeNascimento) {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Cpf = cpf;
            Rua = rua;
            Bairro = bairro;
            Estado = estado;
            Complemento = complemento;
            Cidade = cidade;
            Numero = numero;
            DataDeNascimento = dataDeNascimento;
        }
    }
}
