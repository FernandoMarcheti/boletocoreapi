using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Lancamento
    {
        [JsonProperty("quantidade")]
        public int quantidade;
        [JsonProperty("produto")]
        public Produto produto;
        [JsonProperty("valorUnitario")]
        public decimal valorUnitario;
    }
}