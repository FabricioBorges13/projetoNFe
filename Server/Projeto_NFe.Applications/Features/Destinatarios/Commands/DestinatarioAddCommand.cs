using FluentValidation;
using FluentValidation.Results;
using Projeto_NFe.Applications.Features.Enderecos;

namespace Projeto_NFe.Applications.Features.Destinatarios.Commands
{
    public class DestinatarioAddCommand
    {
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NumeroDocumento { get; set; }
        public EnderecoAddCommand Endereco { get; set; }

        public virtual ValidationResult Validar()
        {
            return new AddDestinatarioCommandValidator().Validate(this);
        }
        class AddDestinatarioCommandValidator : AbstractValidator<DestinatarioAddCommand>
        {
            public AddDestinatarioCommandValidator()
            {
                RuleFor(x => x.NomeRazaoSocial).NotNull().NotEmpty();
                RuleFor(x => x.InscricaoEstadual).NotNull();
                RuleFor(x => x.NumeroDocumento).NotNull().NotEmpty();
                RuleFor(x => x.Endereco).NotNull().NotEmpty();
            }
        }
    }
}
