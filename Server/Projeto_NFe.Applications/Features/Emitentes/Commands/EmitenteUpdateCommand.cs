using FluentValidation;
using FluentValidation.Results;
using Projeto_NFe.Applications.Features.Enderecos;
using Projeto_NFe.Domain.Base.NumeroDeDocumentos;
using Projeto_NFe.Domain.Features.Enderecos;

namespace Projeto_NFe.Applications.Features.Emitentes.Commands
{
    public class EmitenteUpdateCommand
    {
        public int Id { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NumeroDoDocumento { get; set; }
        public EnderecoAddCommand Endereco { get; set; }


        public virtual ValidationResult Validar()
        {
            return new EmitenteUpdateCommandValidator().Validate(this);
        }
        class EmitenteUpdateCommandValidator : AbstractValidator<EmitenteUpdateCommand>
        {
            public EmitenteUpdateCommandValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(e => e.InscricaoEstadual).NotNull().NotEmpty();
                RuleFor(e => e.InscricaoMunicipal).NotNull().NotEmpty();
                RuleFor(e => e.Endereco).NotNull().NotEmpty();
                RuleFor(e => e.NomeRazaoSocial).NotNull().NotEmpty();
                RuleFor(e => e.NumeroDoDocumento).NotNull().NotEmpty();
            }
        }

    }
}
