using FastMechanical.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FastMechanical.Models {
    public class Estoque {

        public int Id { get; set; }
        public Materiais Material { get; set; }
        public int Baixa { get; set; }
        public int Adicao { get; set; }
        public string Observacao { get; set; }
        public Pessoa Executor { get; set; }
        public Agenda Agenda { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public string ChaveAcessoNotaFiscal { get; set; }
        public DateTime? DataBaixa { get; set; }
        public DateTime? DataAdicao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public DateTime? DataInsercaoExclusao { get; set; }
        public int IdMovimentacao { get; set; }

        public TipoMovimentacao TipoMovimentacao { get; set; }
        public Estoque() {
        }

        public Estoque(Materiais material, int baixa, int adicao, string observacao, Pessoa executor, Agenda agenda, string numeroNotaFiscal, string chaveAcessoNotaFiscal, DateTime? dataBaixa, DateTime? dataAdicao, DateTime? dataExclusao, DateTime? dataInsercaoExclusao, int idMovimentacao, TipoMovimentacao tipoMovimentacao) {
            Material = material;
            Baixa = baixa;
            Adicao = adicao;
            Observacao = observacao;
            Executor = executor;
            Agenda = agenda;
            NumeroNotaFiscal = numeroNotaFiscal;
            ChaveAcessoNotaFiscal = chaveAcessoNotaFiscal;
            DataBaixa = dataBaixa;
            DataAdicao = dataAdicao;
            DataExclusao = dataExclusao;
            DataInsercaoExclusao = dataInsercaoExclusao;
            IdMovimentacao = idMovimentacao;
            TipoMovimentacao = tipoMovimentacao;
        }
    }
}
