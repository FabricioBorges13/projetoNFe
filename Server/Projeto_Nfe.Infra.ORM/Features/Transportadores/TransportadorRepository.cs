using Projeto_Nfe.Infra.ORM.Context;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Transportadores
{
    public class TransportadorRepository : ITransportadorRepository
    {
        ProjetoNFeContext _context;

        public TransportadorRepository(ProjetoNFeContext context)
        {
            _context = context;
        }
        public Transportador Add(Transportador transportador)
        {
            var newTransportador = _context.Transportadores.Add(transportador);
            _context.SaveChanges();
            return newTransportador;
        }

        public Transportador GetById(long id)
        {
            return _context.Transportadores.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Transportador> GetAll()
        {
            return _context.Transportadores;
        }

        public bool Delete(long transportadorId)
        {
            var _transportador = _context.Transportadores.Where(c => c.Id == transportadorId).FirstOrDefault();
            
            if (_transportador == null)
                throw new NotFoundException();
            _context.Entry(_transportador).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public bool Update(Transportador transportador)
        {
            // Altera o estado
            _context.Entry(transportador).State = EntityState.Modified;
            // Salva mudanças
            return _context.SaveChanges() > 0;
        }
    }
    
}
