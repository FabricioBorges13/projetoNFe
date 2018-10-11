using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.NotasFiscais.Commands
{
    public class NotaFiscalUpdateProdutosCommand
    {
        public long[] ProdutosId { get; set; }
        public long NotaFiscalId { get; set; }
        
        public virtual ValidationResult Validar()
        {
            return new NotaFiscalUpdateCommandValidator().Validate(this);
        }

        class NotaFiscalUpdateCommandValidator : AbstractValidator<NotaFiscalUpdateProdutosCommand>
        {
            public NotaFiscalUpdateCommandValidator()
            {
                RuleFor(x => x.NotaFiscalId).NotNull().GreaterThan(0);
            }
        }

    }
}
