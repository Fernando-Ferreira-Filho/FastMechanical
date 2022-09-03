using FastMechanical.Data;
using FastMechanical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMechanical.Models.Enums;

namespace FastMechanical.Services {
    public class ClienteService : IClienteService {
        private readonly BancoContext _context;

        public ClienteService(BancoContext context) {
            _context = context;
        }

        public async Task<List<Cliente>> FindAllAsync() {

            try
            {
                return await _context.Cliente.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {ex.Message}");
            }
        }
        // public List<Cliente> ListInativos() {
        //try
        //{
        //    var buscar = _context.Cliente.ToList();
        //  //   return buscar.Where(x => x.Status == Status.Desativado).ToList();
        // }
        // catch (Exception ex) { 
        //     throw new Exception(ex.Message);
        // }
        //}

        public async Task<List<Cliente>> FindAllDisableAsync()
        {

            try
            {
                var list = await _context.Cliente.ToListAsync();
                return list.Where(x => x.Status == Status.Desativado ).ToList();

            }
            catch (Exception e)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }

        public async Task<List<Cliente>> FindAllActiveAsync()
        {

            try
            {
                var list = await _context.Cliente.ToListAsync();
                return list.Where(x => x.Status == Status.Ativado).ToList();
            }
            catch (Exception e)
            {
                throw new Exception($"Houve um erro para listar, ERRO: {e.Message}");
            }
        }


        public async Task<Cliente> FindByIdAsync(int id) {
            try {
                return await _context.Cliente.FirstOrDefaultAsync(obj => obj.Id == id);
            }
            catch (Exception e) {
                throw new Exception($"Houve um erro para encontrar tente mais tarde, ERRO: {e.Message}");
            }
        }

        public async Task InsertAsync(Cliente cliente) {
            try {
                _context.Cliente.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
                if (e.InnerException.Message.Contains(cliente.Cpf)) {
                    throw new Exception($"Houve um erro ao salvar, registro duplicado");
                }
                throw new Exception($"Houve um erro ao salvar, ERRO: {e.InnerException.Message}");
           }
        }

        public Cliente TransformUpperCase(Cliente cliente) {

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

        public async Task UpdateAsync(Cliente cliente) {
            try {
                bool hasAny = await _context.Cliente.AnyAsync(x => x.Id == cliente.Id);

                if (!hasAny) {
                    throw new Exception("ID não encontrado");
                }
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();

            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
