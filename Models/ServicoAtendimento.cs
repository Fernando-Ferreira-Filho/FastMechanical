namespace FastMechanical.Models {
    public class ServicoAtendimento {
        public int Id { get; set; }

        public Agenda Agenda { get; set; }
        public Servicos Servico { get; set; }
    }
}
