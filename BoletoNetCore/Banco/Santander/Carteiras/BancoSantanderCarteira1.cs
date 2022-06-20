using System;

namespace BoletoNetCore.Banco.Santander.Carteiras
{
    [CarteiraCodigo("1")]
    internal class BancoSantanderCarteira1 : ICarteira<BancoSantander>
    {
        private BancoSantanderCarteira1()
        {
        }

        internal static Lazy<ICarteira<BancoSantander>> Instance { get; } =
            new Lazy<ICarteira<BancoSantander>>(() => new BancoSantanderCarteira1());

        public void FormataNossoNumero(Boleto.Boleto boleto)
        {
        }

        public string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto)
        {
            return "                         ";
        }
    }
}