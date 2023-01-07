namespace FastMechanical.Models {
    public class PecaAtendimento {
        public int Id { get; set; }

        public Agenda Agenda { get; set; }
        public Materiais Material { get; set; }
        public int Quantidade { get; set; }
    }
}
