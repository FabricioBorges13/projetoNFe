using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.Enderecos
{
    public class ExceptionNumeroInvalidoMenorDoQueUm: DomainException
    {
        public ExceptionNumeroInvalidoMenorDoQueUm() : base("Numero tem que ser maior do que zero")
        {

        }
    }
}
