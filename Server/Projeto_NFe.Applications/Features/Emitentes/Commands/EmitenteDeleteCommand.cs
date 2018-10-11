using FluentValidation;
using FluentValidation.Results;

namespace Projeto_NFe.Applications.Features.Emitentes.Commands
{
    public class EmitenteDeleteCommand
    {
        public virtual int[] EmitenteIds { get; set; }
        public virtual ValidationResult Validar()
        {
            return new EmitenteDeleteCommandValidator().Validate(this);
        }

        class EmitenteDeleteCommandValidator : AbstractValidator<EmitenteDeleteCommand>
        {
            public EmitenteDeleteCommandValidator()
            {
                RuleFor(x => x.EmitenteIds).NotNull();
                RuleFor(x => x.EmitenteIds.Length).GreaterThan(0);
            }
        }
    }
}
