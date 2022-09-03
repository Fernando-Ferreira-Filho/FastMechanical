using System;
using System.Collections.Generic;

namespace FastMechanical.Models.ViewModel
{
    public class VeiculoViewModel
    {

        public Veiculo Veiculo { get; set; }
        public List<Cliente> Clientes { get; set; }
        public int PessoaId { get; set; }
    }
}
