using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastMechanical.Models.ViewModel {
    public class BaixarEstoqueViewModel {

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Material")]
        public int Material { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Quantidade baixada do estoque")]
        public int Baixa { get; set; }


        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public List<Materiais> ListaMateriais { get; set; }

        public List<Estoque> Estoques { get; set; }
    }
}
