using AutoMapper;
using Projeto_NFe.Applications.Features.Enderecos.ViewModel;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Enderecos.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EnderecoAddCommand, Endereco>()
                .ForPath(e => e.Bairro, mc => mc.MapFrom(em => em.Bairro))
                .ForPath(e => e.Estado, mc => mc.MapFrom(em => em.Estado))
                .ForPath(e => e.Logradouro, mc => mc.MapFrom(em => em.Logradouro))
                .ForPath(e => e.Municipio, mc => mc.MapFrom(em => em.Municipio))
                .ForPath(e => e.Numero, mc => mc.MapFrom(em => em.Numero))
                .ForPath(e => e.Pais, mc => mc.MapFrom(em => em.Pais));

            CreateMap<Endereco, EnderecoViewModel>();

        }
    }
}
