using System.Collections.Generic;
using System.Threading.Tasks;
using BoletoNetCore.Arquivo;
using BoletoNetCore.Boleto;
using BoletoNetCore.Enums;

namespace BoletoNetCore.Banco
{
    public interface IBanco
    {
        /// <summary>
        ///     Benefici�rio de uma Cobran�a
        /// </summary>
        Beneficiario Beneficiario { get; set; }

        int Codigo { get; }
        string Nome { get; }
        string Digito { get; }

        List<string>
            IdsRetornoCnab400RegistroDetalhe
        {
            get;
        } // Identifica os registros que cada banco implementa no arquivo retorno, sendo que o primeiro ID da List<> identifica um novo boleto dentro do arquivo retorno.

        bool RemoveAcentosArquivoRemessa { get; }

        /// <summary>
        ///     Formata o benefici�rio (Ag�ncia, Conta, C�digo)
        /// </summary>
        void FormataBeneficiario();

        /// <summary>
        ///     Formata o campo livre do c�digo de barras
        /// </summary>
        string FormataCodigoBarraCampoLivre(Boleto.Boleto boleto);

        /// <summary>
        ///     Formata o nosso n�mero
        /// </summary>
        void FormataNossoNumero(Boleto.Boleto boleto);

        /// <summary>
        ///     Respons�vel pela valida��o de todos os dados referente ao banco, que ser�o usados no boleto
        /// </summary>
        void ValidaBoleto(Boleto.Boleto boleto);

        /// <summary>
        ///     Gera o header do arquivo de remessa
        /// </summary>
        string GerarHeaderRemessa(TipoArquivo tipoArquivo, int numeroArquivoRemessa, ref int numeroRegistro);

        /// <summary>
        ///     Gera o Trailer do arquivo de remessa
        /// </summary>
        string GerarDetalheRemessa(TipoArquivo tipoArquivo, Boleto.Boleto boleto, ref int numeroRegistro);

        /// <summary>
        ///     Gera o Trailer do arquivo de remessa
        /// </summary>
        string GerarTrailerRemessa(TipoArquivo tipoArquivo, int numeroArquivoRemessa,
            ref int numeroRegistroGeral, decimal valorBoletoGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);

        string FormatarNomeArquivoRemessa(int numeroSequencial);
    }

    /// <summary>
    ///     Implementa Remessa e Tetorno de Cobran�a no Formato CNAB400
    /// </summary>
    public interface IBancoCNAB400 : IBanco
    {
        //remessa
        string GerarHeaderRemessaCNAB400(ref int numeroArquivoRemessa, ref int numeroRegistroGeral);
        string GerarDetalheRemessaCNAB400(Boleto.Boleto boleto, ref int registro);

        string GerarTrailerRemessaCNAB400(int numeroRegistroGeral, decimal valorBoletoGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);

        //retorno
        void LerHeaderRetornoCNAB400(string registro);
        void LerDetalheRetornoCNAB400Segmento1(ref Boleto.Boleto boleto, string registro);
        void LerDetalheRetornoCNAB400Segmento7(ref Boleto.Boleto boleto, string registro);
        void LerTrailerRetornoCNAB400(string registro);
    }

    /// <summary>
    ///     Implementa Remessa e Retorno de Cobran�a no formato CNAB240 em uma Intitui��o Financeira
    /// </summary>
    public interface IBancoCNAB240 : IBanco
    {
        /// <summary>
        ///     1 - Header de Remessa e Lote do Arquivo de Remessa
        /// </summary>
        /// <param name="numeroArquivoRemessa"></param>
        /// <param name="numeroRegistro"></param>
        /// <returns></returns>
        string GerarHeaderRemessaCNAB240(ref int numeroArquivoRemessa, ref int numeroRegistro);

        string GerarHeaderLoteRemessaCNAB240(ref int numeroArquivoRemessa, ref int numeroRegistro);
        string GerarDetalheRemessaCNAB240(Boleto.Boleto boleto, ref int registro);

        string GerarTrailerLoteRemessaCNAB240(ref int numeroArquivoRemessa, int numeroRegistroGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);

        string GerarTrailerRemessaCNAB240(int numeroRegistroGeral, decimal valorBoletoGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);

        void LerHeaderRetornoCNAB240(ArquivoRetorno arquivoRetorno, string registro);
        void LerDetalheRetornoCNAB240SegmentoT(ref Boleto.Boleto boleto, string registro);
        void LerDetalheRetornoCNAB240SegmentoU(ref Boleto.Boleto boleto, string registro);
        void LerDetalheRetornoCNAB240SegmentoA(ref Boleto.Boleto boleto, string regitro);
    }


    /// <summary>
    ///     Implementa Remessa e Retorno de Cobran�a no formato CNAB150 em uma Intitui��o Financeira
    /// </summary>
    public interface IBancoCNAB150 : IBanco
    {
        /// <summary>
        ///     1 - Header de Remessa e Lote do Arquivo de Remessa
        /// </summary>
        /// <param name="numeroArquivoRemessa"></param>
        /// <param name="numeroRegistro"></param>
        /// <returns></returns>
        string GerarHeaderRemessaCNAB150(ref int numeroArquivoRemessa, ref int numeroRegistro);

        string GerarDetalheRemessaCNAB150(Boleto.Boleto boleto, ref int registro);

        string GerarTrailerLoteRemessaCNAB150(ref int numeroArquivoRemessa, int numeroRegistroGeral,
            decimal valorBoletoGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);

        string GerarTrailerRemessaCNAB150(int numeroRegistroGeral, decimal valorBoletoGeral,
            int numeroRegistroCobrancaSimples, decimal valorCobrancaSimples,
            int numeroRegistroCobrancaVinculada, decimal valorCobrancaVinculada,
            int numeroRegistroCobrancaCaucionada, decimal valorCobrancaCaucionada,
            int numeroRegistroCobrancaDescontada, decimal valorCobrancaDescontada);
    }

    /// <summary>
    ///     Implementa Registro Online de Boleto
    /// </summary>
    public interface IBancoOnlineRest : IBanco
    {
        /// <summary>
        ///     Chave de Acesso (ApiKey), geralmente utilizado para gerar o token de autenticacao
        ///     No caso do sicredi por exemplo, a nomenclatura utilizada é Chave Master
        /// </summary>
        string ChaveApi { get; set; }

        /// <summary>
        ///     Token de autenticacao com validade temporaria
        ///     No caso do sicredi por exemplo, a nomenclatura utilizada é Chave de Transação
        /// </summary>
        string Token { get; set; }

        Task<string> GerarToken();
        Task RegistrarBoleto(Boleto.Boleto boleto);
        Task ConsultarStatus(Boleto.Boleto boleto);
    }
}