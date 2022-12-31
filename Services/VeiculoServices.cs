using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMechanical.Services {
    public class VeiculoServices : IVeiculoServices {
        private readonly BancoContext _context;

        public VeiculoServices(BancoContext context) {
            _context = context;
        }

        public async Task<List<Veiculo>> FindAllAsync() {

            try {
                return await _context.Veiculo.ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Veiculo> FindByIdAsync(int id) {
            try {
                return await _context.Veiculo.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task InsertAsync(Veiculo veiculo) {
            try {
                _context.Veiculo.Add(veiculo);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(veiculo.Renavam)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public Veiculo TransformUpperCase(Veiculo veiculo) {

            veiculo.Renavam = veiculo.Renavam.Trim().ToUpper();
            veiculo.Placa = veiculo.Placa.Trim().ToUpper();
            veiculo.Modelo = veiculo.Modelo.Trim().ToUpper();
            veiculo.Cor = veiculo.Cor.Trim().ToUpper();
            veiculo.Marca = veiculo.Marca.Trim().ToUpper();

            //if (veiculo.Numero != null && cliente.Numero != "") {
            //     veiculo.Numero = cliente.Numero.Trim().ToUpper();
            // }


            // if (cliente.Complemento != null && cliente.Complemento != "") {
            //     cliente.Complemento = cliente.Complemento.Trim().ToUpper();
            //}

            return veiculo;
        }

        public async Task UpdateAsync(Veiculo veiculo) {
            try {
                bool hasAny = await _context.Veiculo.AnyAsync(x => x.Id == veiculo.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Veiculo.Update(veiculo);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
