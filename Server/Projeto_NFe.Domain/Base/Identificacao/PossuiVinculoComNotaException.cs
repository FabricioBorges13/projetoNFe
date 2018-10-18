namespace Projeto_NFe.Domain.Base.Identificacao
{
    public class PossuiVinculoComNotaException : DomainException
    {
        public PossuiVinculoComNotaException() : base("Não é possível deletar pois possuí vinculo com nota.")
        {
        }
    }
}