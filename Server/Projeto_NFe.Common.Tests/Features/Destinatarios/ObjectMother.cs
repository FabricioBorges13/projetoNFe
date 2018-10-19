using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Domain.Features.Destinatarios;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static Destinatario DestinatarioValido
        {
            get
            {
                return new Destinatario()
                {
                    NumeroDocumento = "078.463.799-76",
                    InscricaoEstadual = "123413213163485484301.16",
                    NomeRazaoSocial = "Roubo e furto",
                    Endereco = enderecoValido,
                };
            }
        }

        public static Destinatario destinatarioValidoWithId
        {
            get
            {
                return new Destinatario()
                {
                    Id = 1,
                    NumeroDocumento = "60.353.014/0001-10",
                    InscricaoEstadual = "123413213163485484301.16",
                    NomeRazaoSocial = "Roubo e furto",
                    Endereco = enderecoValido,
                };
            }
        }

        public static DestinatarioUpdateCommand destinatarioUpdateCommandValidoWithId
        {
            get
            {
                return new DestinatarioUpdateCommand()
                {
                    Id = 1,
                    NumeroDoDocumento = "60.353.014/0001-10",
                    InscricaoEstadual = "123413213163485484301.16",
                    NomeRazaoSocial = "Roubo e furto",
                    Endereco = EnderecoValidoParaAtualizar(),
                };
            }
        }

        public static DestinatarioDeleteCommand destinatarioDeleteCommandValidoWithId
        {
            get
            {
                return new DestinatarioDeleteCommand()
                {
                    DestinatarioIds = new int[] { 1 }
                };
            }
        }

        public static DestinatarioAddCommand destinatarioAddCommandValid
        {
            get
            {
                return new DestinatarioAddCommand()
                {
                    NumeroDoDocumento = "60.353.014/0001-10",
                    InscricaoEstadual = "123413213163485484301.16",
                    NomeRazaoSocial = "Roubo e furto",
                    Endereco = EnderecoValidoParaRegistrar(),
                };
            }
        }



        public static IQueryable<Destinatario> destinatarioDefaultList
        {
            get
            {
                List<Destinatario> destinatarioList = new List<Destinatario>();


                destinatarioList.Add(new Destinatario()
                {
                    Id = 2,
                    NumeroDocumento = "85.935.915/0001-41",
                    NomeRazaoSocial = "Again",
                    InscricaoEstadual = "123413213163485484301.16",
                    Endereco = enderecoValido,
                });

                destinatarioList.Add(new Destinatario()
                {
                    Id = 3,
                    NumeroDocumento = "09.005.154/0001-40",
                    NomeRazaoSocial = "Heroes Come Back",
                    InscricaoEstadual = "22222222222222222.16",
                    Endereco = enderecoValido,
                });
                destinatarioList.Add(new Destinatario()
                {
                    Id = 4,
                    NumeroDocumento = "60.353.014/0001-10",
                    NomeRazaoSocial = "Pegasus Fantasy",
                    InscricaoEstadual = "123413213163485484301.16",
                    Endereco = enderecoValido,
                });

                return destinatarioList.AsQueryable();
            }
        }
    }
}