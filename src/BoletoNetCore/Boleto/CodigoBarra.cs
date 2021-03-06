using System;
using BoletoNetCore.Util;

namespace BoletoNetCore.Boleto
{
    public class CodigoBarra
    {
        /// <summary>
        ///     Representação numérica do Código de Barras, composto por 44 posições
        ///     01 a 03 - 3 - Identificação  do  Banco
        ///     04 a 04 - 1 - Código da Moeda
        ///     05 a 05 – 1 - Dígito verificador do Código de Barras
        ///     06 a 09 - 4 - Fator de vencimento
        ///     10 a 19 - 10 - Valor
        ///     20 a 44 – 25 - Campo Livre
        /// </summary>
        public string CodigoDeBarras
        {
            get
            {
                var codigoSemDv = string.Format("{0}{1}{2}{3}{4}",
                    CodigoBanco,
                    Moeda,
                    FatorVencimento,
                    ValorDocumento,
                    CampoLivre);
                return string.Format("{0}{1}{2}",
                    codigoSemDv.Left(4),
                    DigitoVerificador,
                    codigoSemDv.Right(39));
            }
        }

        /// <summary>
        ///     A linha digitável é composta por cinco campos:
        ///     1º campo
        ///     composto pelo código de Banco, código da moeda, as cinco primeiras posições do campo
        ///     livre e o dígito verificador deste campo;
        ///     2º campo
        ///     composto pelas posições 6ª a 15ª do campo livre e o dígito verificador deste campo;
        ///     3º campo
        ///     composto pelas posições 16ª a 25ª do campo livre e o dígito verificador deste campo;
        ///     4º campo
        ///     composto pelo dígito verificador do código de barras, ou seja, a 5ª posição do código de
        ///     barras;
        ///     5º campo
        ///     Composto pelo fator de vencimento com 4(quatro) caracteres e o valor do documento com 10(dez) caracteres, sem
        ///     separadores e sem edição.
        /// </summary>
        public string LinhaDigitavel { get; set; } = string.Empty;

        /// <summary>
        ///     Código do Banco (3 dígitos)
        /// </summary>
        public string CodigoBanco { get; set; } = string.Empty;

        /// <summary>
        ///     Código da Moeda (9 = Real)
        /// </summary>
        public int Moeda { get; set; } = 9;

        /// <summary>
        ///     Campo Livre - Implementado por cada banco.
        /// </summary>
        public string CampoLivre { get; set; } = string.Empty;

        public long FatorVencimento { get; set; } = 0;

        public string ValorDocumento { get; set; } = string.Empty;

        public string DigitoVerificador
        {
            get
            {
                var codigoSemDv = string.Format("{0}{1}{2}{3}{4}",
                    CodigoBanco,
                    Moeda,
                    FatorVencimento,
                    ValorDocumento,
                    CampoLivre);

                // Calcula Dígito Verificador do Código de Barras
                int pesoMaximo = 9, soma = 0, peso = 2;
                for (var i = codigoSemDv.Length - 1; i >= 0; i--)
                {
                    soma = soma + Convert.ToInt32(codigoSemDv.Substring(i, 1)) * peso;
                    if (peso == pesoMaximo)
                        peso = 2;
                    else
                        peso = peso + 1;
                }

                var resto = soma % 11;

                if (resto <= 1 || resto > 9)
                    return "1";

                return (11 - resto).ToString();
            }
        }
    }
}