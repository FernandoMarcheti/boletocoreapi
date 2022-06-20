using BoletoNetCore.BoletoImpressao;
using Wkhtmltopdf.NetCore;

namespace BoletoNetCore.Pdf.BoletoImpressao
{
    public class BoletoBancarioPdf : BoletoBancario
    {
        public byte[] MontaBytesPDF(bool convertLinhaDigitavelToImage = false, string urlImagemLogoBeneficiario = null)
        {
#if NETSTANDARD2
            return new HtmlAsPdf().GetPDF(MontaHtmlEmbedded(convertLinhaDigitavelToImage, true,
                urlImagemLogoBeneficiario));
#else
            throw new NotImplementedException();
#endif
        }
    }
}