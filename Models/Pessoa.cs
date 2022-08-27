using System;
using System.ComponentModel.DataAnnotations;

namespace FastMechanical.Models {
    public class Pessoa {
        public int Id { get; set; }

        public string Nome { get; set; }
        public long? Telefone { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public DateTime? DataDeNascimento { get; set; }

        public Pessoa() { }

        public Pessoa(string nome, long? telefone, string email, string cpf, string rua, string bairro, string estado, string complemento, string cidade, string numero, DateTime? dataDeNascimento) {
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
