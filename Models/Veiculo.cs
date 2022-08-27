using System;

namespace FastMechanical.Models {
    public class Veiculo {

        public int Id { get; set; }
        public string Renavam { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public DateTime AnoDeFabricacao { get; set; }
        public string Cor { get; set; }

        public Pessoa Pessoa { get; set; }

        public Veiculo() { }
        public Veiculo(string renavam, string placa, string modelo, DateTime anoDeFabricacao, string cor, Pessoa pessoa) {
            Renavam = renavam;
            Placa = placa;
            Modelo = modelo;
            AnoDeFabricacao = anoDeFabricacao;
            Cor = cor;
            Pessoa = pessoa;
        }
    }
}
