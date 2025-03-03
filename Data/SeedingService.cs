﻿using System.Linq;
using System;
using FastMechanical.Models;
using FastMechanical.Models.Enums;


namespace FastMechanical.Data {
    public class SeedingService {
        private readonly BancoContext _context;

        public SeedingService(BancoContext context) {
            _context = context;
        }

        public void Seed() {
            if (_context.Pessoa.Any()) {
                return;
            }

            Pessoa c1 = new Pessoa { Nome = "Loeku Supue", Bairro = "Nossa Senhora da Penha", Cidade = "Goianésia", Complemento = null, Cpf = "51956708030", DataDeNascimento = new DateTime(2000, 12, 01), Email = "loekuSupue@gmail.com", Estado = "Goiás", Numero = "857", Rua = "7", Status = Status.Ativado, Telefone = 62156390585, TipoPessoa = TipoPessoa.Cliente, Password = null };
            Pessoa c2 = new Pessoa { Nome = "Elton Simao", Bairro = "Carrilho", Cidade = "Goianésia", Complemento = null, Cpf = "98180715060", DataDeNascimento = new DateTime(1970, 01, 17), Email = "eltonsimao@gmail.com", Estado = "Goiás", Numero = "12", Rua = "48", Status = Status.Ativado, Telefone = 62343410323, TipoPessoa = TipoPessoa.Cliente, Password = null };
            Pessoa c3 = new Pessoa { Nome = "Joao Martins da Silva", Bairro = "Boa Vista", Cidade = "Goianésia", Complemento = null, Cpf = "63346162001", DataDeNascimento = new DateTime(1997, 05, 10), Email = "joaosilva@gmail.com", Estado = "Goiás", Numero = "01", Rua = "24", Status = Status.Ativado, Telefone = 62757496316, TipoPessoa = TipoPessoa.Cliente, Password = null };


            Veiculo v1 = new Veiculo { AnoDeFabricacao = 1996, Cor = "Amarelo", Marca = "GM - Chevrolet", Modelo = "S10 Blazer DLX 2.2 MPFI / EFI", Placa = "LVJ7375", Renavam = "95641158277", Pessoa = c1, Status = Status.Ativado };
            Veiculo v2 = new Veiculo { AnoDeFabricacao = 2022, Cor = "Branco", Marca = "Volkswagen", Modelo = "Polo", Placa = "KEI0627", Renavam = "02677894607", Pessoa = c2, Status = Status.Ativado };
            Veiculo v3 = new Veiculo { AnoDeFabricacao = 2008, Cor = "Dourado", Marca = "Nissan", Modelo = "Frontier SEL CD 4x4 2.5 TB Diesel Aut.", Placa = "HZI2946", Renavam = "12107129462", Pessoa = c3, Status = Status.Ativado };


            Pessoa vd1 = new Pessoa { Nome = "Buapi Rinan", Bairro = "Universitário", Cidade = "Goianésia", Complemento = null, Cpf = "05788039096", DataDeNascimento = new DateTime(1992, 02, 16), Email = "buapirinan@gmail.com", Estado = "Goiás", Numero = "15", Rua = "9", Status = Status.Ativado, Telefone = 62811460860, TipoPessoa = TipoPessoa.Vendedor, Password = "1775866fc55b8179d8b3f92c432d217c27423958" };
            Pessoa vd2 = new Pessoa { Nome = "Maria Santos Ferreira", Bairro = "Sul", Cidade = "Goianésia", Complemento = null, Cpf = "42097926088", DataDeNascimento = new DateTime(2004, 03, 18), Email = "mariaferreira@gmail.com", Estado = "Goiás", Numero = "10", Rua = "19", Status = Status.Desativado, Telefone = 62634089444, TipoPessoa = TipoPessoa.Vendedor, Password = "1775866fc55b8179d8b3f92c432d217c27423958" };

            Pessoa m1 = new Pessoa { Nome = "Marcos Silva", Bairro = "Morro da Ema", Cidade = "Goianésia", Complemento = null, Cpf = "41360773002", DataDeNascimento = new DateTime(2002, 02, 16), Email = "marcosilva@gmail.com", Estado = "Goiás", Numero = "15", Rua = "17", Status = Status.Ativado, Telefone = 62651186664, TipoPessoa = TipoPessoa.Mecanico, Password = "1775866fc55b8179d8b3f92c432d217c27423958" };
            Pessoa m2 = new Pessoa { Nome = "Fernando Torres Silva", Bairro = "Norte", Cidade = "Goianésia", Complemento = null, Cpf = "04278339062", DataDeNascimento = new DateTime(2000, 11, 04), Email = "fernandotorres@gmail.com", Estado = "Goiás", Numero = "486", Rua = "05", Status = Status.Ativado, Telefone = 62634089444, TipoPessoa = TipoPessoa.Mecanico, Password = "1775866fc55b8179d8b3f92c432d217c27423958" };

            Pessoa admin = new Pessoa { Nome = "Antonio Marcos Pereira", Bairro = "Centro", Cidade = "Goianésia", Complemento = null, Cpf = "51629376060", DataDeNascimento = new DateTime(2000, 11, 04), Email = "antonioPereira@gmail.com", Estado = "Goiás", Numero = "486", Rua = "05", Status = Status.Ativado, Telefone = 62876954042, TipoPessoa = TipoPessoa.Administrador, Password = "1775866fc55b8179d8b3f92c432d217c27423958" };


            Servicos s1 = new Servicos { Nome = "Troca De Oleo", Valor = 20.0, Status = Status.Ativado };
            Servicos s2 = new Servicos { Nome = "Revisão Geral", Valor = 250.0, Status = Status.Ativado };
            Servicos s3 = new Servicos { Nome = "Troca De Lampadas Em Geral", Valor = 15.0, Status = Status.Ativado };
            Servicos s4 = new Servicos { Nome = "Outros", Valor = 0, Status = Status.Ativado };


            Materiais mt1 = new Materiais { Nome = "Lampada Led", Descricao = "Utilizavel em Wolks", Quantidade = 55, Status = Status.Ativado, UnidadeMedidade = TipoUnidadeMedidade.PEÇA, PorcentagemLucro = 0.3, ValorCusto = 20, Codigo = "S1" };
            Materiais mt2 = new Materiais { Nome = "Vela Volks Geração 4", Descricao = "", Quantidade = 10, Status = Status.Ativado, UnidadeMedidade = TipoUnidadeMedidade.PEÇA, PorcentagemLucro = 0.5, ValorCusto = 45, Codigo = "S2" };
            Materiais mt3 = new Materiais { Nome = "Fio Eletrico", Descricao = "", Quantidade = 90, Status = Status.Ativado, UnidadeMedidade = TipoUnidadeMedidade.METRO, PorcentagemLucro = 0.05, ValorCusto = 1, Codigo = "S9" };
            Materiais mt4 = new Materiais { Nome = "Sockete lampada", Descricao = "Utilizavel em Wolks", Quantidade = 2, Status = Status.Ativado, UnidadeMedidade = TipoUnidadeMedidade.PEÇA, PorcentagemLucro = 0.3, ValorCusto = 20, Codigo = "A1" };


            _context.Pessoa.AddRange(c1, c2, c3, vd1, vd2, m1, m2, admin);
            _context.Veiculo.AddRange(v1, v2, v3);
            _context.Servicos.AddRange(s1, s2, s3, s4);
            _context.Materiais.AddRange(mt1, mt2, mt3, mt4);
            _context.SaveChanges();
        }
    }
}
