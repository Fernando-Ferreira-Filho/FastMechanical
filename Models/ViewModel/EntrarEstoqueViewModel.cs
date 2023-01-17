using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastMechanical.Models.ViewModel {
    public class EntrarEstoqueViewModel {

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Material")]
        public int Material { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Quantidade adicionada ao estoque")]
        public int Adicao { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [MinLength(44, ErrorMessage = "Campo inválido ")]
        [MaxLength(44, ErrorMessage = "Campo invalido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Chave de acesso da nota fiscal")]
        public string ChaveAcessoNotaFiscal { get; set; }


        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Numero da nota fiscal")]
        public string NumeroNotaFiscal { get; set; }

        public List<Materiais> ListaMateriais { get; set; }

        public List<Estoque> Estoques { get; set; }


    }

}

