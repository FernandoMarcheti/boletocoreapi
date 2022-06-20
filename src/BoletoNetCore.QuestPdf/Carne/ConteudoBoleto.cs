using BoletoNetCore.QuestPdf.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BoletoNetCore.QuestPdf.Carne
{
    internal class ConteudoBoleto : IComponent
    {
        private readonly Boleto.Boleto boleto;
        private readonly byte[] logo;

        public ConteudoBoleto(Boleto.Boleto boleto, byte[] logo)
        {
            this.boleto = boleto;
            this.logo = logo;
        }

        public void Compose(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Element(ComposeDadosBancoELinhaDigitavel);
                stack.Item().Row(row =>
                {
                    row.RelativeColumn().Element(ComposeInformacoesBoleto);
                    row.ConstantColumn(100).Element(ComposeValoresBoleto);
                });

                stack.Item().Row(row =>
                {
                    row.RelativeColumn().Element(ComposeDadosPagador);
                    row.ConstantColumn(100).Element(ComposeDocumentoPagador);
                });

                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Element(ComposeCodBarrasBoleto);
                    row.ConstantColumn(100).Element(ComposeDadosAutenticacacaoMecanicaBoleto);
                });
            });
        }

        private void ComposeDadosAutenticacacaoMecanicaBoleto(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Spacing(15);
                stack.Item().AlignCenter().Text("Autenticação Mecânica", BoletoPdfConstants.LabelStyle);
                stack.Item().AlignCenter().Text("Ficha de Compensação", BoletoPdfConstants.LabelStyle);
            });
        }

        private void ComposeCodBarrasBoleto(IContainer container)
        {
            var codbar = boleto.CodigoBarra.CodigoDeBarras.GerarCodBarras128(40);
            container.Image(codbar);
        }

        private void ComposeDocumentoPagador(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Text("CPF / CNPJ do Sacado", BoletoPdfConstants.LabelStyle);
                stack.Item().Text(boleto.Pagador.CPFCNPJ.MascararCpfCnpj(), BoletoPdfConstants.NormalFieldStyle);
                stack.Item().Text("Código de Baixa", BoletoPdfConstants.LabelStyle);
            });
        }

        private void ComposeDadosPagador(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Row(row =>
                {
                    row.ConstantColumn(30).Text("Pagador", BoletoPdfConstants.LabelStyle);
                    row.RelativeColumn().Stack(stk =>
                    {
                        stk.Item().Text(boleto.Pagador.Nome, BoletoPdfConstants.NormalFieldStyle);
                        stk.Item().Text(
                            $"{boleto.Pagador.Endereco.LogradouroEndereco}, {boleto.Pagador.Endereco.LogradouroNumero} - {boleto.Pagador.Endereco.Bairro}",
                            BoletoPdfConstants.NormalFieldStyle);
                        stk.Item().Text(
                            $"{boleto.Pagador.Endereco.CEP.FormatarCep()}, {boleto.Pagador.Endereco.Cidade} {boleto.Pagador.Endereco.UF}",
                            BoletoPdfConstants.NormalFieldStyle);
                    });
                });

                stack.Item().Text("Sacador/Avalista:", BoletoPdfConstants.LabelStyle);
            });
        }

        private void ComposeInformacoesBoleto(IContainer container)
        {
            container.BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
            {
                stack.Item().Text("Local de Pagamento", BoletoPdfConstants.LabelStyle);
                stack.Item().Text(boleto.Banco.Beneficiario.ContaBancaria.LocalPagamento,
                    BoletoPdfConstants.NormalFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("Beneficiário", BoletoPdfConstants.LabelStyle);
                stack.Item()
                    .Text(
                        $"{boleto.Banco.Beneficiario.Nome} - CNPJ: {boleto.Banco.Beneficiario.CPFCNPJ.MascararCpfCnpj()}",
                        BoletoPdfConstants.NormalFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize).Element(ComposeLinhaDataDocumentoBoleto);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize).Element(ComposeLinhUsoBancoBoleto);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize).Element(ComposeLinhaInstrucoesBoleto);
            });
        }

        private void ComposeLinhaInstrucoesBoleto(IContainer container)
        {
            container.Height(52f).Stack(stack =>
            {
                stack.Item().Text("Instruções (Texto de responsabilidade do benenficiário)",
                    BoletoPdfConstants.LabelStyle);
                stack.Item().Text(boleto.MensagemInstrucoesCaixaFormatado, BoletoPdfConstants.NormalFieldStyle);
            });
        }

        private void ComposeLinhUsoBancoBoleto(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().Text("Uso do Banco", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text("", BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn(0.5f).BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Carteira", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text(boleto.Banco.Beneficiario.ContaBancaria.CarteiraComVariacaoPadrao,
                        BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn(0.5f).BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Espécie", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text(boleto.EspecieMoeda, BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Quantidade", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text("", BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Valor", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text("", BoletoPdfConstants.NormalFieldStyle);
                });
            });
        }

        private void ComposeLinhaDataDocumentoBoleto(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().Text("Data do Documento", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter()
                        .Text(boleto.DataEmissao.ToDateStr(), BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Número do Documento", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text(boleto.NumeroDocumento, BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn(0.5f).BorderRight(BoletoPdfConstants.BorderSize).Stack(st =>
                {
                    st.Item().PaddingLeft(3).Text("Espécie Doc.", BoletoPdfConstants.LabelStyle);
                    st.Item().AlignCenter().Text($"{boleto.EspecieDocumento}", BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn(0.5f).BorderRight(BoletoPdfConstants.BorderSize).Stack(st =>
                {
                    st.Item().PaddingLeft(3).Text("Aceite", BoletoPdfConstants.LabelStyle);
                    st.Item().AlignCenter().Text($"{boleto.Aceite}", BoletoPdfConstants.NormalFieldStyle);
                });

                row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(stack =>
                {
                    stack.Item().PaddingLeft(3).Text("Data do Processamento", BoletoPdfConstants.LabelStyle);
                    stack.Item().AlignCenter().Text(boleto.DataProcessamento.ToDateStr(),
                        BoletoPdfConstants.NormalFieldStyle);
                });
            });
        }

        private void ComposeValoresBoleto(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Text("Vencimento", BoletoPdfConstants.LabelStyle);
                stack.Item().AlignRight().Text(boleto.DataVencimento.ToDateStr(), BoletoPdfConstants.BoldFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("Agência / Código Beneficiário", BoletoPdfConstants.LabelStyle);
                stack.Item().AlignRight().Text(boleto.Banco.Beneficiario.CodigoFormatado,
                    BoletoPdfConstants.NormalFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("Nosso Número", BoletoPdfConstants.LabelStyle);
                stack.Item().AlignRight().Text(boleto.NossoNumeroFormatado, BoletoPdfConstants.NormalFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("(=) Valor do Documento", BoletoPdfConstants.LabelStyle);
                stack.Item().AlignRight().Text(boleto.ValorTitulo.FormatarMoeda(), BoletoPdfConstants.BoldFieldStyle);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("(-) Desconto / Abatimento", BoletoPdfConstants.LabelStyle);
                stack.Item().Height(10);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("(+) Mora / Multa", BoletoPdfConstants.LabelStyle);
                stack.Item().Height(10);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);

                stack.Item().Text("(=) Valor Cobrado", BoletoPdfConstants.LabelStyle);
                stack.Item().Height(10);
                stack.Item().BorderHorizontal(BoletoPdfConstants.BorderSize);
            });
        }

        private void ComposeDadosBancoELinhaDigitavel(IContainer container)
        {
            container.BorderBottom(BoletoPdfConstants.BorderSize).Row(row =>
            {
                row.ConstantColumn(75).Height(20).BorderRight(BoletoPdfConstants.BorderSize).AlignLeft().AlignBottom()
                    .Image(logo, ImageScaling.FitArea);
                row.ConstantColumn(55).BorderRight(BoletoPdfConstants.BorderSize).AlignBottom().AlignCenter().Text(
                    $"{boleto.Banco.Codigo.ToString("000")}-{boleto.Banco.Digito}", BoletoPdfConstants.CodBancoStyle);
                row.RelativeColumn().AlignRight().AlignBottom().Text(boleto.CodigoBarra.LinhaDigitavel,
                    BoletoPdfConstants.LinhaDigitavelStyle);
            });
        }
    }
}