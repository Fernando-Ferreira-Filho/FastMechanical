using FastMechanical.Filters;
using FastMechanical.Models.ViewModel;
using FastMechanical.Services;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.IO;
using System;
using System.Threading.Tasks;
using FastMechanical.Models.Enums;
using FastMechanical.Models;

namespace FastMechanical.Controllers {
    [PaginaParaAdministrador]
    public class RelatorioController : Controller {
        private readonly IPessoaServices _personService;
        private readonly IAlmoxarifadoServices _almoxarifadoServices;
        private readonly IAgendaServices _agendaServices;
        private readonly IVeiculoServices _veiculoServices;
        private readonly IServicosServices _servicosServices;

        public RelatorioController(IPessoaServices personService, IAlmoxarifadoServices almoxarifadoServices, IAgendaServices agendaServices, IVeiculoServices veiculoServices, IServicosServices servicosServices) {
            _personService = personService;
            _almoxarifadoServices = almoxarifadoServices;
            _agendaServices = agendaServices;
            _veiculoServices = veiculoServices;
            _servicosServices = servicosServices;
        }

        public IActionResult Index() {
            RelatorioViewModel relatorio = new RelatorioViewModel { DataFinal = DateTime.Now, DataInicial = DateTime.Now };
            return View(relatorio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LucrosPorData(RelatorioViewModel relatorio) {
            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {

                var atendimentos = await _agendaServices.BuscarAgendaPorDataAsync(relatorio.DataInicial, relatorio.DataFinal);
                var vendas = await _almoxarifadoServices.BuscarVendasPorDatasAsync(relatorio.DataInicial, relatorio.DataFinal);
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\FastMechanical\\wwwroot\\images\\logo.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic};

                document.Add(paragraph1);

                document.Add(new Paragraph($"\nLucros da oficiona por data, data inicial: {relatorio.DataInicial.ToShortDateString()} - data final: {relatorio.DataFinal.ToShortDateString()}\n\n"));
                double valorTotalServicos = 0;
                double valorServicos = 0;
                double valorTotalPecas = 0;
                double valorPecas = 0;
                double valorPeca = 0;
                double valorLucroPeca = 0;
                double valorLucroServicos = 0;
                if (atendimentos.Count > 0) {
                    foreach (var item in atendimentos) {

                        Paragraph title = new Paragraph($"\nAtendimento do veiculo {item.Veiculo.Modelo} - Dono {item.Cliente.Nome} - Data {item.DataInicial.ToShortDateString()}\n\n");
                        title.Alignment = Element.ALIGN_CENTER;
                        document.Add(title);

                        Paragraph sp = new Paragraph($"Serviços\n\n", FontFactory.GetFont("Times New Roman", 10, Font.BOLD));
                        sp.Alignment = Element.ALIGN_CENTER;
                        document.Add(sp);
                        //Serviços--------------------------------------------------------------------------
                        PdfPTable tableServicos = new PdfPTable(3);

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

                        tableServicos.AddCell(H1);
                        tableServicos.AddCell(H2);
                        tableServicos.AddCell(H3);

                        var servicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(item.Id);
                        foreach (var servico in servicoAtendimentos) {
                            valorServicos += servico.Servico.Valor;
                            valorLucroServicos += valorServicos;
                            PdfPCell cell1 = new PdfPCell(new Paragraph($"{servico.Servico.Id}"));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfPCell cell2 = new PdfPCell(new Paragraph($"{servico.Servico.Nome}"));
                            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfPCell cell3 = new PdfPCell(new Paragraph($"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", servico.Servico.Valor)}"));
                            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                            tableServicos.AddCell(cell1);
                            tableServicos.AddCell(cell2);
                            tableServicos.AddCell(cell3);
                        }

                        PdfPCell valorServicosTotal = new PdfPCell(new Paragraph($"Valor total dos serviços: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorServicos)}"));
                        valorServicosTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                        valorServicosTotal.Colspan = 3;
                        tableServicos.AddCell(valorServicosTotal);
                        valorTotalServicos += valorServicos;
                        tableServicos.SpacingAfter = 10f;
                        document.Add(tableServicos);
                        //Fim Serviços--------------------------------------------------------------------------

                        Paragraph pp = new Paragraph($"Peças\n\n", FontFactory.GetFont("Times New Roman", 10, Font.BOLD));
                        pp.Alignment = Element.ALIGN_CENTER;
                        document.Add(pp);
                        //Peças--------------------------------------------------------------------------
                        PdfPTable tablePecas = new PdfPTable(4);

                        Paragraph pt1 = new Paragraph("Id");
                        PdfPCell Ht1 = new PdfPCell(pt1);
                        Ht1.BackgroundColor = new BaseColor(189, 189, 189);
                        Ht1.HorizontalAlignment = Element.ALIGN_CENTER;

                        Paragraph pt2 = new Paragraph("Material");
                        PdfPCell Ht2 = new PdfPCell(new Paragraph(pt2));
                        Ht2.BackgroundColor = new BaseColor(189, 189, 189);
                        Ht2.HorizontalAlignment = Element.ALIGN_CENTER;

                        Paragraph pt3 = new Paragraph("Quantidade");
                        PdfPCell Ht3 = new PdfPCell(pt3);
                        Ht3.BackgroundColor = new BaseColor(189, 189, 189);
                        Ht3.HorizontalAlignment = Element.ALIGN_CENTER;

                        Paragraph pt4 = new Paragraph("Valor");
                        PdfPCell Ht4 = new PdfPCell(pt4);
                        Ht4.BackgroundColor = new BaseColor(189, 189, 189);
                        Ht4.HorizontalAlignment = Element.ALIGN_CENTER;

                        tablePecas.AddCell(Ht1);
                        tablePecas.AddCell(Ht2);
                        tablePecas.AddCell(Ht3);
                        tablePecas.AddCell(Ht4);

                        var pecaServicos = await _servicosServices.BuscarPecaAtendimentoPorAtendimento(item.Id);
                        foreach (var pecas in pecaServicos) {
                            valorPeca = (pecas.Material.ValorCusto * pecas.Material.PorcentagemLucro + pecas.Material.ValorCusto) * pecas.Quantidade;
                            valorPecas += valorPeca;
                            valorLucroPeca += pecas.Material.ValorCusto * pecas.Material.PorcentagemLucro * pecas.Quantidade;

                            PdfPCell cell1 = new PdfPCell(new Paragraph($"{pecas.Material.Id}"));
                            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfPCell cell2 = new PdfPCell(new Paragraph($"{pecas.Material.Nome}"));
                            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfPCell cell3 = new PdfPCell(new Paragraph($"{pecas.Quantidade}"));
                            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                            PdfPCell cell4 = new PdfPCell(new Paragraph($"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorPeca)}"));
                            cell4.HorizontalAlignment = Element.ALIGN_CENTER;

                            tablePecas.AddCell(cell1);
                            tablePecas.AddCell(cell2);
                            tablePecas.AddCell(cell3);
                            tablePecas.AddCell(cell4);
                        }

                        PdfPCell valorPecasTotal = new PdfPCell(new Paragraph($"Valor total dos materiais: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorPecas)}"));
                        valorPecasTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                        valorPecasTotal.Colspan = 4;

                        tablePecas.AddCell(valorPecasTotal);
                        valorTotalPecas += valorPecas;
                        tablePecas.SpacingAfter = 10f;

                        document.Add(tablePecas);
                        document.Add(new Paragraph($"Valor total do atendimento: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorTotalPecas + valorTotalServicos)}"));
                        valorTotalPecas = 0;
                        valorTotalServicos = 0;
                        //Fim peças--------------------------------------------------------------------------
                    }
                }
                if (vendas.Count > 0) {

                    Paragraph sp = new Paragraph($"\n\nVendas realizadas no balcão\n\n");
                    sp.Alignment = Element.ALIGN_CENTER;
                    document.Add(sp);

                    PdfPTable tableVendas = new PdfPTable(4);

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

                    tableVendas.AddCell(H1);
                    tableVendas.AddCell(H2);
                    tableVendas.AddCell(H3);
                    tableVendas.AddCell(H4);

                    valorTotalPecas = 0;
                    valorPecas = 0;
                    valorPeca = 0;

                    foreach (var item in vendas) {

                        var valor = (item.Material.ValorCusto * item.Material.PorcentagemLucro + item.Material.ValorCusto) * item.Baixa;
                        valorPecas += valor;
                        valorLucroPeca += item.Material.ValorCusto * item.Material.PorcentagemLucro * item.Baixa;

                        PdfPCell cell1 = new PdfPCell(new Paragraph($"{item.Material.Id}"));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell2 = new PdfPCell(new Paragraph($"{item.Material.Nome}"));
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell3 = new PdfPCell(new Paragraph($"{item.Baixa}"));
                        cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell4 = new PdfPCell(new Paragraph($"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valor)}"));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        tableVendas.AddCell(cell1);
                        tableVendas.AddCell(cell2);
                        tableVendas.AddCell(cell3);
                        tableVendas.AddCell(cell4);
                    }
                    PdfPCell valorPecasTotal = new PdfPCell(new Paragraph($"Valor total dos materiais vendidos: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorPecas)}"));
                    valorPecasTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    valorPecasTotal.Colspan = 4;
                    tableVendas.AddCell(valorPecasTotal);

                    tableVendas.SpacingAfter = 10f;
                    document.Add(tableVendas);
                }
                Paragraph ts = new Paragraph($"\n\nValor lucrado na data mencionada: {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorLucroPeca + valorLucroServicos)}\n\n", FontFactory.GetFont("Times New Roman", 18, Font.BOLD));
                ts.Alignment = Element.ALIGN_CENTER;
                document.Add(ts);
            }
            catch (DocumentException de) {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe) {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required
            return File(stream, "application/pdf", $"Orçamento.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Orcamentos(RelatorioViewModel relatorio) {

            var agendas = await _agendaServices.BuscarAgendaPorDataAsync(relatorio.DataInicial, relatorio.DataFinal);
            relatorio.Agendas = agendas;
            return View("Index", relatorio);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImprimirOrcamento(RelatorioViewModel relatorio) {

            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {

                Agenda agenda = await _agendaServices.BuscarAgendaPorIdAsync(relatorio.Agenda.Id);

                if (agenda == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                var pecaAtendimentos = await _almoxarifadoServices.BuscarMateriaisPorAgendaIdAsync(agenda.Id);
                var servicoAtendimentos = await _servicosServices.BuscarServicoAtendimentoPorAtendimento(agenda.Id);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\FastMechanical\\wwwroot\\images\\logo.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic};

                document.Add(paragraph1);
                double valorServicos = 0;
                double valorPecas = 0;
                document.Add(new Paragraph($"\nOrçamento do veiculo {agenda.Veiculo.Modelo} placa - {agenda.Veiculo.Placa} - Mecanico: {agenda.Mecanico.Nome}\n\n"));
                if (servicoAtendimentos.Count > 0) {
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

                    foreach (var item in servicoAtendimentos) {
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
                if (pecaAtendimentos.Count > 0) {

                    document.Add(new Paragraph($"Peças utilizadas\n\n"));
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

                    foreach (var item in pecaAtendimentos) {
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
            catch (DocumentException de) {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe) {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required
            return File(stream, "application/pdf", $"Orçamento.pdf");
        }
    }
}
