using System;
using BoletoNetCore.Util;

namespace BoletoNetCore.Banco.Cecred.Carteiras
{
    [CarteiraCodigo("1")]
    internal class BancoCecredCarteira1 : ICarteira<BancoCecred>
    {
        internal static Lazy<ICarteira<BancoCecred>> Instance { get; } =
            new Lazy<ICarteira<BancoCecred>>(() => new BancoCecredCarteira1());

        public string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto)
        {
            var parte1 = string.Format("{0}{1}{2}",
                Utils.FormatCode(boleto.Banco.Beneficiario.Codigo, 6),
                boleto.NossoNumero,
                Utils.FormatCode(boleto.Carteira, 2));
            return parte1;
        }

        public void FormataNossoNumero(Boleto.Boleto boleto)
        {
            var nossoNumero = boleto.NossoNumero;
            boleto.NossoNumeroDV = Mod11(Sequencial(boleto));
            var conta = Utils.FormatCode(
                boleto.Banco.Beneficiario.ContaBancaria.Conta + boleto.Banco.Beneficiario.ContaBancaria.DigitoConta, 8);
            boleto.NossoNumero = Sequencial(boleto);
            boleto.NossoNumeroFormatado = string.Format("{0}/{1}", conta, nossoNumero);
        }

        private string Mod11(string seq)
        {
            var num1 = 0;
            var num2 = 9;
            var num3 = 2;
            for (var startIndex = seq.Length - 1; startIndex >= 0; --startIndex)
            {
                num1 += Convert.ToInt32(seq.Substring(startIndex, 1)) * num2;
                if (num2 == num3)
                    num2 = 9;
                else
                    --num2;
            }

            var num4 = num1 % 11;
            string str;
            switch (num4)
            {
                case 0:
                    str = "0";
                    break;
                case 10:
                    str = "1";
                    break;
                default:
                    str = num4.ToString();
                    break;
            }

            return str;
        }

        private string Sequencial(Boleto.Boleto boleto)
        {
            var conta = boleto.Banco.Beneficiario.ContaBancaria.Conta;
            var digitoConta = boleto.Banco.Beneficiario.ContaBancaria.DigitoConta;
            var contaComDigito = conta + digitoConta;
            var nossoNumero = boleto.NossoNumero;
            return string.Format("{0}{1}", Utils.FormatCode(contaComDigito, 8), Utils.FormatCode(nossoNumero, 9));
        }
    }
}