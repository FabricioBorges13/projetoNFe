namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class NumeroDocumentoVazioOuNuloException : DomainException
    {
        public NumeroDocumentoVazioOuNuloException() : base("Numero de Documento não pode ser vazio ou nulo.")
        {
        }
        
    }
}