using BoletoNetCore.Boleto;
using BoletoNetCore.QuestPdf.Carne;

namespace BoletoNetCore.QuestPdf
{
    public static class BoletoNetCoreHelper
    {
        public static byte[] ImprimirCarnePdf(this Boletos listaBoletos)
        {
            return new BoletoCarne().BoletoPdf(listaBoletos);
        }
    }
}