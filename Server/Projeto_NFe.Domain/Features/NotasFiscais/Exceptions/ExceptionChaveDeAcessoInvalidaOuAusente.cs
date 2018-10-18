using Projeto_NFe.Domain.Base;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public class ExceptionChaveDeAcessoInvalidaOuAusente : DomainException
    {
        public ExceptionChaveDeAcessoInvalidaOuAusente(): base("A Chave de acesso está invalida ou ausente")
        {

        }
    }
}
