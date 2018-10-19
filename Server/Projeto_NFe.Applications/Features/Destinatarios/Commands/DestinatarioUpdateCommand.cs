using FluentValidation;
using FluentValidation.Results;
using Projeto_NFe.Applications.Features.Enderecos;
using Projeto_NFe.Domain.Base.NumeroDeDocumentos;
using Projeto_NFe.Domain.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Destinatarios.Commands
{
    public class DestinatarioUpdateCommand
    {
        public int Id { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NumeroDoDocumento { get; set; }
        public EnderecoAddCommand Endereco { get; set; }

        public virtual ValidationResult Validar()
        {
            return new UpdateDestinatarioCommandValidator().Validate(this);
        }
        class UpdateDestinatarioCommandValidator : AbstractValidator<DestinatarioUpdateCommand>
        {
            public UpdateDestinatarioCommandValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(x => x.NomeRazaoSocial).NotNull();
                RuleFor(x => x.NumeroDoDocumento).NotNull();
                RuleFor(x => x.Endereco).NotNull();
            }

        }
    }
}
