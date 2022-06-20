using System;

namespace BoletoNetCore.Banco.BancoNordeste.Carteiras
{
    [CarteiraCodigo("1")]
    internal class BancoNordesteCarteira1 : ICarteira<BancoNordeste>
    {
        private BancoNordesteCarteira1()
        {
        }

        internal static Lazy<ICarteira<BancoNordeste>> Instance { get; } =
            new Lazy<ICarteira<BancoNordeste>>(() => new BancoNordesteCarteira1());

        public void FormataNossoNumero(Boleto.Boleto boleto)
        {
        }

        public string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto)
        {
            return "                         ";
        }
    }
}