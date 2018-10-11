using AutoMapper;
using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Applications.Features.Emitentes.ViewModel;
using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.ViewModels;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.Produtos;

namespace Projeto_NFe.Applications.Features.Emitentes.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmitenteAddCommand, Emitente>()
                .ForPath(e => e.NumeroDocumento.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento)); 

            CreateMap<Emitente, EmitenteViewModel>()
                .ForMember(e => e.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDocumento.NumeroDoDocumento));

            CreateMap<Emitente, EmitenteNaNotaFiscalViewModel>();

            CreateMap<EmitenteUpdateCommand, Emitente>()
                .ForPath(e => e.NumeroDocumento.NumeroDoDocumento, mc => mc.MapFrom(em => em.NumeroDoDocumento));
        }
    }
}
