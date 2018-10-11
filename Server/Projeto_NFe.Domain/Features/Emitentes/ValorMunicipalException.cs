using Projeto_NFe.Domain.Base;
using System;
using System.Runtime.Serialization;

namespace Projeto_NFe.Domain.Features.Emitentes
{
    [Serializable]
    public class InscricaoMunicipalEstaVaziaException : DomainException
    {
        public InscricaoMunicipalEstaVaziaException() : base("Emitente precisa que a inscricao Municipal esteja preenchida")
        {
        }
    }
}