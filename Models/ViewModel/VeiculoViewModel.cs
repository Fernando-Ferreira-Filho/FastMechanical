using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastMechanical.Models.ViewModel
{
    public class VeiculoViewModel
    {


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

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida favor inserir novamente")]
        [Display(Name = "Ano de Fabricação")]
        public DateTime AnoDeFabricacao { get; set; }
        //pendente fazer

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(25, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Cor")]
        public string Cor { get; set; }
        [Display(Name = "Dono")]
        public Cliente Pessoa { get; set; }
        public List<Cliente> Clientes { get; set; }
        public int PessoaId { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name ="Ano de fabricação")]
        public int Year { get; set; }

    }
}
