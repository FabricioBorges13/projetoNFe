using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;

namespace Projeto_NFe.Applications.Features.NotasFiscais.Commands
{
    public class NotaFiscalAddCommand
    {
        public string NaturezaOperacao { get; set; }

        public DateTime DataEntrada { get; set; }

        public double ValorIpi { get; set; }

        public double ValorIcms { get; set; }

        public long EmitenteId { get; set; }

        public long TransportadorId { get; set; }

        public long DestinatarioId { get; set; }
        
        public double ValorDoFrete { get; set; }
        
    }
}
