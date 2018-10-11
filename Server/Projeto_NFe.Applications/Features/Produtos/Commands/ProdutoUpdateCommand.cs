using FluentValidation;
using FluentValidation.Results;

namespace Projeto_NFe.Applications.Features.Produtos.Commands
{
    public class ProdutoUpdateCommand
    {
        public int Id { get; set; }
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }

        public virtual ValidationResult Validar()
        {
            return new ProdutoUpdateCommandValidator().Validate(this);
        }
        class ProdutoUpdateCommandValidator : AbstractValidator<ProdutoUpdateCommand>
        {
            public ProdutoUpdateCommandValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
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
