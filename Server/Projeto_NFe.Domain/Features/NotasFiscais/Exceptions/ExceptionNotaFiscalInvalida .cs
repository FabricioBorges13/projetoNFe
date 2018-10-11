using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class ExceptionNotaFiscalInvalida : DomainException
    {
        public ExceptionNotaFiscalInvalida() : base("Não é possivel emitir a nota, está faltando campos necessários para emissão")
        {

        }
    }
}
