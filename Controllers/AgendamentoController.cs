using FastMechanical.Filters;
using FastMechanical.Models;
using FastMechanical.Models.Enums;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;
using static iTextSharp.text.pdf.AcroFields;

namespace FastMechanical.Controllers
{
    [PaginaParaAdminEVendedor]
    public class AgendamentoController : Controller
    {

        private readonly IPessoaServices _personService;
        private readonly IAlmoxarifadoServices _almoxarifadoServices;
        private readonly IAgendaServices _agendaServices;
        private readonly IVeiculoServices _veiculoServices;
        private readonly IServicosServices _servicosServices;

        public AgendamentoController(IPessoaServices personService, IAlmoxarifadoServices almoxarifadoServices, IAgendaServices agendaServices, IVeiculoServices veiculoServices, IServicosServices servicosServices)
        {
            _personService = personService;
            _almoxarifadoServices = almoxarifadoServices;
            _agendaServices = agendaServices;
            _veiculoServices = veiculoServices;
            _servicosServices = servicosServices;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var agenda = new AgendaViewModel { ListaAgenda = await _agendaServices.ListarTodasAgendasAtivasAsync(), ListaPessoa = await _personService.TodasPessoasAtivasAsync() };
                return View(agenda);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> Excluir(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);
                if (agenda == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (agenda.Status == AgendaStatus.Em_atendimento || agenda.Status == AgendaStatus.Finalizado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                return View("Excluir", agenda);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> Pagamento(int? id)
        {
            try
            {
                if (id == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);

                if (agenda == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agenda.Status != AgendaStatus.Aguardando_pagamento)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                AtendimentoViewModel atendimento = new AtendimentoViewModel { Agenda = agenda, PecaAtendimentos = await _almoxarifadoServices.BuscarMateriaisPorAgendaIdAsync(agenda.Id), ServicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id) };

                return View(atendimento);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        public async Task<IActionResult> RelatorioAgenda(int? id)
        {
            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try
            {

                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(id.Value);

                if (agenda == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agenda.Status != AgendaStatus.Aguardando_pagamento)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                var pecaAtendimentos = await _almoxarifadoServices.BuscarMateriaisPorAgendaIdAsync(agenda.Id);
                var servicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                //var image = System.Drawing.Image.FromFile("D:\\TCC\\FastMechanical\\wwwroot\\images\\logo.png");
                var image = System.Drawing.Image.FromFile("C:\\Users\\Lucas Jorge\\OneDrive - w0ytt\\Documentos\\FastMechanical\\wwwroot\\images\\logo.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic};

                document.Add(paragraph1);
                double valorServicos = 0;
                double valorPecas = 0;
                document.Add(new Paragraph($"\nOrçamento do veiculo {agenda.Veiculo.Modelo} placa - {agenda.Veiculo.Placa} - Mecanico: {agenda.Mecanico.Nome}\n\n"));
                if (servicoAtendimentos.Count > 0)
                {
                    document.Add(new Paragraph($"Serviços realizados\n\n"));
                    PdfPTable table = new PdfPTable(3);

                    Paragraph p1 = new Paragraph("Id");
                    PdfPCell H1 = new PdfPCell(p1);
                    H1.BackgroundColor = new BaseColor(189, 189, 189);
                    H1.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p2 = new Paragraph("Serviço");
                    PdfPCell H2 = new PdfPCell(new Paragraph(p2));
                    H2.BackgroundColor = new BaseColor(189, 189, 189);
                    H2.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p3 = new Paragraph("Valor");
                    PdfPCell H3 = new PdfPCell(p3);
                    H3.BackgroundColor = new BaseColor(189, 189, 189);
                    H3.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(H1);
                    table.AddCell(H2);
                    table.AddCell(H3);

                    foreach (var item in servicoAtendimentos)
                    {
                        valorServicos += item.Servico.Valor;
                        PdfPCell cell1 = new PdfPCell(new Paragraph($"{item.Servico.Id}"));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell2 = new PdfPCell(new Paragraph($"{item.Servico.Nome}"));
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell3 = new PdfPCell(new Paragraph($"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.Servico.Valor)}"));
                        cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        table.AddCell(cell3);
                    }

                    PdfPCell valorServicosTotal = new PdfPCell(new Paragraph($"Valor total dos serviços: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorServicos)}"));
                    valorServicosTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    valorServicosTotal.Colspan = 3;
                    table.AddCell(valorServicosTotal);

                    table.SpacingAfter = 10f;
                    document.Add(table);
                }
                if (pecaAtendimentos.Count > 0)
                {

                    document.Add(new Paragraph($"Peças realizados\n\n"));
                    PdfPTable table = new PdfPTable(4);

                    Paragraph p1 = new Paragraph("Id");
                    PdfPCell H1 = new PdfPCell(p1);
                    H1.BackgroundColor = new BaseColor(189, 189, 189);
                    H1.HorizontalAlignment = Element.ALIGN_CENTER;



                    Paragraph p2 = new Paragraph("Material");
                    PdfPCell H2 = new PdfPCell(new Paragraph(p2));
                    H2.BackgroundColor = new BaseColor(189, 189, 189);
                    H2.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p3 = new Paragraph("Quantidade");
                    PdfPCell H3 = new PdfPCell(p3);
                    H3.BackgroundColor = new BaseColor(189, 189, 189);
                    H3.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p4 = new Paragraph("Valor");
                    PdfPCell H4 = new PdfPCell(p3);
                    H4.BackgroundColor = new BaseColor(189, 189, 189);
                    H4.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(H1);
                    table.AddCell(H2);
                    table.AddCell(H3);
                    table.AddCell(H4);

                    foreach (var item in pecaAtendimentos)
                    {
                        var valor = (item.Material.ValorCusto * item.Material.PorcentagemLucro + item.Material.ValorCusto) * item.Quantidade;
                        valorPecas += valor;
                        PdfPCell cell1 = new PdfPCell(new Paragraph($"{item.Material.Id}"));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell2 = new PdfPCell(new Paragraph($"{item.Material.Nome}"));
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell3 = new PdfPCell(new Paragraph($"{item.Quantidade}"));
                        cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell4 = new PdfPCell(new Paragraph($"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor)}"));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        table.AddCell(cell3);
                        table.AddCell(cell4);
                    }
                    PdfPCell valorPecasTotal = new PdfPCell(new Paragraph($"Valor total dos serviços: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorPecas)}"));
                    valorPecasTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    valorPecasTotal.Colspan = 4;
                    table.AddCell(valorPecasTotal);

                    table.SpacingAfter = 10f;
                    document.Add(table);
                    document.Add(new Paragraph($"\n\n\nValor total da manutenção: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorPecas + valorServicos)}\n\n"));
                }

            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required
            return File(stream, "application/pdf", $"Orçamento.pdf");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agenda(AgendaViewModel agenda)
        {
            try
            {
                agenda.Veiculos = await _veiculoServices.BuscarVeiculoPorClienteId(agenda.Cliente.Id);
                agenda.Servicos = await _servicosServices.TodosServicosAtivosAsync();
                agenda.Mecanicos = await _personService.TodosMecanicosAtivosAsync();
                return View(agenda);
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarAgenda(AgendaViewModel agenda)
        {
            try
            {

                if (agenda.Veiculo == null || agenda.Servico == null || agenda.Mecanico == null)
                {
                    TempData["ErrorMessage"] = "Todos os campos devem estar preenchidos";
                    return RedirectToAction("Index");
                }
                Pessoa mecanico = await _personService.BuscarMecanicoPorIdAsync(agenda.Mecanico.Id);
                if (mecanico == null)
                {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Veiculo veiculo = await _veiculoServices.EncontrarVeiculoPorIdAsync(agenda.Veiculo.Id);
                if (veiculo == null)
                {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Pessoa cliente = await _personService.BuscarClientePorIdAsync(veiculo.Pessoa.Id);
                if (cliente == null)
                {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }

                Servicos servico = await _servicosServices.EncontrarServicosPorIdAsync(agenda.Servico.Id);
                if (servico == null)
                {
                    TempData["ErrorMessage"] = "ID inválido";
                    return RedirectToAction("Index");
                }
                Agenda agendaDb = new Agenda { Cliente = cliente, Veiculo = veiculo, Servico = servico, Mecanico = mecanico, Status = Models.Enums.AgendaStatus.Aguardando, DataInicial = DateTime.Now, DataFinal = null };
                await _agendaServices.SalvarAgendaAsync(agendaDb);

                TempData["SuccessMessage"] = "Agenda marcada com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Agenda agenda)
        {
            try
            {
                var agendaDb = await _agendaServices.BuscarAgendaPorIdAsync(agenda.Id);
                if (agendaDb == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agendaDb.Status == AgendaStatus.Em_atendimento)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agendaDb.Status == AgendaStatus.Finalizado)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                await _agendaServices.ExcluirAgendaAsync(agendaDb);

                TempData["SuccessMessage"] = "Agenda excluida com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarAgendamento(AtendimentoViewModel agendaViewMdodel)
        {
            try
            {
                var agendaDb = await _agendaServices.BuscarAgendaPorIdAsync(agendaViewMdodel.Agenda.Id);
                if (agendaDb == null)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (agendaDb.Status != AgendaStatus.Aguardando_pagamento)
                {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                agendaDb.Status = AgendaStatus.Finalizado;
                agendaDb.DataFinal = DateTime.Now;

                await _agendaServices.AtualizarAgendaAsync(agendaDb);

                TempData["SuccessMessage"] = "Agenda finalizada com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["ErrorMessage"] = erro.Message;
                return View();
            }
        }
    }
}
