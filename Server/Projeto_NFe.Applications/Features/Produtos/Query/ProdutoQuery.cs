using System;

namespace Projeto_NFe.Applications.Features.Produtos.Query
{
    public class ProdutoQuery
    {
        public virtual int Quantidade { get; set; }

        public ProdutoQuery(int _quantidade)
        {
            Quantidade = _quantidade;
        }

        public static implicit operator ProdutoQuery(int v)
        {
            throw new NotImplementedException();
        }
    }
}
