using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class ExceptionDataDeEntradaMaiorQueDataEmissao : DomainException
    {
        public ExceptionDataDeEntradaMaiorQueDataEmissao(): base("A data de entrada não pode ser maior que a data de emissão")
        {

        }
    }
}
