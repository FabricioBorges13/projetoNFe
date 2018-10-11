using FluentValidation;
using FluentValidation.Results;
using Projeto_NFe.Applications.Features.Enderecos;

namespace Projeto_NFe.Applications.Features.Transportadores.Commands
{
    public class TransportadorAddCommand
    {
        public virtual string NomeRazaoSocial { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string NumeroDoDocumento { get; set; } 
        public virtual bool ResponsabilidadeFrete { get; set; }
        public virtual EnderecoAddCommand Endereco { get; set; }

        public virtual ValidationResult Validation()
        {
            return new TransportadorAddCommandValidator().Validate(this);
        }

        class TransportadorAddCommandValidator : AbstractValidator<TransportadorAddCommand>
        {
            public TransportadorAddCommandValidator()
            {
                RuleFor(x => x.InscricaoEstadual).NotNull();
                RuleFor(x => x.NomeRazaoSocial).NotNull();
                RuleFor(x => x.NumeroDoDocumento).NotNull();
                RuleFor(x => x.ResponsabilidadeFrete).NotNull();
                RuleFor(x => x.Endereco).NotNull();
            }
        }
    }
}
