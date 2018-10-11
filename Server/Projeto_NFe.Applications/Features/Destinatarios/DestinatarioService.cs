using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Applications.Features.Destinatarios.Querries;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Destinatarios;

namespace Projeto_NFe.Applications.Features.Destinatarios
{
    public class DestinatarioService : IDestinatarioService
    {
        IDestinatarioRepository _destinatarioRepository;

        public DestinatarioService(IDestinatarioRepository DestinatarioRepository)
        {
            _destinatarioRepository = DestinatarioRepository;
        }

        public int Add(DestinatarioAddCommand destinatario)
        {
            var destinatarioAdd = Mapper.Map<DestinatarioAddCommand, Destinatario>(destinatario);
            var novoDestinatario = _destinatarioRepository.Add(destinatarioAdd);

            return (int)novoDestinatario.Id;
        }

        public bool Delete(DestinatarioDeleteCommand destinatarios)
        {
            var isRemovedAll = true;
            foreach (var destinatarioId in destinatarios.DestinatarioIds)
            {
                var isRemoved = _destinatarioRepository.Delete(destinatarioId);
                // Aqui poderia dar o tramento adequado, armazenado quais ids foram removidos
                // e quais não forma removidos (e buscar o porquê). 
                // Como é somente um exemplo, vamos somente retornar false (que não foi totalmente concluído)
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public IQueryable<Destinatario> GetAll()
        {
            return _destinatarioRepository.GetAll();
        }

        public Destinatario GetById(long destinatarioId)
        {
            return _destinatarioRepository.GetById(destinatarioId);
        }

        public bool Update(DestinatarioUpdateCommand destinatario)
        {
            var destinatarioDb = _destinatarioRepository.GetById(destinatario.Id);
            if (destinatarioDb == null)
                throw new NotFoundException();

            var destinatarioEdit = Mapper.Map(destinatario, destinatarioDb);

            return _destinatarioRepository.Update(destinatarioEdit);
        }
    }
}
