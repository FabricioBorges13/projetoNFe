using System.Linq;

namespace Projeto_NFe.Domain.Features.Produtos
{
    public interface IProdutoRepository
    {
        Produto Add(Produto produto);
        IQueryable<Produto> GetAll();
        Produto GetById(long Id);
        bool Update(Produto objeto);
        bool Delete(long objeto);
    }
}
