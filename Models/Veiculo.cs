using PresMed.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;
using System;
using FastMechanical.Models.ViewModel;
using FastMechanical.Models.Enums;

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
        [MinLength(5, ErrorMessage = "Campo invalido ")]
        [MaxLength(255, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(30, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Ano de Fabricação")]
        public int AnoDeFabricacao { get; set; }
        //pendente fazer

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(25, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Cor")]
        public string Cor { get; set; }

        public Status Status { get; set; }
        public Person Pessoa { get; set; }

        public Veiculo() { }
        public Veiculo(string renavam, string placa, string modelo, int anoDeFabricacao, string cor, String marca, Person pessoa) {
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
