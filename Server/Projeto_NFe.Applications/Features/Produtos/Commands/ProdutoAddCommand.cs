using FluentValidation;
using FluentValidation.Results;

namespace Projeto_NFe.Applications.Features.Produtos.Commands
{
    public class ProdutoAddCommand
    {
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }

        public virtual ValidationResult Validar()
        {
            return new ProdutoAddCommandValidator().Validate(this);
        }

        class ProdutoAddCommandValidator : AbstractValidator<ProdutoAddCommand>
        {
            public ProdutoAddCommandValidator()
            {
                RuleFor(x => x.CodigoProduto).NotNull();
                RuleFor(x => x.Descricao).NotNull();
                RuleFor(x => x.Quantidade).GreaterThan(0);
                RuleFor(x => x.ValorUnitario).GreaterThan(0);
                RuleFor(x => x.ValorIpi).GreaterThan(0);
                RuleFor(x => x.ValorIcms).GreaterThan(0);
            }
        }

    }
}
