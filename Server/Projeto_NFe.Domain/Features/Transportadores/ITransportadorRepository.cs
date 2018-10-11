using System.Linq;

namespace Projeto_NFe.Domain.Features.Transportadores
{
    public interface ITransportadorRepository
    {
        Transportador Add(Transportador transportador);

        bool Update(Transportador transportador);

        Transportador GetById(long id);

        IQueryable<Transportador> GetAll();

        bool Delete(long transportador);
    }
}
