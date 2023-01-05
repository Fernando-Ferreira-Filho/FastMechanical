using FastMechanical.Models.Enums;
using System;

namespace FastMechanical.Models {
    public class Agenda {
        public int Id { get; set; }
        public Pessoa Mecanico { get; set; }
        public Veiculo Veiculo { get; set; }
        public Pessoa Cliente { get; set; }
        public Servicos Servico { get; set; }
        public AgendaStatus Status { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public Agenda() { }

        public Agenda(Pessoa mecanico, Veiculo veiculo, Pessoa cliente, Servicos servico, AgendaStatus status, DateTime dataInicial, DateTime dataFinal) {
            Mecanico = mecanico;
            Veiculo = veiculo;
            Cliente = cliente;
            Servico = servico;
            Status = status;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }
    }
}
