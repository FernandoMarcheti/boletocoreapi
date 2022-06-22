using BoletoNetCore.Boleto;
using System;

namespace api.Models
{
    public enum Bancos
    {
        BancodoBrasil = 1,
        Banrisul = 41,
        Basa = 3,
        Bradesco = 237,
        BRB = 70,
        Caixa = 104,
        HSBC = 399,
        Itau = 341,
        Real = 356,
        Safra = 422,
        Santander = 33,
        Sicoob = 756,
        Sicred = 748,
        Sudameris = 347,
        Unibanco = 409
    }

    public class Exemplos
    {

        //public Exemplos(int Banco)
        //{
        //    boletoBancario = new BoletoBancario();
        //    boletoBancario.CodigoBanco = (short)Banco;

        //}
        //public BoletoBancario boletoBancario { get; set; }

        //// **************************************************

        //public Retorno Credcitrus(Conta obj, Boletos boletos, string codigo, int digitoCodigo)
        //{
       


        //    string nossoNumero = obj.nossoNumero.ToString().PadLeft(7, '0');
        //    string nDocumento = obj.nossoNumero.ToString().PadLeft(9, '0');

        //    var b = new Boleto(vencimento, (decimal)obj.valor, "1", nossoNumero, c);

        //    b.NumeroParcela = 1;

        //    b.ModalidadeCobranca = 1;
        //    b.TipoModalidade = "01";
        //    b.NumeroParcela = 1;

        //    b.Remessa = new Remessa(TipoOcorrenciaRemessa.EntradaDeTitulos);
        //    b.DataDocumento = DateTime.Now;
        //    b.Banco = new Banco(756);
        //    b.NumeroDocumento = nDocumento;
        //    b.EspecieDocumento = new EspecieDocumento_Sicoob("1");
        //    b.Aceite = "A";

        //b.Sacado = new Sacado((string) obj.cliente.cpfCnpj, (string) obj.cliente.razaoSocial);
        //b.Sacado.Endereco.End = (string) obj.cliente.endereco;
        //b.Sacado.Endereco.Bairro = (string) obj.cliente.bairro;
        //b.Sacado.Endereco.Cidade = (string) obj.cliente.cidade;
        //b.Sacado.Endereco.CEP = (string) obj.cliente.cep;
        //b.Sacado.Endereco.UF = (string) obj.cliente.uf;

        //    Instrucao_Sicoob protestar = new Instrucao_Sicoob((int)EnumInstrucoes_Sicoob.Protestar5DiasUteis, 5);
        //    if (obj.cedente.jurosMora > 0)
        //    {
        //        Instrucao_Sicoob juros = new Instrucao_Sicoob((int)EnumInstrucoes_Sicoob.CobrarJuros, (double)obj.cedente.jurosMora);
        //        b.Instrucoes.Add(juros);
        //        b.PercJurosMora = obj.cedente.jurosMora;
        //    }

        //    b.Instrucoes.Add(protestar);

        //    if (obj.cedente.valorMulta > 0)
        //    {
        //        Instrucao_Sicoob multa = new Instrucao_Sicoob((int)EnumInstrucoes_Sicoob.ValorMulta, obj.cedente.valorMulta);
        //        b.Instrucoes.Add(multa);
        //    }

        //    boletoBancario.Boleto = b;

        //    List<Lancamento> lancamentos = obj.lancamentos;

        //    boletoBancario.Boleto = b;
        //    boletoBancario.Boleto.JurosMora = obj.cedente.jurosMora;
        //    boletoBancario.Boleto.ValorMulta = obj.cedente.valorMulta;

        //    boletoBancario.Boleto.Valida();

        //    if (obj.valor > 0)
        //        boletos.Add(boletoBancario.Boleto);

        //    var retorno = boletoBancario.MontaHtmlEmbedded();
        //    retorno = retorno.Replace("<tr class=\"ct\">", "<tr class=\"ct\" style=\"height: 12px;\" >");
        //    retorno = formatarBoleto(retorno);

