using System;
using Projeto_NFe.Base.Domain;
using Projeto_NFe.Domain.Base.NumeroDeDocumentos;
using Projeto_NFe.Domain.Features.Enderecos;

namespace Projeto_NFe.Domain.Base.Identificacao
{
    public abstract class Identificacao : Entidade
    {
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public NumeroDeDocumento NumeroDocumento { get; set; }
        public Endereco Endereco { get; set; }

        public virtual void Validar()
        {
            ValidarNumeroDocumento();
        }

        private void ValidarNumeroDocumento()
        {
            if (NumeroDocumento == null)
                throw new NumeroDocumentoVazioOuNuloException();
        }
    }
}
