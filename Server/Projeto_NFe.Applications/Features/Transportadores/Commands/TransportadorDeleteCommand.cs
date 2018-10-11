using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Transportadores.Commands
{
    public class TransportadorDeleteCommand
    {
        public virtual int[] TransportadorIds { get; set; }

        public virtual ValidationResult Validation()
        {
            return new TransportadorDeleteCommandValidator().Validate(this);
        }
    }

    class TransportadorDeleteCommandValidator : AbstractValidator<TransportadorDeleteCommand>
    {
        public TransportadorDeleteCommandValidator()
        {
            RuleFor(x => x.TransportadorIds).NotNull();
            RuleFor(x => x.TransportadorIds.Length).GreaterThan(0);
        }
    }
}
