using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Destinatarios.Commands
{
    public class DestinatarioDeleteCommand
    {
        public virtual int[] DestinatarioIds { get; set; }

        public virtual ValidationResult Validar()
        {
            return new DeleteDestinatarioCommandValidator().Validate(this);
        }
        class DeleteDestinatarioCommandValidator: AbstractValidator<DestinatarioDeleteCommand>
        {
            public DeleteDestinatarioCommandValidator()
            {
                RuleFor(x => x.DestinatarioIds).NotNull();
                RuleFor(x => x.DestinatarioIds.Length).GreaterThan(0);
            }
        }
    }

    
}
