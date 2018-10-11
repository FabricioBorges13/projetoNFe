using AutoMapper;
using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Applications.Features.Transportadores.ViewModel;
using Projeto_NFe.Domain.Features.Transportadores;

namespace Projeto_NFe.Applications.Features.Transportadores.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransportadorAddCommand, Transportador>()
                .ForPath(e => e.NumeroDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento));

            CreateMap<Transportador, TransportadorViewModel>()
               .ForMember(e => e.NumeroDocumento, mc => mc.MapFrom(em => em.NumeroDocumento.NumeroDoDocumento));

            CreateMap<Transportador, TransportadoraNaNotaFiscalViewModel>();
            
            CreateMap<TransportadorUpdateCommand, Transportador>()
              .ForPath(e => e.NumeroDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento));

        }
    }
}
