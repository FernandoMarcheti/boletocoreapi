using BoletoNetCore.QuestPdf.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace BoletoNetCore.QuestPdf.Carne
{
    internal class ReciboLateralCarne : IComponent
    {
        private readonly Boleto.Boleto boleto;
        private readonly byte[] logo;

        public ReciboLateralCarne(Boleto.Boleto boleto, byte[] logo)
        {
            this.logo = logo;
            this.boleto = boleto;
        }

        public void Compose(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Width(75).Height(20).AlignBottom().AlignLeft().Image(logo, ImageScaling.FitArea);
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(st =>
                    {
                        st.Item().Text("Parcela", BoletoPdfConstants.LabelStyle);
                        st.Item().Text("001 / 001", BoletoPdfConstants.NormalFieldStyle); // todo
                    });

                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("Vencimento", BoletoPdfConstants.LabelStyle);
                        st.Item().Text(boleto.DataVencimento.ToDateStr(), BoletoPdfConstants.BoldFieldStyle);
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("Agência / Código Beneficiário", BoletoPdfConstants.LabelStyle);
                        st.Item().AlignRight().Text(boleto.Banco.Beneficiario.CodigoFormatado,
                            BoletoPdfConstants.NormalFieldStyle);
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().BorderRight(BoletoPdfConstants.BorderSize).Stack(st =>
                    {
                        st.Item().Text("Espécie", BoletoPdfConstants.LabelStyle);
                        st.Item().AlignCenter().Text(boleto.EspecieMoeda, BoletoPdfConstants.NormalFieldStyle);
                    });

                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("Quantidade", BoletoPdfConstants.LabelStyle);
                        st.Item().ExtendHorizontal();
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("(=) Valor do Documento", BoletoPdfConstants.LabelStyle);
                        st.Item().AlignRight().Text(boleto.ValorTitulo.FormatarMoeda(),
                            BoletoPdfConstants.BoldFieldStyle);
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("(-) Desconto / Abatimento", BoletoPdfConstants.LabelStyle);
                        st.Item().Height(10).ExtendHorizontal();
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("(+) Mora / Multa", BoletoPdfConstants.LabelStyle);
                        st.Item().Height(10).ExtendHorizontal();
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("(=) Valor Cobrado", BoletoPdfConstants.LabelStyle);
                        st.Item().Height(10).ExtendHorizontal();
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("Número Documento", BoletoPdfConstants.LabelStyle);
                        st.Item().AlignRight().Text(boleto.NumeroDocumento, BoletoPdfConstants.NormalFieldStyle);
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        st.Item().Text("Nosso Número", BoletoPdfConstants.LabelStyle);
                        st.Item().AlignRight().Text(boleto.NossoNumeroFormatado, BoletoPdfConstants.NormalFieldStyle);
                    });
                });
                stack.Item().BorderTop(BoletoPdfConstants.BorderSize).Row(row =>
                {
                    row.RelativeColumn().Stack(st =>
                    {
                        var ben = boleto.Banco.Beneficiario;
                        var pag = boleto.Pagador;
                        st.Spacing(5);
                        st.Item().Text(
                            $"Beneficiário: {ben.Nome} - {ben.Endereco.LogradouroEndereco}, {ben.Endereco.LogradouroNumero} - {ben.Endereco.Bairro} - {ben.Endereco.Cidade} - {ben.Endereco.UF} - {ben.Endereco.CEP.FormatarCep()} - CNPJ: {ben.CPFCNPJ.MascararCpfCnpj()}",
                            BoletoPdfConstants.LabelStyle);

                        var cpfCnpj = pag.CPFCNPJ.IsCnpj() ? "CNPJ" : "CPF";
                        st.Item().Text($"Pagador: {pag.Nome} - {cpfCnpj}: {pag.CPFCNPJ.MascararCpfCnpj()}",
                            BoletoPdfConstants.LabelStyle);
                    });
                });
            });
        }
    }
}