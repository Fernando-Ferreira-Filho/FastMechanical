using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class MecanicoService : IMecanicoService {
        private readonly BancoContext _context;

        public MecanicoService(BancoContext context) {
            _context = context;
        }
        public async Task<List<Mecanico>> FindAllAsync() {

            try {
                return await _context.Mecanico.ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Mecanico> FindByIdAsync(int id) {
            try {
                return await _context.Mecanico.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task InsertAsync(Mecanico mecanico) {
            try {
                _context.Mecanico.Add(mecanico);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(mecanico.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public Mecanico TransformUpperCase(Mecanico mecanico) {

            mecanico.Nome = mecanico.Nome.Trim().ToUpper();
            mecanico.Email = mecanico.Email.Trim().ToUpper();
            mecanico.Cpf = mecanico.Cpf.Trim().ToUpper();
            mecanico.Rua = mecanico.Rua.Trim().ToUpper();
            mecanico.Bairro = mecanico.Bairro.Trim().ToUpper();
            mecanico.Estado = mecanico.Estado.Trim().ToUpper();
            mecanico.Cidade = mecanico.Cidade.Trim().ToUpper();

            if (mecanico.Numero != null && mecanico.Numero != "") {
                mecanico.Numero = mecanico.Numero.Trim().ToUpper();
            }


            if (mecanico.Complemento != null && mecanico.Complemento != "") {
                mecanico.Complemento = mecanico.Complemento.Trim().ToUpper();
            }

            return mecanico;
        }

        public async Task UpdateAsync(Mecanico mecanico) {
            try {
                bool hasAny = await _context.Mecanico.AnyAsync(x => x.Id == mecanico.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Mecanico.Update(mecanico);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
