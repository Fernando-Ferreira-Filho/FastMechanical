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
    public class PessoaServices : IPessoaServices {

        private readonly BancoContext _context;

        public PessoaServices(BancoContext context) {
            _context = context;
        }
        public async Task<List<Pessoa>> TodosClientesAtivosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosMecanicosAtivosAsync() {
            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosVendedoresAtivosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Vendedor && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosAdminAtivosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Administrador && c.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }


        public async Task<List<Pessoa>> TodosClientesDesativadosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Cliente && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosMecanicosDesativadosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Mecanico && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosVendedoresDesativadosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Vendedor && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<List<Pessoa>> TodosAdminDesativadosAsync() {

            try {
                return await _context.Pessoa.Where(c => c.TipoPessoa == TipoPessoa.Administrador && c.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Pessoa> BuscarClientePorIdAsync(int id) {

            try {
                return await _context.Pessoa.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Cliente && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Pessoa> BuscarMecanicoPorIdAsync(int id) {

            try {
                return await _context.Pessoa.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Mecanico && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Pessoa> BuscarVendedoresPorIdAsync(int id) {

            try {
                return await _context.Pessoa.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Vendedor && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Pessoa> BuscarAdminPorIdAsync(int id) {

            try {
                return await _context.Pessoa.FirstOrDefaultAsync(i => i.TipoPessoa == TipoPessoa.Administrador && i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task<Pessoa> BuscarPessoaPorIdAsync(int id) {
            try {
                return await _context.Pessoa.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }

        }

        public async Task SalvarAsync(Pessoa person) {

            try {
                await _context.Pessoa.AddAsync(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(person.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }


        }

        public async Task AtualizarAsync(Pessoa person) {
            try {
                _context.Pessoa.Update(person);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }

        }

        public async Task<Pessoa> TransformCaptalizeAsync(Pessoa person) {

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
