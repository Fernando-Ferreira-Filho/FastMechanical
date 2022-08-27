using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class PessoaService : IPessoaService {
        private readonly BancoContext _context;

        public PessoaService(BancoContext context) {
            _context = context;
        }

        public async Task<List<Pessoa>> FindAllAsync() {

            try {
                return await _context.Pessoa.ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Pessoa> FindByIdAsync(int id) {
            try {
                return await _context.Pessoa.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task InsertAsync(Pessoa cliente) {
            try {
                _context.Pessoa.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(cliente.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public Pessoa TransformUpperCase(Pessoa cliente) {

            cliente.Nome = cliente.Nome.Trim().ToUpper();
            cliente.Email = cliente.Email.Trim().ToUpper();
            cliente.Cpf = cliente.Cpf.Trim().ToUpper();
            cliente.Rua = cliente.Rua.Trim().ToUpper();
            cliente.Bairro = cliente.Bairro.Trim().ToUpper();
            cliente.Estado = cliente.Estado.Trim().ToUpper();
            cliente.Cidade = cliente.Cidade.Trim().ToUpper();

            if (cliente.Numero != null && cliente.Numero != "") {
                cliente.Numero = cliente.Numero.Trim().ToUpper();
            }


            if (cliente.Complemento != null && cliente.Complemento != "") {
                cliente.Complemento = cliente.Complemento.Trim().ToUpper();
            }

            return cliente;
        }

        public async Task UpdateAsync(Pessoa cliente) {
            try {
                bool hasAny = await _context.Pessoa.AnyAsync(x => x.Id == cliente.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Pessoa.Update(cliente);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
