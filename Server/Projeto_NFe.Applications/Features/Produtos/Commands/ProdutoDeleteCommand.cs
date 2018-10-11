using FluentValidation;
using FluentValidation.Results;

namespace Projeto_NFe.Applications.Features.Produtos.Commands
{
    public class ProdutoDeleteCommand
    {
        public virtual int[] ProdutoIds { get; set; }

        public virtual ValidationResult Validar()
        {
            return new ProdutoDeleteCommandValidator().Validate(this);
        }

        class ProdutoDeleteCommandValidator : AbstractValidator<ProdutoDeleteCommand>
        {
            public ProdutoDeleteCommandValidator()
            {
                RuleFor(x => x.ProdutoIds).NotNull();
                RuleFor(x => x.ProdutoIds.Length).GreaterThan(0);
            }
        }
    }
}
