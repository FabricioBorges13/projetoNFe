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
                Endereco = destinatarioValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Filho de Jonh",
                NumeroDocumento = "60.353.014/0001-10",
            };
        }

        public static Emitente EmitenteValidoComId()
        {
            return new Emitente()
            {
                Id = 1,
                Endereco = destinatarioValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDocumento = "078.463.799-76",

            };
        }

        public static List<Emitente> EmitenteValidoListDefault()
        {
            List<Emitente> destinatarioList = new List<Emitente>();

            destinatarioList.Add(new Emitente()
            {
                Id = 1,
                Endereco = destinatarioValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDocumento = "078.463.799-76",
            });

            destinatarioList.Add(new Emitente()
            {
                Id = 2,
                Endereco = destinatarioValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Go Go Power Rangers",
                NumeroDocumento = "078.463.799-76",
            });
            destinatarioList.Add(new Emitente()
            {
                Id = 3,
                Endereco = destinatarioValido,
                InscricaoEstadual = "SC",
                inscricaoMunicipal = "Lages",
                NomeRazaoSocial = "It's Morphing Time",
                NumeroDocumento = "078.463.799-76",
            });

            return destinatarioList;

        }

        public static EmitenteAddCommand GetEmitenteValidoParaRegistrar()
        {
            return new EmitenteAddCommand()
            {
                InscricaoEstadual = "SC",
                InscricaoMunicipal = "Lages",
                NomeRazaoSocial = "Proteger e Servir",
                NumeroDoDocumento = "078.463.799-76",
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
                NumeroDoDocumento = "078.463.799-76",
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
