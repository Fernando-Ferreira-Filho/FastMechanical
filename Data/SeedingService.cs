using FastMechanical.Models;
using FastMechanical.Models.Enums;
using System;
using System.Linq;

namespace FastMechanical.Data
{
    public class SeedingService
    {
        private readonly BancoContext _context;
        public SeedingService(BancoContext bancoContext)
        {
            _context = bancoContext;
        }

        public void seed() {
            if (_context.Cliente.Any() || _context.Cliente.Any()){
                return;
            }

            Cliente c1 = new Cliente("Fernando Ferreira Filho",62981174693,"fernando.teste@gmail.com","70281834164","36", "Nossa Senhora Da Penha", "GO",null, "Goianésia",Status.Ativado, "67", new DateTime(2000,11,04));
            Cliente c2 = new Cliente("Maria Antonia da Silva", 629876756456, "maria@gmail.com", "07503831006", "46", "Carrilho", "GO", null, "Goianésia",Status.Ativado, "56", new DateTime(1999, 09, 06));
            Cliente c3 = new Cliente("João Fernandes Junior", 62987674565, "joaofj@gmail.com", "43681694095", "34","Centro", "GO", null, "Goianésia", Status.Ativado, "34", new DateTime(1980, 10, 01));
            

            Veiculo v1 = new Veiculo("12348756908", "ATR5344", "Gol", new DateTime(2000, 09, 02), "Prata", "Volksvagem", c1);
            Veiculo v2 = new Veiculo("83123379825", "CPK4964", "Saveiro", new DateTime(2020, 01, 15), "Vermelho", "volksvagem", c2);
            Veiculo v3 = new Veiculo("07269181960", "HTK0439", "hilux", new DateTime(2022/01/01), "Preto", "Toyota", c3);
            Veiculo v4 = new Veiculo("65199325523", "HPQ8279", "Fox", new DateTime(2021/09/01), "Branco", "Volksvagem", c2);



            _context.Cliente.AddRange(c1,c2,c3);
            _context.Veiculo.AddRange(v1, v2, v3, v4);
            _context.Mecanico.AddRange();
            _context.Vendedor.AddRange();
            _context.SaveChanges();
        }

    }
}
