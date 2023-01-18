using FastMechanical.Models.Enums;
using FastMechanical.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FastMechanical.Models;
using FastMechanical.Filters;
using FastMechanical.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FastMechanical.Controllers
{
    [PaginaParaUsuarioLogado]
    [PaginaParaAdministrador]
    public class AdminController : Controller
    {

        private readonly IPessoaServices _personService;
        private readonly IAlmoxarifadoServices _almoxarifadoServices;

        public AdminController(IPessoaServices personService, IAlmoxarifadoServices almoxarifadoServices)
        {
            _personService = personService;
            _almoxarifadoServices = almoxarifadoServices;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                ViewData["Title"] = "Listagem de admines ativos";
                var list = await _personService.TodosAdminAtivosAsync();
                return View(list);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> Inativos()
        {
            try
            {
                ViewData["Title"] = "Listagen de admines inativos.";
                var list = await _personService.TodosAdminDesativadosAsync();
                return View("Index", list);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {

            try
            {

                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(admin);

            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> Disable(int? id)
        {

            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View(admin);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }


        public async Task<IActionResult> Enabled(int? id)
        {

            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (admin.Status == Status.Ativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", admin);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }


        }

        public async Task<IActionResult> Details(int? id)
        {

            try
            {

                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(admin);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public IActionResult Administracao()
        {

            try
            {

                return View();
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> ExcluirBaixa()
        {

            try
            {
                ExcluirBaixaViewModel estoque = new ExcluirBaixaViewModel
                {
                    Movimentacao = await _almoxarifadoServices.BuscarBaixaPorDiaAsync(DateTime.Now),
                    Data = DateTime.Now
                };

                return View(estoque);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> ConfirmarBaixa(int? id)
        {

            try
            {
                if (id == null)
                {
                    if (id == null)
                    {
                        TempData["ErrorMessage"] = "ID não encontrado";
                        return RedirectToAction("Index");
                    }
                }
                Estoque movimentacao = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(id.Value);
                if (movimentacao == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(movimentacao);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> ExcluirEntrada()
        {

            try
            {
                ExcluirBaixaViewModel estoque = new ExcluirBaixaViewModel
                {
                    Movimentacao = await _almoxarifadoServices.BuscarAdicaoPorDiaAsync(DateTime.Now),
                    Data = DateTime.Now
                };

                return View(estoque);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> ConfirmarExclusaoEntrada(int? id)
        {

            try
            {
                if (id == null)
                {
                    if (id == null)
                    {
                        TempData["ErrorMessage"] = "ID não encontrado";
                        return RedirectToAction("Index");
                    }
                }
                Estoque movimentacao = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(id.Value);
                if (movimentacao == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(movimentacao);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> ExcluirVenda()
        {

            try
            {
                ExcluirBaixaViewModel estoque = new ExcluirBaixaViewModel
                {
                    Movimentacao = await _almoxarifadoServices.BuscarVendaPorDiaAsync(DateTime.Now),
                    Data = DateTime.Now
                };

                return View(estoque);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> ConfirmarExclusaoVenda(int? id)
        {

            try
            {
                if (id == null)
                {
                    if (id == null)
                    {
                        TempData["ErrorMessage"] = "ID não encontrado";
                        return RedirectToAction("Index");
                    }
                }
                Estoque movimentacao = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(id.Value);
                if (movimentacao == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(movimentacao);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> ExcluirExclusao()
        {

            try
            {
                ExcluirBaixaViewModel estoque = new ExcluirBaixaViewModel
                {
                    Movimentacao = await _almoxarifadoServices.BuscarExclusaoPorDiaAsync(DateTime.Now),
                    Data = DateTime.Now
                };

                return View(estoque);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }


        public async Task<IActionResult> ConfirmarExclusaoExclusao(int? id)
        {

            try
            {
                if (id == null)
                {
                    if (id == null)
                    {
                        TempData["ErrorMessage"] = "ID não encontrado";
                        return RedirectToAction("Index");
                    }
                }
                Estoque movimentacao = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(id.Value);
                if (movimentacao == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(movimentacao);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> AlterarPerfil()
        {

            try
            {
                List<Pessoa> pessoa = await _personService.TodasPessoasAtivasAsync();
                return View(pessoa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> AlterarPerfilUsuario(int? id)
        {

            try
            {
                if (id == null)
                {
                    if (id == null)
                    {
                        TempData["ErrorMessage"] = "ID não encontrado";
                        return RedirectToAction("Index");
                    }
                }
                Pessoa pessoa = await _personService.BuscarPessoaPorIdAsync(id.Value);
                if (pessoa == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (pessoa.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(pessoa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        public async Task<IActionResult> Senha(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id.Value);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (admin.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }
                return View(admin);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        public async Task<IActionResult> BaixarEstoque()
        {
            try
            {
                var baixarEstoqueViewModel = new BaixarEstoqueViewModel { ListaMateriais = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync() };
                return View(baixarEstoqueViewModel);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Pessoa admin)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(admin);
                }
                admin.Status = Status.Ativado;
                string str = admin.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                admin.Cpf = str;
                admin = await _personService.TransformCaptalizeAsync(admin);
                admin.TipoPessoa = TipoPessoa.Administrador;
                admin.Password = Pessoa.PasswordGenerate();
                string title = "Senha de acesso so sitema Fastmechanical";
                string body = $"Olá, sua senha de acesso ao sistema fastmechanical é: {admin.Password}";
                Pessoa.SendMail(admin.Email, body, title);
                admin.SetPasswordHash();
                await _personService.SalvarAsync(admin);
                TempData["SuccessMessage"] = "Usuario cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id)
        {

            try
            {
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (admin.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                admin.Status = Status.Desativado;
                await _personService.AtualizarAsync(admin);
                TempData["SuccessMessage"] = "Usuário desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id)
        {

            try
            {
                Pessoa admin = await _personService.BuscarAdminPorIdAsync(id);
                if (admin == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (admin.Status == Status.Ativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                if (admin.TipoPessoa != TipoPessoa.Administrador)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                admin.Status = Status.Ativado;
                await _personService.AtualizarAsync(admin);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Pessoa admin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(admin);
                }
                int id = (int)admin.Id;
                Pessoa dbPessoa = await _personService.BuscarAdminPorIdAsync(id);
                if (dbPessoa == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPessoa.Telefone = admin.Telefone;
                dbPessoa.Email = admin.Email;
                dbPessoa.Rua = admin.Rua;
                dbPessoa.Bairro = admin.Bairro;
                dbPessoa.Estado = admin.Estado;
                dbPessoa.Cidade = admin.Cidade;
                dbPessoa.Complemento = admin.Complemento;
                dbPessoa.Numero = admin.Numero;
                dbPessoa.DataDeNascimento = admin.DataDeNascimento;
                dbPessoa = await _personService.TransformCaptalizeAsync(dbPessoa);
                await _personService.AtualizarAsync(dbPessoa);
                TempData["SuccessMessage"] = "Usuario alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirBaixa(ExcluirBaixaViewModel baixa)
        {

            try
            {
                baixa.Movimentacao = await _almoxarifadoServices.BuscarBaixaPorDiaAsync(baixa.Data);

                return View(baixa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivarExclusao(Estoque movimentacao)
        {

            try
            {

                var movimentacaoDb = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(movimentacao.Id);
                if (movimentacaoDb.Baixa == 0)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                movimentacaoDb.DataExclusao = DateTime.Now;
                await _almoxarifadoServices.AtualizarMovimentacaoAsync(movimentacaoDb);
                var material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(movimentacaoDb.Material.Id);
                material.Quantidade += movimentacaoDb.Baixa;
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);
                var pessoaDb = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                await _almoxarifadoServices.AtualizarMaterialAsync(material);
                await _almoxarifadoServices.SalvarMovimentacaoEstoqueAsync(new Estoque { Material = material, Executor = pessoaDb, TipoMovimentacao = TipoMovimentacao.Exclusao, IdMovimentacao = movimentacaoDb.Id, DataInsercaoExclusao = DateTime.Now });

                TempData["SuccessMessage"] = "Baixa excluída com sucesso";
                return RedirectToAction("Administracao");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirEntrada(ExcluirBaixaViewModel baixa)
        {

            try
            {
                baixa.Movimentacao = await _almoxarifadoServices.BuscarAdicaoPorDiaAsync(baixa.Data);

                return View(baixa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivarExclusaoEntrada(Estoque movimentacao)
        {

            try
            {

                var movimentacaoDb = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(movimentacao.Id);

                if (movimentacaoDb.Adicao == 0)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                movimentacaoDb.DataExclusao = DateTime.Now;
                await _almoxarifadoServices.AtualizarMovimentacaoAsync(movimentacaoDb);
                var material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(movimentacaoDb.Material.Id);
                material.Quantidade -= movimentacaoDb.Adicao;
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);
                var pessoaDb = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                await _almoxarifadoServices.AtualizarMaterialAsync(material);
                await _almoxarifadoServices.SalvarMovimentacaoEstoqueAsync(new Estoque { Material = material, Executor = pessoaDb, TipoMovimentacao = TipoMovimentacao.Exclusao, IdMovimentacao = movimentacaoDb.Id, DataInsercaoExclusao = DateTime.Now });

                TempData["SuccessMessage"] = "Entrada excluída com sucesso";
                return RedirectToAction("Administracao");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirVenda(ExcluirBaixaViewModel baixa)
        {

            try
            {
                baixa.Movimentacao = await _almoxarifadoServices.BuscarVendaPorDiaAsync(baixa.Data);

                return View(baixa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivarExclusaoVenda(Estoque movimentacao)
        {

            try
            {

                var movimentacaoDb = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(movimentacao.Id);
                if (movimentacaoDb.Baixa == 0)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                movimentacaoDb.DataExclusao = DateTime.Now;
                await _almoxarifadoServices.AtualizarMovimentacaoAsync(movimentacaoDb);
                var material = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(movimentacaoDb.Material.Id);
                material.Quantidade += movimentacaoDb.Baixa;
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);
                var pessoaDb = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                await _almoxarifadoServices.AtualizarMaterialAsync(material);
                await _almoxarifadoServices.SalvarMovimentacaoEstoqueAsync(new Estoque { Material = material, Executor = pessoaDb, TipoMovimentacao = TipoMovimentacao.Exclusao, IdMovimentacao = movimentacaoDb.Id, DataInsercaoExclusao = DateTime.Now });

                TempData["SuccessMessage"] = "Venda excluída com sucesso";
                return RedirectToAction("Administracao");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirExclusao(ExcluirBaixaViewModel baixa)
        {

            try
            {
                baixa.Movimentacao = await _almoxarifadoServices.BuscarExclusaoPorDiaAsync(baixa.Data);

                return View(baixa);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EfetivarExclusaoExclusao(Estoque movimentacao)
        {

            try
            {

                var movimentacaoDb = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(movimentacao.Id);
                var movimentacaoARefazer = await _almoxarifadoServices.BuscarMovimentacaoPorIdAsync(movimentacaoDb.IdMovimentacao);
                var materialDb = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(movimentacaoDb.Material.Id);

                if (movimentacaoARefazer.Baixa != 0)
                {
                    materialDb.Quantidade -= movimentacaoARefazer.Baixa;
                }
                if (movimentacaoARefazer.Adicao != 0)
                {
                    materialDb.Quantidade += movimentacaoARefazer.Adicao;
                }
                movimentacaoARefazer.DataExclusao = null;

                await _almoxarifadoServices.AtualizarMovimentacaoAsync(movimentacaoARefazer);
                await _almoxarifadoServices.ExcluirMovimentacaoasync(movimentacaoDb);

                TempData["SuccessMessage"] = "Exclusão excluída com sucesso";
                return RedirectToAction("Administracao");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarPerfilUsuario(Pessoa pessoa)
        {

            try
            {

                Pessoa dbPessoa = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                if (dbPessoa == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (dbPessoa.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                dbPessoa.TipoPessoa = pessoa.TipoPessoa;
                await _personService.AtualizarAsync(dbPessoa);
                TempData["SuccessMessage"] = "Perfil alterado com sucesso";
                return RedirectToAction("Administracao");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Senha(Pessoa admin)
        {
            try
            {

                Pessoa adminDb = await _personService.BuscarAdminPorIdAsync(admin.Id);

                if (adminDb == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (adminDb.Status == Status.Desativado)
                {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }

                adminDb.Password = Pessoa.PasswordGenerate();
                string title = "Nova senha de acesso ao sistema FastMechanical";
                string body = $"Olá, \n\nSua senha foi redefinida! \n\nSegue a nova senha de acesso ao sistema FastMechanical: {adminDb.Password}";
                Pessoa.SendMail(adminDb.Email, body, title);
                adminDb.SetPasswordHash();
                await _personService.AtualizarAsync(adminDb);
                TempData["SuccessMessage"] = "Senha enviada com sucesso";
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaixarEstoque(BaixarEstoqueViewModel materialViewModel)
        {
            try
            {
                materialViewModel.ListaMateriais = await _almoxarifadoServices.ListarTodosMateriaisAtivosAsync();
                if (!ModelState.IsValid)
                {
                    return View(materialViewModel);
                }

                if (materialViewModel.Baixa == 0)
                {
                    TempData["ErrorMessage"] = "Baixa não pode ser zero.";
                    return RedirectToAction("BaixarEstoque");
                }
                Materiais dbMaterial = await _almoxarifadoServices.EncontrarMaterialPorIdAsync(materialViewModel.Material);
                if (dbMaterial == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (dbMaterial.Quantidade <= 0)
                {
                    TempData["ErrorMessage"] = "Não é possivel baixar estoque de material zerado.";
                    return RedirectToAction("BaixarEstoque");
                }

                var baixa = dbMaterial.Quantidade - materialViewModel.Baixa;

                if (baixa < 0)
                {
                    TempData["ErrorMessage"] = "Baixa maior que o estoque, favor verificar.";
                    return RedirectToAction("BaixarEstoque");

                }
                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;
                Pessoa pessoa = JsonConvert.DeserializeObject<Pessoa>(sessionUser);

                var dbPessoa = await _personService.BuscarPessoaPorIdAsync(pessoa.Id);

                await _almoxarifadoServices.SalvarMovimentacaoEstoqueAsync(new Estoque { Baixa = materialViewModel.Baixa, Executor = dbPessoa, Observacao = materialViewModel.Observacao, Material = dbMaterial, TipoMovimentacao = TipoMovimentacao.Baixa, DataBaixa = DateTime.Now, DataAdicao = null });
                dbMaterial.Quantidade -= materialViewModel.Baixa;
                await _almoxarifadoServices.AtualizarMaterialAsync(dbMaterial);
                TempData["SuccessMessage"] = "Material alterado com sucesso";

                return RedirectToAction("Administracao");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
