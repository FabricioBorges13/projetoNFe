using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Domain.Features.Destinatarios;
using System.Linq;

namespace Projeto_NFe.Applications.Features.Destinatarios
{
    public interface IDestinatarioService
    {
        int Add(DestinatarioAddCommand objeto);
        
        IQueryable<Destinatario> GetAll();

        Destinatario GetById(long Id);

        bool Update(DestinatarioUpdateCommand objeto);

        bool Delete(DestinatarioDeleteCommand objeto);
    }
}
