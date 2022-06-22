using api.Models;
using BoletoNetCore.Arquivo;
using BoletoNetCore.Banco;
using BoletoNetCore.Boleto;
using BoletoNetCore.BoletoImpressao;
using BoletoNetCore.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Api.Controllers
{
    [Route("api/boletos")]
    [ApiController]
    public class BoletoController : ControllerBase
    {

        [HttpPost]
        public IActionResult Post([FromBody] List<Conta> contas)
        {


            Console.WriteLine("chegou");
            Console.WriteLine(contas[0].dataVencimento);
            string nConvenio = "242530";
            string digitoCodigo = "0";
            var beneficiario = contas[0].cedente;
            var cliente = contas[0].cliente;

            var contaBancaria = new ContaBancaria
            {
                Agencia = beneficiario.agencia,
                DigitoAgencia = beneficiario.digitoAgencia,
                Conta = beneficiario.conta,
                DigitoConta = beneficiario.digitoConta,
                CarteiraPadrao = "1",
                VariacaoCarteiraPadrao = "01",
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
            };
Console.WriteLine("chegou2");
            var beneficiarioObj = new Beneficiario
            {
                CPFCNPJ = beneficiario.cnpj,
                Nome = beneficiario.razaoSocial,
                Codigo = nConvenio,
                CodigoDV = digitoCodigo,
                CodigoTransmissao = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = cliente.endereco,
                    LogradouroNumero = "",
                    LogradouroComplemento = "",
                    Bairro = cliente.bairro,
                    Cidade = cliente.cidade,
                    UF = cliente.uf,
                    CEP = cliente.cep
                },
                ContaBancaria = contaBancaria
            };

                
Console.WriteLine("chegou3");
            string boletosHtml = "";
            var response = new HttpResponseMessage();
            List<Retorno> objsRetorno = new List<Retorno>();
            var banco = Banco.Instancia(BoletoNetCore.Banco.Bancos.Sicoob);
            banco.Beneficiario = beneficiarioObj;
            banco.FormataBeneficiario();
            Boletos boletos = new Boletos
            {
                Banco = banco
            };
Console.WriteLine("chegou4");
            foreach (var obj in contas)
            {
                string boleto = "";
                var nossoNumero = obj.nossoNumero.ToString().PadLeft(7, '0');
                var numeroDocumento= obj.nossoNumero.ToString().PadLeft(9, '0');

                if (obj.isDecimoTerceiro)
                {

                } 
                else
                {
                    if (obj.valor == 0) continue;

                    DateTime vencimento = obj.dataVencimento;

                    var html = new StringBuilder();
                    var boletoHtml = "";    
                    Console.WriteLine("chegou5");
                    var boletoGerado = GerarBoleto(boletos.Banco, vencimento, 1, "A", nossoNumero, numeroDocumento, obj);
                    var boletoParaImpressao = new BoletoBancario
                    {
                        Boleto = boletoGerado,
                        OcultarInstrucoes = true,
                        MostrarComprovanteEntrega = false,
                        MostrarEnderecoBeneficiario = true,
                        ExibirDemonstrativo = true,
                        OcultarReciboPagador = true
                    };
                    Console.WriteLine("chegou7");
                    html.Append("<div style=\"page-break-after: always;\">");
                    html.Append(boletoParaImpressao.MontaHtmlEmbedded());
                    
                    Console.WriteLine("chegou");
                    html.Append("</div>");
                    boletoHtml = html.ToString();
                    html.Append("</div>");
                    boletos.Add(boletoGerado);
                    Console.WriteLine("chegou6");

                    Retorno objRetorno = new Retorno();
                    objRetorno.html = WebUtility.HtmlDecode(boletoHtml);
                    objRetorno.nossoNumero = obj.nossoNumero.ToString().PadLeft(7, '0');
                    objRetorno.numeroDocumento = obj.nossoNumero.ToString().PadLeft(9, '0');
                    objRetorno.codigoBeneficiario = nConvenio;
                    objRetorno.valor = (decimal)obj.valor;
                        
                    objsRetorno.Add(objRetorno);
                }

            }

            DateTime d1 = DateTime.Now;
            var nomeAquivo = (d1.ToShortDateString() + d1.ToLongTimeString()).Replace("/", "").Replace(":", "");
            GerarArquivoRemessa(nConvenio, nomeAquivo, beneficiarioObj, contas[0].id, boletos, nomeAquivo, contas[0].observacao);
                

            Console.WriteLine("acabou");
            return Ok(objsRetorno);

        }

        internal static Boleto GerarBoleto(IBanco banco, DateTime vencimento, int i, string aceite, string nossoNumero, string numeroDocumento, Conta conta)
        {
            var cliente = conta.cliente;
            var pagador = new Pagador
            {
                CPFCNPJ = cliente.cpfCnpj,
                Nome = cliente.razaoSocial,
                Observacoes = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = cliente.endereco,
                    LogradouroNumero = "",
                    Bairro = cliente.bairro,
                    Cidade = cliente.cidade,
                    UF = cliente.uf,
                    CEP = cliente.cep
                }
            };
            var boleto = new Boleto(banco)
            {
                Pagador = pagador,
                DataEmissao = DateTime.Now,
                DataProcessamento = DateTime.Now,
                DataVencimento = vencimento,
                ValorTitulo = conta.valor,
                NossoNumero = nossoNumero,
                NumeroDocumento = numeroDocumento,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Aceite = aceite,
                CodigoInstrucao1 = "111111111111111111111111111111",
                CodigoInstrucao2 = "2222222222222222222222222222",
                //DataDesconto = DateTime.Now.AddMonths(i),
                //ValorDesconto = (decimal)(100 * i * 0.10),
                DataMulta = conta.dataVencimento.AddDays(1),
                //PercentualMulta = conta.cedente.jurosMora,
                ValorMulta = conta.cedente.valorMulta < conta.valor ? conta.cedente.valorMulta : 0,
                DataJuros = conta.dataVencimento.AddDays(1),
                PercentualJurosDia = conta.cedente.jurosMora,
                //ValorJurosDia = (decimal)(100 * i * (0.2 / 100)),
                AvisoDebitoAutomaticoContaCorrente = "2",
                MensagemArquivoRemessa = "",
                NumeroControleParticipante = "CHAVEPRIMARIA" + nossoNumero,
                CodigoProtesto = TipoCodigoProtesto.ProtestarDiasCorridos,
                DiasProtesto = 5
            };
            // Mensagem - Instruções do Caixa
            boleto.ImprimirValoresAuxiliares = true;

            boleto.ValidarDados();
            return boleto;
        }
        private void GerarArquivoRemessa(string nConvenio, string nossoNumero, Beneficiario c, int numeroBanco, Boletos boletos, string nomeAquivo, string obs)
        {
            Console.WriteLine("remessa1");
            var path = @"/boletos";
            path = CriaSubdiretorio(path, nossoNumero);
            Console.WriteLine("remessa2");
            path += "/" + nomeAquivo + ".txt";
            var banco = Banco.Instancia(BoletoNetCore.Banco.Bancos.Sicoob);
            Console.WriteLine("remessa3");

            var arquivoRemessa = new ArquivoRemessa(boletos.Banco, TipoArquivo.CNAB240, 1);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                arquivoRemessa.GerarArquivoRemessa(boletos, fileStream);
            }
            Console.WriteLine("remessa4");

        }

        private string CriaSubdiretorio(string path, string subDiretorio)
        {
            string pathString = System.IO.Path.Combine(path, subDiretorio);
            System.IO.Directory.CreateDirectory(pathString);
            return pathString;
        }
    }
}