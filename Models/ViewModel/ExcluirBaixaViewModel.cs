using System;
using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel {
    public class ExcluirBaixaViewModel {
        public IEnumerable<Estoque> Movimentacao { get; set; }
        public DateTime Data { get; set; }
    }
}
