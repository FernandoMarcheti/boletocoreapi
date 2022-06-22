using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Produto
    {
        [JsonProperty("nome")]
        public String nome;

        public String getNome() { return nome; }
        public void setNome(String nome) { this.nome = nome; }
    }
}