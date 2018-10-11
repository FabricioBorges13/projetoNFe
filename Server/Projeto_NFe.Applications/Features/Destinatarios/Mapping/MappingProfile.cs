using AutoMapper;
using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Applications.Features.Destinatarios.ViewModels;
using Projeto_NFe.Domain.Features.Destinatarios;

namespace Projeto_NFe.Applications.Features.Destinatarios.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<DestinatarioAddCommand, Destinatario>()
                 .ForPath(e => e.NumeroDocumento.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento)); ;

            CreateMap<Destinatario, DestinatarioViewModel>()
                .ForMember(e => e.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDocumento.NumeroDoDocumento));

            CreateMap<Destinatario, DestinatarioNaNotaFiscalViewModel>();


            CreateMap<DestinatarioUpdateCommand, Destinatario>()
                .ForPath(e => e.NumeroDocumento.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento));
        }
    }
}
