using FastMechanical.Data;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace FastMechanical.Services {
    public class PersonServices : IPersonServices {

        private readonly BancoContext _context;

        public PersonServices(BancoContext context) {
            _context = context;
        }
        public async Task<List<Person>> TodosClientesAtivosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Ativado).ToListAsync();
        }

        public async Task<List<Person>> TodosMecanicosAtivosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Ativado).ToListAsync();
        }

        public async Task<List<Person>> TodosVendedoresAtivosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Vendedor && c.Status == Status.Ativado).ToListAsync();
        }


        public async Task<List<Person>> TodosClientesDesativadosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Desativado).ToListAsync();
        }

        public async Task<List<Person>> TodosMecanicosDesativadosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Desativado).ToListAsync();
        }

        public async Task<List<Person>> TodosVendedoresDesativadosAsync() {
            return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Desativado).ToListAsync();
        }

        public async Task<Person> BuscarClientePorIdAsync(int id) {
            return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Cliente && i.Id == id);
        }

        public async Task<Person> BuscarMecanicoPorIdAsync(int id) {
            return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Mecanico && i.Id == id);
        }

        public async Task<Person> BuscarVendedoresPorIdAsync(int id) {
            return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Mecanico && i.Id == id);
        }

        public async Task SalvarAsync(Person person) {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarAsync(Person person) {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> TransformCaptalizeAsync(Person person) {

            TextInfo myTI = new CultureInfo("pt-BR", false).TextInfo;
            person.Nome = myTI.ToTitleCase(person.Nome).Trim();
            person.Email = person.Email.ToLower();
            person.Cpf = person.Cpf.ToLower();
            person.Rua = myTI.ToTitleCase(person.Rua).Trim();
            person.Bairro = myTI.ToTitleCase(person.Bairro).Trim();
            person.Estado = person.Estado.Trim().ToUpper();
            if (person.Complemento != null) {
                person.Complemento = myTI.ToTitleCase(person.Complemento).Trim();
            }
            if (person.Numero != null) {
                person.Numero = myTI.ToTitleCase(person.Numero).Trim();
            }
            person.Cidade = myTI.ToTitleCase(person.Cidade).Trim();

            return person;

        }

    }
}
