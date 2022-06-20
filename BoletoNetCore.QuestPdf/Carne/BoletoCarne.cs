using System.Reflection;
using BoletoNetCore.Banco;
using BoletoNetCore.Boleto;
using Microsoft.Extensions.FileProviders;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BoletoNetCore.QuestPdf.Carne
{
    internal class BoletoCarne : IDocument
    {
        private Boletos listaBoletos;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.MarginHorizontal(20);
                page.MarginVertical(20);
                page.Content().Element(ComposeContent);
            });
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        private byte[] ObterLogoBanco(IBanco banco)
        {
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
            using (var reader = embeddedProvider.GetFileInfo($"logos/{banco.Codigo.ToString("000")}.bmp")
                       .CreateReadStream())
            {
                var logo = new byte[reader.Length];
                reader.Read(logo, 0, (int) reader.Length);
                return logo;
            }
        }

        private void ComposeContent(IContainer container)
        {
            container.Stack(stack =>
            {
                byte[] logo = null;
                var codBanco = 0;
                foreach (var bol in listaBoletos)
                {
                    if (logo == null || codBanco != bol.Banco.Codigo)
                    {
                        codBanco = bol.Banco.Codigo;
                        logo = ObterLogoBanco(bol.Banco);
                    }

                    stack.Item().Row(row =>
                    {
                        row.ConstantColumn(100).Component(new ReciboLateralCarne(bol, logo));
                        row.RelativeColumn().PaddingLeft(5).Component(new ConteudoBoleto(bol, logo));
                    });

                    stack.Item().PaddingBottom(3).Text("Recibo do Pagador - Autenticar no Verso",
                        BoletoPdfConstants.LabelStyle);
                    stack.Item().ExtendHorizontal().BorderHorizontal(BoletoPdfConstants.BorderSize);
                    stack.Item().Height(15).ExtendHorizontal();
                }
            });
        }

        public byte[] BoletoPdf(Boletos listaBoletos)
        {
            this.listaBoletos = listaBoletos;
            return this.GeneratePdf();
        }
    }
}