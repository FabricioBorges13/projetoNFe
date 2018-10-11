using System;
using System.Linq;
using AutoMapper;
using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.Query;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Produtos;

namespace Projeto_NFe.Applications.Features.Produtos
{
    public class ProdutoService : IProdutoService
    {
        IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _repository = produtoRepository;
        }

        public long Add(ProdutoAddCommand produto)
        {
            var _produto = Mapper.Map<ProdutoAddCommand, Produto>(produto);
            var novoProduto = _repository.Add(_produto);

            return novoProduto.Id;
        }

        public bool Delete(ProdutoDeleteCommand produtos)
        {
            var isRemovedAll = true;
            foreach (var produtoId in produtos.ProdutoIds)
            {
                var isRemoved = _repository.Delete(produtoId);
                // Aqui poderia dar o tramento adequado, armazenado quais ids foram removidos
                // e quais não forma removidos (e buscar o porquê). 
                // Como é somente um exemplo, vamos somente retornar false (que não foi totalmente concluído)
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public IQueryable<Produto> GetAll()
        {
            return _repository.GetAll();
        }

        public Produto GetById(long Id)
        {
            return _repository.GetById(Id);
        }

        public bool Update(ProdutoUpdateCommand produto)
        {
            var _produto = _repository.GetById(produto.Id);
            if (_produto == null)
                throw new NotFoundException();

            var updateEmitente = Mapper.Map(produto, _produto);

            return _repository.Update(updateEmitente);

        }
    }
}
