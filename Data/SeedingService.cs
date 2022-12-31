using PresMed.Models;
using System.Linq;
using System;
using FastMechanical.Models;
using FastMechanical.Models.ViewModel;
using System.Threading.Tasks;

namespace FastMechanical.Data {
    public class SeedingService {
        private readonly BancoContext _context;

        public SeedingService(BancoContext context) {
            _context = context;
        }

        public void Seed() {
            if (_context.Person.Any()) {
                return;
            }

            Person c1 = new Person { Nome = "Loeku Supue", Bairro = "Nossa Senhora da Penha", Cidade = "Goianésia", Complemento = null, Cpf = "51956708030", DataDeNascimento = new DateTime(2000, 12, 01), Email = "loekuSupue@gmail.com", Estado = "Goiás", Numero = "857", Rua = "7", Status = Models.Enums.Status.Ativado, Telefone = 62156390585, TipoPessoa = Models.Enums.TipoPessoa.Cliente };

            Person c2 = new Person { Nome = "Elton Simao", Bairro = "Carrilho", Cidade = "Goianésia", Complemento = null, Cpf = "98180715060", DataDeNascimento = new DateTime(1970, 01, 17), Email = "eltonsimao@gmail.com", Estado = "Goiás", Numero = "12", Rua = "48", Status = Models.Enums.Status.Ativado, Telefone = 62343410323, TipoPessoa = Models.Enums.TipoPessoa.Cliente };

            Person c3 = new Person { Nome = "Joao Martins da Silva", Bairro = "Boa Vista", Cidade = "Goianésia", Complemento = null, Cpf = "63346162001", DataDeNascimento = new DateTime(1997, 05, 10), Email = "joaosilva@gmail.com", Estado = "Goiás", Numero = "01", Rua = "24", Status = Models.Enums.Status.Ativado, Telefone = 62757496316, TipoPessoa = Models.Enums.TipoPessoa.Cliente };


            Veiculo v1 = new Veiculo { AnoDeFabricacao = 1996, Cor = "Amarelo", Marca = "GM - Chevrolet", Modelo = "S10 Blazer DLX 2.2 MPFI / EFI", Placa = "LVJ7375", Renavam = "95641158277", Pessoa = c1, Status = Models.Enums.Status.Ativado };
            Veiculo v2 = new Veiculo { AnoDeFabricacao = 2022, Cor = "Branco", Marca = "Volkswagen", Modelo = "Polo", Placa = "KEI0627", Renavam = "02677894607", Pessoa = c2, Status = Models.Enums.Status.Ativado };
            Veiculo v3 = new Veiculo { AnoDeFabricacao = 2008, Cor = "Dourado", Marca = "Nissan", Modelo = "Frontier SEL CD 4x4 2.5 TB Diesel Aut.", Placa = "HZI2946", Renavam = "12107129462", Pessoa = c3, Status = Models.Enums.Status.Ativado };


            Person vd1 = new Person { Nome = "Buapi Rinan", Bairro = "Universitário", Cidade = "Goianésia", Complemento = null, Cpf = "05788039096", DataDeNascimento = new DateTime(1992, 02, 16), Email = "buapirinan@gmail.com", Estado = "Goiás", Numero = "15", Rua = "9", Status = Models.Enums.Status.Ativado, Telefone = 62811460860, TipoPessoa = Models.Enums.TipoPessoa.Vendedor };


            Person vd2 = new Person { Nome = "Maria Santos Ferreira", Bairro = "Sul", Cidade = "Goianésia", Complemento = null, Cpf = "42097926088", DataDeNascimento = new DateTime(2004, 03, 18), Email = "mariaferreira@gmail.com", Estado = "Goiás", Numero = "10", Rua = "19", Status = Models.Enums.Status.Desativado, Telefone = 62634089444, TipoPessoa = Models.Enums.TipoPessoa.Vendedor };

            Person m1 = new Person { Nome = "Marcos Silva", Bairro = "Morro da Ema", Cidade = "Goianésia", Complemento = null, Cpf = "41360773002", DataDeNascimento = new DateTime(2002, 02, 16), Email = "marcosilva@gmail.com", Estado = "Goiás", Numero = "15", Rua = "17", Status = Models.Enums.Status.Ativado, Telefone = 62651186664, TipoPessoa = Models.Enums.TipoPessoa.Mecanico };


            Person m2 = new Person { Nome = "Fernando Torres Silva", Bairro = "Norte", Cidade = "Goianésia", Complemento = null, Cpf = "04278339062", DataDeNascimento = new DateTime(2000, 11, 04), Email = "fernandotorres@gmail.com", Estado = "Goiás", Numero = "486", Rua = "05", Status = Models.Enums.Status.Ativado, Telefone = 62634089444, TipoPessoa = Models.Enums.TipoPessoa.Mecanico };


            Person admin = new Person { Nome = "Antonio Marcos Pereira", Bairro = "Centro", Cidade = "Goianésia", Complemento = null, Cpf = "51629376060", DataDeNascimento = new DateTime(2000, 11, 04), Email = "antonioPereira@gmail.com", Estado = "Goiás", Numero = "486", Rua = "05", Status = Models.Enums.Status.Ativado, Telefone = 62876954042, TipoPessoa = Models.Enums.TipoPessoa.Administrador };


            _context.Person.AddRange(c1, c2, c3, vd1, vd2, m1, m2, admin);
            _context.Veiculo.AddRange(v1, v2, v3);
            _context.SaveChanges();
        }
    }
}
