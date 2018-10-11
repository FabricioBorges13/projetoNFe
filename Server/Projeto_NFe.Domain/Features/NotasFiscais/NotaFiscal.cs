using Projeto_NFe.Base.Domain;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Domain.Base.Imposto;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class NotaFiscal : Entidade
    {
        public string NaturezaOperacao { get; set; }

        public DateTime DataEntrada { get; set; } = new DateTime();

        public virtual List<Produto> Produtos { get; set; }

        public virtual Imposto ImpostoDaNota { get; set; }

        public virtual Emitente Emitente { get; set; }
        public long? EmitenteId { get; set; }

        public virtual Transportador Transportador { get; set; }
        public long? TransportadorId { get; set; }

        public virtual Destinatario Destinatario { get; set; }
        public long? DestinatarioId { get; set; }

        public bool NotaEmitida { get; set; }

        public DateTime DataEmissao { get; set; } = new DateTime();

        public string ChaveAcesso { get; set; }

        public double ValorDoFrete { get; set; }

        public double ValorTotalDosProdutos { get; set; }

        public double ValorTotalDaNota { get; set; }

        public NotaFiscal()
        {
            NotaEmitida = false;
            ImpostoDaNota = new Imposto();
            ChaveAcesso = "";
            Produtos = new List<Produto>();
            DataEmissao = DateTime.MinValue;
        }

        public virtual void EmitirNotaFiscal()
        {
            ValidarNaturezaOperacao();
            ValidarSePossuiChaveDeAcesso();
            ValidarSeANotaJaEstaEmitida();
            ValidarSeANotaEValida();
            ValidarDataDeEntradaMaiorQueDataEmissao();

            CalcularValorTotalDosProdutosEImpostoDaNota();
            CalcularImpostoDeCadaProduto();
            CalcularValorTotalDaNota();

            DataEmissao = DateTime.Now;
            NotaEmitida = true;
        }

        public void ValidarSePossuiChaveDeAcesso()
        {
            if (this.ChaveAcesso.Length != 44)
            {
                throw new ExceptionChaveDeAcessoInvalidaOuAusente();
            }
        }

        public virtual void ValidarSeANotaEValida()
        {

            if (Emitente == null)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Destinatario == null)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Transportador == null)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Produtos.Count == 0)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Emitente.NumeroDocumento == null)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Destinatario.NumeroDocumento == null)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
            if (Emitente.NumeroDocumento.NumeroDoDocumento == Destinatario.NumeroDocumento.NumeroDoDocumento)
            {
                throw new ExceptionNotaFiscalInvalida();
            }
        }

        public void ValidarSeANotaJaEstaEmitida()
        {
            if (NotaEmitida == true)
            {
                throw new ExceptionNotaJaEmitida();
            }
        }

        public void ValidarDataDeEntradaMaiorQueDataEmissao()
        {
            if (DataEmissao.Year != 1753)
            {
                if (DataEntrada > DataEmissao)
                {
                    throw new ExceptionDataDeEntradaMaiorQueDataEmissao();
                }
            }

        }


        public void ValidarNaturezaOperacao()
        {
            if (String.IsNullOrEmpty(NaturezaOperacao))
            {
                throw new ExceptionNaturezaOperacaoVaziaOuNulo();
            }
        }

        public void CalcularValorTotalDosProdutosEImpostoDaNota()
        {
            ImpostoDaNota = new Imposto();

            var valorTotalDosProdutosNaNota = 0.0;

            for (int i = 0; i < Produtos.Count; i++)
            {
                valorTotalDosProdutosNaNota += Produtos[i].ValorTotal;

                ImpostoDaNota.ValorIpi += Produtos[i].Impostos.ValorIpi;
                ImpostoDaNota.ValorIcms += Produtos[i].Impostos.ValorIcms;
            }

            ValorTotalDosProdutos = valorTotalDosProdutosNaNota;
        }

        public void CalcularImpostoDeCadaProduto()
        {
            for (int i = 0; i < Produtos.Count; i++)
            {
                Produtos[i].CalcularImpostoDoProduto();
            }
        }


        public void CalcularValorTotalDaNota()
        {
            ValorTotalDaNota = ImpostoDaNota.ValorIpi + ImpostoDaNota.ValorIcms + ValorDoFrete + ValorTotalDosProdutos;
        }

        public virtual void GerarChaveDeAcesso(DateTime dataDaChaveDeAcesso)
        {
            ValidarSeANotaJaEstaEmitida();

            string chaveDeAcesso = "";

            chaveDeAcesso = dataDaChaveDeAcesso.Ticks.ToString();
            chaveDeAcesso += dataDaChaveDeAcesso.Ticks.ToString();
            chaveDeAcesso += dataDaChaveDeAcesso.Ticks.ToString();

            int diferenca = chaveDeAcesso.Length - 44;
            int numeroDeCorte = chaveDeAcesso.Length - diferenca;

            chaveDeAcesso = chaveDeAcesso.Remove(numeroDeCorte);

            ChaveAcesso = chaveDeAcesso;
        }

    }
}
