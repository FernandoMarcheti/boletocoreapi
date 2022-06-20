using System.Collections.Generic;

namespace BoletoNetCore.Banco.BancoNordeste
{
    internal sealed partial class BancoNordeste : BancoFebraban<BancoNordeste>, IBanco
    {
        public BancoNordeste()
        {
            Codigo = 4;
            Nome = "Banco Nordeste";
            Digito = "3";
            IdsRetornoCnab400RegistroDetalhe = new List<string> {"1"};
            RemoveAcentosArquivoRemessa = true;
        }

        public void FormataBeneficiario()
        {
        }
    }
}