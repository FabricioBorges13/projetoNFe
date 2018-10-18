namespace Projeto_NFe.Domain.Base.Identificacao
{
    public class EnderecoVazioOuNuloException : DomainException
    {
        public EnderecoVazioOuNuloException() : base("Endereço não pode ser vazio ou nulo.")
        {
        }
    }
}