using FastMechanical.Data;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System;

namespace FastMechanical.Services {
    public class PersonServices : IPersonServices {

        private readonly BancoContext _context;

        public PersonServices(BancoContext context) {
            _context = context;
        }
        public async Task<List<Person>> TodosClientesAtivosAsync() {

            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Person>> TodosMecanicosAtivosAsync() {
            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Person>> TodosVendedoresAtivosAsync() {

            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Vendedor && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }


        public async Task<List<Person>> TodosClientesDesativadosAsync() {

            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Person>> TodosMecanicosDesativadosAsync() {

            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Person>> TodosVendedoresDesativadosAsync() {

            try {
                return await _context.Person.Where(c => c.TipoPessoa == TipoPessoa.Vendedor && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Person> BuscarClientePorIdAsync(int id) {

            try {
                return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Cliente && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Person> BuscarMecanicoPorIdAsync(int id) {

            try {
                return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Mecanico && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Person> BuscarVendedoresPorIdAsync(int id) {

            try {
                return await _context.Person.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Vendedor && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task SalvarAsync(Person person) {

            try {
                await _context.Person.AddAsync(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(person.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }


        }

        public async Task AtualizarAsync(Person person) {
            try {
                _context.Person.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }

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
