using Projeto_NFe.Domain.Base.Exceptions;
using System;

namespace Projeto_NFe.Domain.Base
{
    public abstract class DomainException : Exception
    {
        private string v;

        public DomainException(CodigosDeErro codigosDeErro, string message) : base(message)
        {
            CodigosDeErro = codigosDeErro;
        }

        protected DomainException(string v)
        {
            this.v = v;
        }

        public CodigosDeErro CodigosDeErro { get; }
    }
}
