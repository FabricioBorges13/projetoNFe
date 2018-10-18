namespace Projeto_NFe.Domain.Base.Identificacao
{
    public class InscricaoEstadualVazioOuNuloException : DomainException
    {
        public InscricaoEstadualVazioOuNuloException() : base("Inscrição não pode ser vazio ou nulo.")
        {
        }
    }
}