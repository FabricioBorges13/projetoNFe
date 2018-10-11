using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Base.Imposto
{
    public class ValorImpostoInvalido : DomainException
    {
        public ValorImpostoInvalido() : base("Valor do imposto deve ser maior que zero")
        {
        }
    }
}
