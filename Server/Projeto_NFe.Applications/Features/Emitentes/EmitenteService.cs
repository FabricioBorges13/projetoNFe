using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Emitentes;

namespace Projeto_NFe.Applications.Features.Emitentes
{
    public class EmitenteService : IEmitenteService
    {
        IEmitenteRepository _repository;
        public EmitenteService(IEmitenteRepository repository)
        {
            _repository = repository;
        }
        public long Add(EmitenteAddCommand emitente)
        {
            var _emitente = Mapper.Map<EmitenteAddCommand, Emitente>(emitente);
            var novoDestinatario = _repository.Add(_emitente);

            return novoDestinatario.Id;
        }

        public bool Update(EmitenteUpdateCommand emitente)
        {
            var _emitente = _repository.GetById(emitente.Id);
            if (_emitente == null)
                throw new NotFoundException();

            var updateEmitente = Mapper.Map(emitente, _emitente);

            return _repository.Update(updateEmitente);
        }

        public bool Delete(EmitenteDeleteCommand emitentes)
        {
            var isRemovedAll = true;
            foreach (var emitenteId in emitentes.EmitenteIds)
            {
                var isRemoved = _repository.Remove(emitenteId);
                // Aqui poderia dar o tramento adequado, armazenado quais ids foram removidos
                // e quais não forma removidos (e buscar o porquê). 
                // Como é somente um exemplo, vamos somente retornar false (que não foi totalmente concluído)
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public Emitente GetById(long id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<Emitente> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
