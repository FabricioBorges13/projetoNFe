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
    public class NotaFiscalViewModel
    {
        public virtual long Id { get; set; }
        public virtual DateTime DataEmissao { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual double PrecoTotal { get; set; }
        public virtual string NaturezaOperacao { get; set; }
        public virtual TransportadoraNaNotaFiscalViewModel Transportador { get; set; }
        public virtual EmitenteNaNotaFiscalViewModel Emitente { get; set; }
        public virtual DestinatarioNaNotaFiscalViewModel Destinatario { get; set; }

    }
}
