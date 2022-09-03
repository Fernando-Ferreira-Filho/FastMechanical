using PresMed.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;
using System;

namespace FastMechanical.Models {
    public class Veiculo {

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(11, ErrorMessage = "Campo invalido ")]
        [MaxLength(11, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Renavam")]
        public string Renavam { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(7, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(20, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(30, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        public DateTime AnoDeFabricacao { get; set; }
        //pendente fazer

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(25, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Cor")]
        public string Cor { get; set; }
        [Display(Name = "Dono" )]
        public Cliente Pessoa { get; set; }

        public Veiculo() { }
        public Veiculo(string renavam, string placa, string modelo, DateTime anoDeFabricacao, string cor, String marca, Cliente pessoa) {
            Renavam = renavam;
            Placa = placa;
            Modelo = modelo;
            AnoDeFabricacao = anoDeFabricacao;
            Cor = cor;
            Marca = marca;
            Pessoa = pessoa;
        }
    }
}
