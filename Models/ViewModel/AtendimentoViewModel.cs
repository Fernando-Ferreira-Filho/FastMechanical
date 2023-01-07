using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel {
    public class AtendimentoViewModel {
        public List<Servicos> Servicos { get; set; }
        public List<Materiais> Materiais { get; set; }
        public List<ServicoAtendimento> ServicoAtendimentos { get; set; }
        public List<PecaAtendimento> PecaAtendimentos { get; set; }
        public Agenda Agenda { get; set; }
        public Servicos Servico { get; set; }
        public Materiais Material { get; set; }
        public int Quantidade { get; set; }

    }
}
