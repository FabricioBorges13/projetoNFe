using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Domain.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Emitentes
{
    public interface IEmitenteService
    {
        long Add(EmitenteAddCommand emitente);

        bool Update(EmitenteUpdateCommand emitente);

        bool Delete(EmitenteDeleteCommand emitente);

        Emitente GetById(long id);

        IQueryable<Emitente> GetAll(int quantidade = 0);
    }
}
