using FluentValidation;
using FluentValidation.Results;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;

namespace Projeto_NFe.Applications.Features.NotasFiscais.Commands
{
    public class NotaFiscalUpdateCommand
    {
        public long Id { get; set; }
        public string NaturezaOperacao { get; set; }
        public DateTime DataEntrada { get; set; }
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }
        public long EmitenteId { get; set; }
        public long TransportadorId { get; set; }
        public long DestinatarioId { get; set; }

        public double ValorDoFrete { get; set; }

        public virtual ValidationResult Validar()
        {
            return new NotaFiscalUpdateCommandValidator().Validate(this);
        }

        class NotaFiscalUpdateCommandValidator : AbstractValidator<NotaFiscalUpdateCommand>
        {
            public NotaFiscalUpdateCommandValidator()
            {
                RuleFor(x => x.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
