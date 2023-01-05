using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel {
    public class AgendaViewModel {
        public List<Agenda> ListaAgenda { get; set; }
        public List<Pessoa> ListaPessoa { get; set; }
        public Pessoa Cliente { get; set; }
        public Pessoa Mecanico { get; set; }
        public Servicos Servico { get; set; }
        public Veiculo Veiculo { get; set; }
        public List<Veiculo> Veiculos { get; set; }
        public List<Servicos> Servicos { get; set; }
        public List<Pessoa> Mecanicos { get; set; }
    }
}
