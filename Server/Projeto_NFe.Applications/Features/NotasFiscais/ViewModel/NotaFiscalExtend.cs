using Projeto_NFe.Applications.Features.Destinatarios.ViewModels;
using Projeto_NFe.Applications.Features.Emitentes.ViewModel;
using Projeto_NFe.Applications.Features.Transportadores.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.NotasFiscais.ViewModel
{
    public class NotaFiscalExtend
    {
        public long Id { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrada { get; set; }
        public virtual TransportadoraNaNotaFiscalViewModel Transportador { get; set; }
        public virtual EmitenteNaNotaFiscalViewModel Emitente { get; set; }
        public virtual DestinatarioNaNotaFiscalViewModel Destinatario { get; set; }
        public double PrecoTotal { get; set; }
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }
        public double ValorDoFrete { get; set; }
        public double ValorTotalDosProdutos { get; set; }
        public double ValorTotalDaNota { get; set; }
    }
}
