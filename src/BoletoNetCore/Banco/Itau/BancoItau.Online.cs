using System;
using System.Threading.Tasks;

namespace BoletoNetCore.Banco.Itau
{
    internal partial class BancoItau : IBancoOnlineRest
    {
        public string ChaveApi { get; set; }
        public string Token { get; set; }

        public Task ConsultarStatus(Boleto.Boleto boleto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     TODO: Necessário verificar quais os métodos necessários
        /// </summary>
        /// <returns></returns>
        public Task<string> GerarToken()
        {
            throw new NotImplementedException();
        }

        public Task RegistrarBoleto(Boleto.Boleto boleto)
        {
            throw new NotImplementedException();
        }
    }
}