using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastMechanical.Models {
    public class Estoque {

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Material")]
        public Materiais Material { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Quantidade baixada do estoque")]
        public int Baixa { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Quantidade adicionada ao estoque")]
        public int Adicao { get; set; }
        public Pessoa Executor { get; set; }
        public int AtendimentoId { get; set; }


    }
}
