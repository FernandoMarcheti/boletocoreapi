using System.Collections.ObjectModel;

namespace BoletoNetCore.Boleto.DemonstrativoValoresBoleto
{
    public class GrupoDemonstrativo
    {
        #region Fields

        private ObservableCollection<ItemDemonstrativo> _itens;

        #endregion

        #region Public Properties

        public string Descricao { get; set; }

        public ObservableCollection<ItemDemonstrativo> Itens =>
            _itens ?? (_itens = new ObservableCollection<ItemDemonstrativo>());

        #endregion
    }
}