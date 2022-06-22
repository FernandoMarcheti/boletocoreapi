using BoletoNetCore.Boleto;

namespace api.Models
{
    public class ListaBoletos
    {
        public Boletos boletos { get; set; }

        public ListaBoletos()
        {
            boletos = new Boletos();
        }
    }
}