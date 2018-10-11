using FluentValidation;
using FluentValidation.Results;

namespace Projeto_NFe.Applications.Features.NotasFiscais.Commands
{
    public class NotaFiscalDeleteCommand
    {
        public virtual int[] NotaFiscalIds { get; set; }

        public virtual ValidationResult Validar()
        {
            return new NotaFiscalDeleteCommandValidator().Validate(this);
        }

        class NotaFiscalDeleteCommandValidator : AbstractValidator<NotaFiscalDeleteCommand>
        {
            public NotaFiscalDeleteCommandValidator()
            {
                RuleFor(x => x.NotaFiscalIds).NotNull();
                RuleFor(x => x.NotaFiscalIds.Length).GreaterThan(0);
            }
        }
    }
}
