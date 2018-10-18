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
            ValidarRazaoSocial();
            ValidarIncricaoEstadual();
            ValidarEndereco();
            ValidarNumeroDocumento();
        }

        private void ValidarNumeroDocumento()
        {
            if (NumeroDocumento == null)
                throw new NumeroDocumentoVazioOuNuloException();
        }

        protected void ValidarRazaoSocial()
        {
            if (string.IsNullOrEmpty(NomeRazaoSocial))
                throw new NomeVazioOuNuloException();
        }

        protected void ValidarIncricaoEstadual()
        {
            if (string.IsNullOrEmpty(InscricaoEstadual))
                throw new InscricaoEstadualVazioOuNuloException();
        }

        protected void ValidarEndereco()
        {
            if (Endereco == null)
                throw new EnderecoVazioOuNuloException();
        }

        public void ValidarVinculoComNota(bool temVinculoNota)
        {
            if (temVinculoNota)
            {
                throw new PossuiVinculoComNotaException();
            }
        }
    }
}
