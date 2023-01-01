using FastMechanical.Models.Enums;
using FastMechanical.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;
using System;

namespace FastMechanical.Models {
    public class Pessoa {

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo inválido ")]
        [MaxLength(50, ErrorMessage = "Campo inválido ")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Campo inválido")]
        [Display(Name = "Telefone")]
        public long? Telefone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Campo inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [MaxLength(11, ErrorMessage = "Campo invalido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [CpfValidation(ErrorMessage = "Campo inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "Rua")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(40, ErrorMessage = "Campo inválido")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "UF")]
        public string Estado { get; set; }
        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(40, ErrorMessage = "Campo inválido")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [MinLength(1, ErrorMessage = "Campo inválido")]
        [MaxLength(7, ErrorMessage = "Campo inválido")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter mais de 18 anos e menos de 130 anos")]
        public DateTime? DataDeNascimento { get; set; }
        public Status Status { get; set; }

        public TipoPessoa TipoPessoa { get; set; }

        public Pessoa() { }

        public Pessoa(string nome, long? telefone, string email, string cpf, string rua, string bairro, string estado, string complemento, string cidade, Status status, string numero, DateTime? dataDeNascimento, TipoPessoa tipoPessoa) {
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
            Status = status;
            TipoPessoa = tipoPessoa;
        }

    }
}
