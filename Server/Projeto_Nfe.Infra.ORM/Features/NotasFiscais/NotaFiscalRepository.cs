using Projeto_Nfe.Infra.ORM.Context;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.NotasFiscais
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {

        private ProjetoNFeContext _context;

        public NotaFiscalRepository(ProjetoNFeContext context)
        {
            _context = context;
        }

        public NotaFiscal Add(NotaFiscal notaFiscal)
        {
            _context.NotasFiscais.Add(notaFiscal);
            _context.SaveChanges();
            return notaFiscal;
        }

        public bool Delete(long id)
        {
            var notaFiscal = _context.NotasFiscais.Where(c => c.Id == id).FirstOrDefault();
            if (notaFiscal == null)
                throw new NotFoundException();
            _context.Entry(notaFiscal).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public IQueryable<NotaFiscal> GetAll()
        {
            return _context.NotasFiscais;
        }

        public NotaFiscal GetById(long id)
        {
            return _context.NotasFiscais.Where(a => a.Id == id)
                .Include(a => a.Produtos).FirstOrDefault();
        }
        

        public bool Update(NotaFiscal notaFiscal)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(notaFiscal);
            
            if (dbEntityEntry.State == EntityState.Detached)
            {
                _context.NotasFiscais.Attach(notaFiscal);
            }

            return _context.SaveChanges() > 0;
        }
    }
}
