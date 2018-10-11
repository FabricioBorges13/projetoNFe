using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Enderecos
{
    public class EnderecoAddCommand
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public virtual ValidationResult Validar()
        {
            return new EnderecoAddCommandValidator().Validate(this);
        }

        class EnderecoAddCommandValidator : AbstractValidator<EnderecoAddCommand>
        {
            public EnderecoAddCommandValidator()
            {
                RuleFor(e => e.Bairro).NotNull().NotEmpty();
                RuleFor(e => e.Estado).NotNull().NotEmpty();
                RuleFor(e => e.Logradouro).NotNull().NotEmpty();
                RuleFor(e => e.Municipio).NotNull().NotEmpty();
                RuleFor(e => e.Numero).NotNull().NotEmpty();
                RuleFor(e => e.Pais).NotNull().NotEmpty();
            }
        }
    }
}
