using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Domain.Features.NotasFiscais;
using System;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static NotaFiscal NotaFiscalValida
        {
            get
            {
                return new NotaFiscal()
                {
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    Produtos = DefaultListProduto,
                    Emitente = EmitenteValidoComId(),
                    Transportador = transportadorDefaultWithId,
                    Destinatario = destinatarioValidoWithId,
                    NotaEmitida = false,
                };
            }
        }

        public static NotaFiscal NotaFiscalValidaComId
        {
            get
            {
                return new NotaFiscal()
                {
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    Produtos = DefaultListProduto,
                    EmitenteId = EmitenteValidoComId().Id,
                    TransportadorId = transportadorDefaultWithId.Id,
                    DestinatarioId = destinatarioValidoWithId.Id,
                    NotaEmitida = false,
                };
            }
        }


        public static NotaFiscalAddCommand NotaFiscalValidaParaRegistro
        {
            get
            {
                return new NotaFiscalAddCommand()
                {
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    EmitenteId = EmitenteValidoComId().Id ,
                    TransportadorId = transportadorDefaultWithId.Id,
                    DestinatarioId = destinatarioValidoWithId.Id,
                    ValorDoFrete = 12,
                    ValorIcms = 12,
                    ValorIpi = 21,
                };
            }
        }

        public static NotaFiscalUpdateCommand NotaFiscalValidaParaAtualizar
        {
            get
            {
                return new NotaFiscalUpdateCommand()
                {
                    Id = 1,
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    EmitenteId = EmitenteValidoComId().Id,
                    TransportadorId = transportadorDefaultWithId.Id,
                    DestinatarioId = destinatarioValidoWithId.Id,
                };
            }
        }

        public static NotaFiscalDeleteCommand NotaFiscalValidaParaDeletar
        {
            get
            {
                return new NotaFiscalDeleteCommand()
                {
                    NotaFiscalIds = new int[] { 1 }
                };
            }
        }


        public static NotaFiscal NotaFiscalValidaWithId
        {
            get
            {
                return new NotaFiscal()
                {
                    Id = 1,
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    Produtos = DefaultListProdutoWithId,
                    Emitente = EmitenteValidoComId(),
                    Transportador = transportadorDefaultWithId,
                    Destinatario = destinatarioValidoWithId,
                    NotaEmitida = false,
                };
            }
        }

        public static NotaFiscal NotaFiscalValidaNaoEmitida
        {
            get
            {
                return new NotaFiscal()
                {
                    DataEntrada = DateTime.Now.AddDays(-1),
                    DataEmissao = DateTime.Now.AddDays(3),
                    NaturezaOperacao = "Comercio de Computadores",
                    Produtos = DefaultListProduto,
                    Emitente = EmitenteValidoComId(),
                    Transportador = transportadorDefaultWithId,
                    Destinatario = destinatarioValidoWithId,
                    NotaEmitida = false,
                };
            }
        }

        public static NotaFiscal NotaFiscalValidaInvalida
        {
            get
            {
                return new NotaFiscal()
                {
                    DataEntrada = DateTime.Now.AddDays(-1),
                    NaturezaOperacao = "Comercio de Computadores",
                    Produtos = DefaultListProduto,
                    Emitente = EmitenteValidoComId(),
                    Transportador = transportadorDefaultWithId,
                    Destinatario = destinatarioValidoWithId,
                    NotaEmitida = false,
                };
            }
        }

    }
}
