using System;
using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel {
    public class RelatorioViewModel {

        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public List<Agenda> Agendas { get; set; }
        public Agenda Agenda { get; set; }
    }
}
