using Projeto_Nfe.Infra.ORM.Context;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Produtos
{
    public class ProdutoRepository : IProdutoRepository
    {
        private ProjetoNFeContext _context;

        public ProdutoRepository(ProjetoNFeContext context)
        {
            _context = context;
        }

        public Produto Add(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return produto;
        }

        public bool Delete(long id)
        {
            var contaCorrente = _context.Produtos.Where(c => c.Id == id).FirstOrDefault();
            if (contaCorrente == null)
                throw new NotFoundException();
            _context.Entry(contaCorrente).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public IQueryable<Produto> GetAll()
        {
            return _context.Produtos;
        }

        public Produto GetById(long Id)
        {
            return _context.Produtos.Find(Id);
        }

        public bool Update(Produto objeto)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(objeto);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                _context.Produtos.Attach(objeto);
            }

            return _context.SaveChanges() > 0;
        }
    }
}
