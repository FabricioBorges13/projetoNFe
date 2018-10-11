using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.Enderecos
{
    public class ExceptionBairroInvalidoVazioOuNulo : DomainException
    {
        public ExceptionBairroInvalidoVazioOuNulo(): base("Bairro tem que ser preenchido")
        {

        }
    }
}
