using AutoMapper;
using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.ViewModels;
using Projeto_NFe.Domain.Features.Produtos;

namespace Projeto_NFe.Applications.Features.Produtos.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoAddCommand, Produto>()
                .ForPath(p => p.Impostos.ValorIcms, mc => mc.MapFrom(pq => pq.ValorIcms))
                .ForPath(p => p.Impostos.ValorIpi, mc => mc.MapFrom(pq => pq.ValorIpi));

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(p => p.ValorIcms, mc => mc.MapFrom(pq => pq.Impostos.ValorIcms))
                .ForMember(p => p.ValorIpi, mc => mc.MapFrom(pq => pq.Impostos.ValorIpi));

            CreateMap<ProdutoUpdateCommand, Produto>()
                .ForPath(p => p.Impostos.ValorIcms, mc => mc.MapFrom(pq => pq.ValorIcms))
                .ForPath(p => p.Impostos.ValorIpi, mc => mc.MapFrom(pq => pq.ValorIpi));
        }
    }
}
