using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Applications.Features.Destinatarios.Querries;
using Projeto_NFe.Domain.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
