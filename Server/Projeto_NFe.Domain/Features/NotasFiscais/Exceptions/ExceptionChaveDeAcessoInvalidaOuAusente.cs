using Projeto_NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class ExceptionChaveDeAcessoInvalidaOuAusente : DomainException
    {
        public ExceptionChaveDeAcessoInvalidaOuAusente(): base("A Chave de acesso está invalida ou ausente")
        {

        }
    }
}
