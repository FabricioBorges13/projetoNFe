namespace Projeto_NFe.Domain.Base.NumeroDeDocumentos
{
    public class NumeroDoDocumentoInvalido : DomainException
    {
        public NumeroDoDocumentoInvalido() : base("Numero do documento inválido, não é um CPF ou um CNPJ")
        {
        }
    }
}
