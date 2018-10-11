using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class ExceptionNotaJaEmitida : DomainException
    {
        public ExceptionNotaJaEmitida(): base("Não é possivel emitir uma nota já emitida")
        {

        }
    }
}
