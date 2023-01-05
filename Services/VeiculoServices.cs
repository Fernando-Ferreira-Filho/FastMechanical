using FastMechanical.Data;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
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

        public async Task<List<Veiculo>> TodosVeiculosAtivosAsync() {

            try {
                return await _context.Veiculo.Include(v => v.Pessoa).Where(v => v.Status == Status.Ativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Veiculo>> TodosVeiculosDesativadosAsync() {

            try {
                return await _context.Veiculo.Include(v => v.Pessoa).Where(v => v.Status == Status.Desativado).ToListAsync();
            }
            catch (Exception ex) {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<Veiculo> EncontrarVeiculoPorIdAsync(int id) {
            try {
                return await _context.Veiculo.Include(v => v.Pessoa).FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task SalvarVeiculoAsync(Veiculo veiculo) {
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

        public Veiculo TransformCaptalizeAsync(Veiculo veiculo) {

            veiculo.Renavam = veiculo.Renavam.Trim().ToUpper();
            veiculo.Placa = veiculo.Placa.Trim().ToUpper();
            veiculo.Modelo = veiculo.Modelo.Trim().ToUpper();
            veiculo.Cor = veiculo.Cor.Trim().ToUpper();
            veiculo.Marca = veiculo.Marca.Trim().ToUpper();

            return veiculo;
        }

        public async Task AtualizarVeiculoAsync(Veiculo veiculo) {
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

        public async Task<List<Veiculo>> BuscarVeiculoPorClienteId(int id) {
            return await _context.Veiculo.Include(v => v.Pessoa).Where(v => v.Pessoa.Id == id).ToListAsync();
        }
    }
}
