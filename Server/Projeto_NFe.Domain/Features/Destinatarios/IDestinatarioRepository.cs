using System.Linq;

namespace Projeto_NFe.Domain.Features.Destinatarios
{
    public interface IDestinatarioRepository
    {
        Destinatario Add(Destinatario objeto);

        IQueryable<Destinatario> GetAll();
        
        Destinatario GetById(long Id);

        bool Update(Destinatario objeto);

        bool Delete(long objeto);
    }
}
