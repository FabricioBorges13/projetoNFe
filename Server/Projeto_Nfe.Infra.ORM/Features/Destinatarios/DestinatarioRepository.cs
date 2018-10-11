using Projeto_Nfe.Infra.ORM.Context;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

namespace Projeto_Nfe.Infra.ORM.Features.Destinatarios
{

    public class DestinatarioRepository: IDestinatarioRepository
    {
        private ProjetoNFeContext _context;

        public DestinatarioRepository(ProjetoNFeContext context)
        {
            _context = context;
                
        }
        public Destinatario Add(Destinatario objeto)
        {
            _context.Destinatarios.Add(objeto);
            _context.SaveChanges();
            return objeto;
        }

        public bool Delete(long objeto)
        {
            var destinatario = _context.Destinatarios.Where(c => c.Id == objeto).FirstOrDefault();
            if (destinatario == null)
                throw new NotFoundException();
                _context.Entry(destinatario).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public IQueryable<Destinatario> GetAll()
        {
            return _context.Destinatarios;
        }

        public Destinatario GetById(long Id)
        {
            return _context.Destinatarios.FirstOrDefault(d => d.Id == Id);
        }

        public bool Update(Destinatario objeto)
        {
            _context.Entry(objeto).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
    }
}