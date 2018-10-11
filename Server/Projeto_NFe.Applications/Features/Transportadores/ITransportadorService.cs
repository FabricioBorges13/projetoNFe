using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Transportadores
{
    public interface ITransportadorService
    {
        long Add(TransportadorAddCommand transportador);

        bool Update(TransportadorUpdateCommand transportador);

        bool Delete(TransportadorDeleteCommand transportador);

        Transportador GetById(long id);

        IQueryable<Transportador> GetAll();
    }
}
