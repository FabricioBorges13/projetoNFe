using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Base.Exceptions
{
    public class NotFoundException : DomainException
    {
        public NotFoundException() : base(CodigosDeErro.NotFound, "Registry not found") { }
    }
}
