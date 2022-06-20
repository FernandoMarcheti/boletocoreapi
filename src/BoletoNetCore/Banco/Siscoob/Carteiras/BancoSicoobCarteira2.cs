using System;

namespace BoletoNetCore.Banco.Siscoob.Carteiras
{
    [CarteiraCodigo("1/02")]
    internal class BancoSicoobCarteira2 : ICarteira<BancoSicoob>
    {
        internal static Lazy<ICarteira<BancoSicoob>> Instance { get; } =
            new Lazy<ICarteira<BancoSicoob>>(() => new BancoSicoobCarteira2());

        public string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto)
        {
            return "                         ";
        }

        public void FormataNossoNumero(Boleto.Boleto boleto)
        {
        }
    }
}