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

namespace Projeto_NFe.Applications.Features.Transportadores.Commands
{
    public class TransportadorUpdateCommand
    {
        public virtual long Id { get; set; }
        public virtual string NomeRazaoSocial { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string NumeroDoDocumento { get; set; }
        public virtual EnderecoAddCommand Endereco { get; set; }
        public virtual bool ResponsabilidadeFrete { get; set; }

        public virtual ValidationResult Validation()
        {
            return new TransportadorUpdateCommandValidator().Validate(this);
        }

        class TransportadorUpdateCommandValidator : AbstractValidator<TransportadorUpdateCommand>
        {
            public TransportadorUpdateCommandValidator()
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
