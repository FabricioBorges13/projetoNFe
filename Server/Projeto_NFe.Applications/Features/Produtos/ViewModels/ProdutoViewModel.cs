namespace Projeto_NFe.Applications.Features.Produtos.ViewModels
{
    public class ProdutoViewModel
    {
        public long Id { get; set; }
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }
    }
}
