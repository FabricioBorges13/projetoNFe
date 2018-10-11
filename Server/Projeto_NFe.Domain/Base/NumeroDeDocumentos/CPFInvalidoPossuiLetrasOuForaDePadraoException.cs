namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class CPFInvalidoPossuiLetrasOuForaDePadraoException : DomainException
    {
        public CPFInvalidoPossuiLetrasOuForaDePadraoException() : base("CPF invalido contem letras ou está fora do padrao")
        {
        }
    }
}
