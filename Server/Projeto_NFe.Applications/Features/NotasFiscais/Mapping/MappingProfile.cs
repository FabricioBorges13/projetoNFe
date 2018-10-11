using AutoMapper;
using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Applications.Features.NotasFiscais.ViewModel;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Transportadores;

namespace Projeto_NFe.Applications.Features.NotasFiscais.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotaFiscalAddCommand, NotaFiscal>()
                .ForPath(p => p.ImpostoDaNota.ValorIcms, mc => mc.MapFrom(pq => pq.ValorIcms))
                .ForPath(p => p.ImpostoDaNota.ValorIpi, mc => mc.MapFrom(pq => pq.ValorIpi))
                .ForPath(p => p.Emitente.Id, mc => mc.MapFrom(pq => pq.EmitenteId))
                .ForPath(p => p.Transportador.Id, mc => mc.MapFrom(pq => pq.TransportadorId))
                .ForPath(p => p.Destinatario.Id, mc => mc.MapFrom(pq => pq.DestinatarioId));
            
            CreateMap<NotaFiscal, NotaFiscalViewModel>();
                
            CreateMap<NotaFiscal, NotaFiscalExtend>()
                .ForMember(p => p.ValorIcms, mc => mc.MapFrom(pq => pq.ImpostoDaNota.ValorIcms))
                .ForMember(p => p.ValorIpi, mc => mc.MapFrom(pq => pq.ImpostoDaNota.ValorIpi));

            CreateMap<NotaFiscalUpdateCommand, NotaFiscal>()
                .ForPath(p => p.ImpostoDaNota.ValorIcms, mc => mc.MapFrom(pq => pq.ValorIcms))
                .ForPath(p => p.ImpostoDaNota.ValorIpi, mc => mc.MapFrom(pq => pq.ValorIpi))
                .ForPath(p => p.Emitente.Id, mc => mc.MapFrom(pq => pq.EmitenteId))
                .ForPath(p => p.Transportador.Id, mc => mc.MapFrom(pq => pq.TransportadorId))
                .ForPath(p => p.Destinatario.Id, mc => mc.MapFrom(pq => pq.DestinatarioId));

        }
    }
}
