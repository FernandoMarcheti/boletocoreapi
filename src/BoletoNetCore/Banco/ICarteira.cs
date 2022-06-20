namespace BoletoNetCore.Banco
{
    internal interface ICarteira<T>
        where T : IBanco
    {
        void FormataNossoNumero(Boleto.Boleto boleto);
        string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto);
    }
}