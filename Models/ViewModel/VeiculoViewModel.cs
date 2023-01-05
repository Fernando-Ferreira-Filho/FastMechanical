using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using FastMechanical.Models.Enums;
using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel {
    public class VeiculoViewModel {

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
        public Pessoa Pessoa { get; set; }
        public List<Pessoa> ClienteList { get; set; }
    }
}
