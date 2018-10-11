namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class CNPJInvalidoValorInvalidoException : DomainException
    {
        public CNPJInvalidoValorInvalidoException() : base("CNPJ invalido")
        {
        }
    }
}
