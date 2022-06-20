using System.Collections.Generic;
using BoletoNetCore.Banco;

namespace BoletoNetCore.Boleto
{
    public class Boletos : List<Boleto>
    {
        public IBanco Banco { get; set; }
    }
}