namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class CNPJInvalidoPossuiLetrasOuForaDePadraoException : DomainException
    {
        public CNPJInvalidoPossuiLetrasOuForaDePadraoException() : base("CNPJ invalido contem letras ou está fora do padrao")
        {
        }
    }
}
