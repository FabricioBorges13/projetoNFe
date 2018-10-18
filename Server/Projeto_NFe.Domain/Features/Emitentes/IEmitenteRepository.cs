using System.Linq;

namespace Projeto_NFe.Domain.Features.Emitentes
{
    public interface IEmitenteRepository 
    {
        Emitente Add(Emitente emitente);
        bool Update(Emitente emitente);
        bool Remove(long emitenteId);
        Emitente GetById(long emitenteId);
        IQueryable<Emitente> GetAll();
    }
}
