using System.Collections.Generic;
using System.Text;

namespace BoletoNetCore.Util
{
    /// <summary>
    ///     Classe representativa de um registro (linha) de um arquivo EDI
    /// </summary>
    public class TRegistroEDI
    {
        #region Variáveis Privadas e Protegidas

        protected TTipoRegistroEDI _TipoRegistro;
        protected int _TamanhoMaximo = 0;
        protected char _CaracterPreenchimento = ' ';
        protected List<TCampoRegistroEDI> _CamposEDI = new List<TCampoRegistroEDI>();

        #endregion

        #region Propriedades

        /// <summary>
        ///     Tipo de Registro da linha do arquivo EDI
        /// </summary>
        public TTipoRegistroEDI TipoRegistro => _TipoRegistro;

        /// <summary>
        ///     Seta a linha do registro para a decodificação nos campos;
        ///     Obtém a linha decodificada a partir dos campos.
        /// </summary>
        public string LinhaRegistro { get; set; }

        /// <summary>
        ///     Coleção dos campos do registro EDI
        /// </summary>
        public List<TCampoRegistroEDI> CamposEDI
        {
            get => _CamposEDI;
            set => _CamposEDI = value;
        }

        #endregion

        #region Métodos Públicos

        public void Adicionar(TTiposDadoEDI tipo, int posicao, int tamanho, int decimais, object valor,
            char prenchimento)
        {
            CamposEDI.Add(new TCampoRegistroEDI(tipo, posicao, tamanho, decimais, valor, prenchimento));
        }

        /// <summary>
        ///     Codifica uma linha a partir dos campos; o resultado irá na propriedade LinhaRegistro
        /// </summary>
        public virtual void CodificarLinha()
        {
            var builder = new StringBuilder();
            foreach (var campos in _CamposEDI)
            {
                campos.CodificarNaturalParaEDI();
                builder.Append(campos.ValorFormatado);
            }

            LinhaRegistro = builder.ToString();
        }

        /// <summary>
        ///     Decodifica uma linha a partir da propriedade LinhaRegistro nos campos do registro
        /// </summary>
        public virtual void DecodificarLinha()
        {
            foreach (var campos in _CamposEDI)
            {
                if (_TamanhoMaximo > 0) LinhaRegistro = LinhaRegistro.PadRight(_TamanhoMaximo, _CaracterPreenchimento);
                campos.ValorFormatado = LinhaRegistro.Substring(campos.PosicaoInicial, campos.TamanhoCampo);
                campos.DecodificarEDIParaNatural();
            }
        }

        #endregion
    }
}