using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Domain.Features.Produtos;
using System.Collections.Generic;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {

        public static Produto ProdutoDefault()
        {
            return new Produto
            {
                CodigoProduto = "001",
                Descricao = "DescricaoProduto",
                Quantidade = 5,
                ValorUnitario = 100.00
            };
        }

        public static Produto ProdutoDefaultWithId
        {
            get
            {
                return new Produto
                {
                    Id = 4,
                    CodigoProduto = "001",
                    Descricao = "DescricaoProduto",
                    Quantidade = 5,
                    ValorUnitario = 100.00
                };
            }

        }

        public static List<Produto> DefaultListProduto
        {
            get
            {
                return new List<Produto>
                {
                    ProdutoDefault()
                };
            }
        }


        public static List<Produto> DefaultListProdutoWithId
        {
            get
            {
                return new List<Produto>
                {
                    ProdutoDefaultWithId
                };
            }
        }

        public static List<Produto> ListProdutoUpdateNota
        {
            get
            {
                List<Produto> produtoList = new List<Produto>();

                produtoList.Add(new Produto()
                {
                    Id = 1,
                    CodigoProduto = "002",
                    Descricao = "DescricaoProduto",
                    Quantidade = 1,
                    ValorUnitario = 100.00

                });

                produtoList.Add(new Produto()
                {
                    Id = 2,
                    CodigoProduto = "003",
                    Descricao = "DescricaoProduto",
                    Quantidade = 5,
                    ValorUnitario = 200.00

                });

                return produtoList;
            }
        }
        public static ProdutoAddCommand GetProdutoValidoParaRegistrar()
        {
            return new ProdutoAddCommand()
            {
                CodigoProduto = "003",
                Descricao = "DescricaoProduto",
                Quantidade = 5,
                ValorUnitario = 200.00
            };
        }

        public static ProdutoUpdateCommand GetProdutoValidoParaAtualizar()
        {
            return new ProdutoUpdateCommand()
            {
                Id = 1,
                CodigoProduto = "003",
                Descricao = "Colher de Pau",
                Quantidade = 5,
                ValorUnitario = 200.00
            };
        }

        public static ProdutoDeleteCommand GetProdutoValidoParaDeletar()
        {
            return new ProdutoDeleteCommand()
            {
                ProdutoIds = new int[] { 1 }
            };
        }
    }

}
