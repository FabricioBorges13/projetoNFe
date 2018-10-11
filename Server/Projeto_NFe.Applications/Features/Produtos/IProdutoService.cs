using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.Query;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Produtos
{
    public interface IProdutoService
    {
        long Add(ProdutoAddCommand objeto);
        IQueryable<Produto> GetAll();
        Produto GetById(long Id);
        bool Update(ProdutoUpdateCommand objeto);
        bool Delete(ProdutoDeleteCommand objeto);

    }
}
