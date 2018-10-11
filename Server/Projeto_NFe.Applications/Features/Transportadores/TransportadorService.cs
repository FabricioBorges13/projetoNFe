using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Transportadores;

namespace Projeto_NFe.Applications.Features.Transportadores
{
    public class TransportadorService : ITransportadorService
    {
        ITransportadorRepository _repository;
        public TransportadorService(ITransportadorRepository repository)
        {
            _repository = repository;
        }
        public long Add(TransportadorAddCommand transportador)
        {
            var _transportador = Mapper.Map<TransportadorAddCommand, Transportador>(transportador);
            var novoTransportador = _repository.Add(_transportador);

            return novoTransportador.Id;
        }

        public bool Delete(TransportadorDeleteCommand transportadores)
        {
            var isRemovedAll = true;
            foreach (var transportadorId in transportadores.TransportadorIds)
            {
                var isRemoved = _repository.Delete(transportadorId);
                // Aqui poderia dar o tramento adequado, armazenado quais ids foram removidos
                // e quais não forma removidos (e buscar o porquê). 
                // Como é somente um exemplo, vamos somente retornar false (que não foi totalmente concluído)
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public IQueryable<Transportador> GetAll()
        {
            return _repository.GetAll();
        }

        public Transportador GetById(long id)
        {
            return _repository.GetById(id);
        }

        public bool Update(TransportadorUpdateCommand transportador)
        {
            var _transportador = _repository.GetById(transportador.Id);
            if (_transportador == null)
                throw new NotFoundException();

            var updateEmitente = Mapper.Map(transportador, _transportador);

            return _repository.Update(updateEmitente);
        }
    }
}
