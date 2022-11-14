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

            Cliente C1 = new Cliente("FERNANDO FERREIRA FILHO", 62981174693, "FERNANDO.TESTE@GMAIL.COM", "70281834164", "36", "NOSSA SENHORA DA PENHA", "GO", null, "GOIANÉSIA", Status.Ativado, "67", new DateTime(2000, 11, 04));
            Cliente C2 = new Cliente("MARIA ANTONIA DA SILVA", 629876756456, "MARIA@GMAIL.COM", "07503831006", "46", "CARRILHO", "GO", null, "GOIANÉSIA", Status.Ativado, "56", new DateTime(1999, 09, 06));
            Cliente C3 = new Cliente("JOÃO FERNANDES JUNIOR", 62987674565, "JOAOFJ@GMAIL.COM", "43681694095", "34", "CENTRO", "GO", null, "GOIANÉSIA", Status.Ativado, "34", new DateTime(1980, 10, 01));
            Cliente C4 = new Cliente("LUCAS JORGE FERREIRA RIBEIRO", 62987673212, "LJFRIBEIRO@GMAIL.COM", "48117507056", "22", "PARQUE DAS PALMEIRAS", "GO", null, "GOIANÉSIA", Status.Desativado, "12", new DateTime(2000, 10, 15));


            Veiculo V1 = new Veiculo("12348756908", "ATR5344", "GOL", new DateTime(2000, 09, 02), "PRATA", "VOLKSVAGEM", C1);
            Veiculo V2 = new Veiculo("83123379825", "CPK4964", "SAVEIRO", new DateTime(2020, 01, 15), "VERMELHO", "VOLKSVAGEM", C2);
            Veiculo V3 = new Veiculo("07269181960", "HTK0439", "HILUX", new DateTime(2022, 01, 01), "PRETO", "TOYOTA", C3);
            Veiculo V4 = new Veiculo("65199325523", "HPQ8279", "FOX", new DateTime(2021, 09 , 01), "BRANCO", "VOLKSVAGEM", C2);
            Veiculo V5 = new Veiculo("85046149723", "AGF2309", "BROSS", new DateTime(2020, 01, 01), "VERMELHO", "HONDA", C4);
            Veiculo V6 = new Veiculo("00144288028", "OTE2735", "AMAROK", new DateTime(2022, 01, 01), "PRATA", "VOLKSVAGEM", C4);


            Mecanico M1 = new Mecanico("JOSÉ APARECIDO", 62987675435, "JOSE.@OUTLOOK.COM", "69528967086", "29", "CENTRO", "GO", null, "GOIANÉSIA", "23", new DateTime(1980, 10, 01));
            Mecanico M2 = new Mecanico("LEANDRO COSTA SOUSA", 62999786534, "LEANDRO.COSTA@OUTLOOK.COM", "97249951009", "40", "SÃO CRSTOVÃO", "GO", null, "GOIANÉSIA", "343", new DateTime(1980, 10, 01));



            Vendedor S1 = new Vendedor("MARIA LUISA SOUZA", 62983675435, "MARIA.LUISA@OUTLOOK.COM", "05969311073", "22", "CENTRO", "GO", null, "GOIANÉSIA", "23", new DateTime(2000, 09, 30), Status.Ativado);
            Vendedor S2 = new Vendedor("LUAN PEDRO FERREIRA", 62981908753, "LUAN@GMAIL.COM", "40019538030", "12", "SANTA TEREZA", "GO", null, "GOIANÉSIA", "232", new DateTime(1997, 02, 17), Status.Ativado);
            Vendedor S3 = new Vendedor("JOÃO CARLOS PEREIRA", 62987673412, "JOÃO.CP@YAHOO.COM", "39193405073", "30", "SETOR SUL", "GO", null, "GOIANÉSIA", "132", new DateTime(1993, 09, 18), Status.Ativado);


            _context.Cliente.AddRange(C1, C2, C3, C4);
            _context.Veiculo.AddRange(V1, V2, V3, V4, V5, V6);
            _context.Mecanico.AddRange(M1, M2);
            _context.Vendedor.AddRange(S1, S2, S3);



            _context.SaveChanges();
        }

    }
}
