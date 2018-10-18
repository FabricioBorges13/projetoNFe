using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Domain.Features.Emitentes;
using System.Collections.Generic;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static Emitente GetEmitenteValido()
        {
            return new Emitente()
            {
                Endereco = enderecoValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Filho de Jonh",
                NumeroDocumento = "10.202.550/0001-47",
            };
        }

        public static Emitente EmitenteValidoComId()
        {
            return new Emitente()
            {
                Id = 1,
                Endereco = enderecoValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDocumento = "10.202.550/0001-47",

            };
        }


        public static EmitenteAddCommand GetEmitenteValidoParaRegistrar()
        {
            return new EmitenteAddCommand()
            {
                InscricaoEstadual = "SC",
                InscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDoDocumento = "10.202.550/0001-47",
                Endereco = EnderecoValidoParaRegistrar()
            };
        }

        public static EmitenteUpdateCommand GetEmitenteValidoParaAtualizar()
        {
            return new EmitenteUpdateCommand()
            {
                Id = 1,
                InscricaoEstadual = "SC",
                InscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDoDocumento = "10.202.550/0001-47",
                Endereco = EnderecoValidoParaAtualizar()
            };
        }

        public static EmitenteDeleteCommand GetEmitenteValidoParaDeletar()
        {
            return new EmitenteDeleteCommand()
            {
                EmitenteIds = new int[] { 1 }
            };
        }
    }
}
