using System;
using System.IO;
using BoletoNetCore.Arquivo;
using BoletoNetCore.Enums;

namespace BoletoNetCore.Boleto
{
    /// <summary>
    ///     Classe Auxiliar com métodos de extensão para executar comandos a partir de um Boleto ou de uma lista de boletos
    /// </summary>
    public static class BoletoHelper
    {
        /// <summary>
        ///     Recupera um arquivo HTML com encoding do banco
        /// </summary>
        /// <param name="boleto"></param>
        /// <returns></returns>
        public static byte[] ImprimirHtml(this Boleto boleto)
        {
            throw new NotImplementedException();
        }

        public static Stream GerarRemessa(this Boletos boletos, int numArquivoRemessa,
            TipoArquivo tipoArquivo = TipoArquivo.CNAB240)
        {
            var rem = new ArquivoRemessa(boletos.Banco, TipoArquivo.CNAB240, numArquivoRemessa);
            var ms = new MemoryStream(2048);
            try
            {
                rem.GerarArquivoRemessa(boletos, ms, false);
                ms.Position = 0;
                return ms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="boletos"></param>
        /// <returns></returns>
        public static byte[] ImprimirLoteHtml(this Boletos boletos)
        {
            throw new NotImplementedException();
        }
    }
}