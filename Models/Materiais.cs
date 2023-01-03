using FastMechanical.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastMechanical.Models {
    public class Materiais {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Unidade de medida")]
        public TipoUnidadeMedidade UnidadeMedidade { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Valor de custo")]
        public double ValorCusto { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Porcentagem de lucro")]
        public double PorcentagemLucro { get; set; }


        public Materiais() { }

        public Materiais(string nome, string codigo, string descricao, int quantidade, Status status, TipoUnidadeMedidade unidadeMedidade, double valorCusto, double porcentagemLucro) {
            Nome = nome;
            Codigo = codigo;
            Descricao = descricao;
            Quantidade = quantidade;
            Status = status;
            UnidadeMedidade = unidadeMedidade;
            ValorCusto = valorCusto;
            PorcentagemLucro = porcentagemLucro;
        }
    }
}
