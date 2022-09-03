using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMechanical.Models.Enums;

namespace FastMechanical.Services {
    public class VendedorService : IVendedorService {
        private readonly BancoContext _context;

        public VendedorService(BancoContext context) {
            _context = context;
        }
        public async Task<List<Vendedor>> FindAllAsync() {

            try
            {
                var buscar = await _context.Vendedor.ToListAsync();
                return buscar.Where(x => x.Status == Status.Ativado).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }

        public async Task<List<Vendedor>> FindAllDisableAsync()
        {

            try
            {
                var list = await _context.Vendedor.ToListAsync();
                return list.Where(x => x.Status == Status.Desativado).ToList();

            }
            catch (Exception e)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Vendedor>> FindAllActiveAsync()
        {

            try
            {
                var list = await _context.Vendedor.ToListAsync();
                return list.Where(x => x.Status == Status.Ativado).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }


        public async Task<Vendedor> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Vendedor.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task InsertAsync(Vendedor vendedor)
        {
            try
            {
                _context.Vendedor.Add(vendedor);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (e.InnerException.Message.Contains(vendedor.Cpf))
                {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
            }
        }

        public Vendedor TransformUpperCase(Vendedor vendedor)
        {

            vendedor.Nome = vendedor.Nome.Trim().ToUpper();
            vendedor.Email = vendedor.Email.Trim().ToUpper();
            vendedor.Cpf = vendedor.Cpf.Trim().ToUpper();
            vendedor.Rua = vendedor.Rua.Trim().ToUpper();
            vendedor.Bairro = vendedor.Bairro.Trim().ToUpper();
            vendedor.Estado = vendedor.Estado.Trim().ToUpper();
            vendedor.Cidade = vendedor.Cidade.Trim().ToUpper();

            if (vendedor.Numero != null && vendedor.Numero != "")
            {
                vendedor.Numero = vendedor.Numero.Trim().ToUpper();
            }


            if (vendedor.Complemento != null && vendedor.Complemento != "")
            {
                vendedor.Complemento = vendedor.Complemento.Trim().ToUpper();
            }

            return vendedor;
        }

        public async Task UpdateAsync(Vendedor vendedor)
        {
            try
            {
                bool hasAny = await _context.Vendedor.AnyAsync(x => x.Id == vendedor.Id);

                if (!hasAny)
                {
                    throw new Exception("ID não encontrado");
                }
                _context.Vendedor.Update(vendedor);
                await _context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