        //    objRetorno.html = WebUtility.HtmlDecode(retorno);
        //    objRetorno.nossoNumero = obj.nossoNumero.ToString().PadLeft(7, '0');
        //    objRetorno.numeroDocumento = obj.nossoNumero.ToString().PadLeft(9, '0');
        //    objRetorno.codigoBeneficiario = codigoCliente;
        //    objRetorno.valor = (decimal)obj.valor;
        //    return objRetorno;

        //}

       
        private string formatarBoleto(string boleto)
        {
            var cabecalho = boleto.Substring(0, boleto.IndexOf("<body>")) + "<body>";

            var textoReplace2 = "<td class=\"pL6\">Instruções</td>";
            var rodape = boleto.Substring(boleto.IndexOf(textoReplace2));

            var textoReplace3 = "<table class=\"w666\">";
            rodape = rodape.Substring(rodape.IndexOf(textoReplace3)) + "<div style=\"margin-bottom: 150px;\"> </div>";

            var retorno = cabecalho + rodape;
            return retorno;
        }
        private string formatarBoletoDecimo(string boleto)
        {
            var textoReplace = "Recibo do Pagador</td>";
            var cabecalho = boleto.Substring(0, boleto.IndexOf(textoReplace)) + "Recibo do Pagador</td></tr></table><br />";

            var textoReplace2 = "<td class=\"pL6\">Instruções</td>";
            var rodape = boleto.Substring(boleto.IndexOf(textoReplace2));

            var textoReplace3 = "<table class=\"w666\">";
            rodape = rodape.Substring(rodape.IndexOf(textoReplace3));
            return cabecalho + rodape;
        }
       

        private string CriaSubdiretorio(string path, string subDiretorio)
        {
            string pathString = System.IO.Path.Combine(path, subDiretorio);
            System.IO.Directory.CreateDirectory(pathString);
            return pathString;
        }

        //private void demonstrativo(string obs, BoletoBancario boletoBancario, List<Lancamento> lancamentos, dynamic cliente, dynamic empresa)
        //{
        //    boletoBancario.ExibirDemonstrativo = true;

        //    GrupoDemonstrativo gd = new GrupoDemonstrativo();
        //    ItemDemonstrativo id = new ItemDemonstrativo();
        //    id.Descricao = "<pre style=\"font-size: 14px;\">   Mensalidade  </pre> ";
        //    id.Referencia = cliente.valorMensalidade.ToString();
        //    id.Valor = cliente.valorMensalidade;
        //    gd.Itens.Add(id);
        //    int qtdeLancamentos = lancamentos.Count;
        //    foreach (var item in lancamentos)
        //    {id = new ItemDemonstrativo();
        //            id.Descricao = "<pre style=\"font-size: 14px;z\"> " + item.quantidade + "- " + item.produto.nome + "</pre> ";
        //            id.Referencia = item.valorUnitario.ToString();
        //            id.Valor = item.valorUnitario * item.quantidade;
        //            gd.Itens.Add(id);

        //    }

        //    if (obs != null)
        //        gd.Itens.Add(new ItemDemonstrativo { Descricao = "<pre style=\"font-size: 14px;\"> Observação: " + obs + "</pre> " });

        //    boletoBancario.Boleto.Demonstrativos.Add(gd);

        //    boletoBancario.Boleto.JurosMora = empresa.jurosMora;
        //    boletoBancario.Boleto.ValorMulta = empresa.valorMulta;

        //    List<IInstrucao> descricaoProdutos = new List<IInstrucao>();
        //    string msg = empresa.instrucaoCobranca;
        //    Instrucao instrucao = new Instrucao(1) { Descricao = msg, };
        //    descricaoProdutos.Add(instrucao);
        //    boletoBancario.Boleto.Instrucoes = descricaoProdutos;
        //}

    }
}