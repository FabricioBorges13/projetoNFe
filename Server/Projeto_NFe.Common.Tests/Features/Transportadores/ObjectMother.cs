using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {

        public static Transportador transportadorDefault
        {
            get
            {
                return new Transportador
                {
                    NomeRazaoSocial = "Razao",
                    NumeroDocumento = "432.428.468-77",
                    InscricaoEstadual = "Estadual",
                    Endereco = destinatarioValido,
                    ResponsabilidadeFrete = true
                };
            }
        }

        public static Transportador transportadorDefaultWithId
        {
            get
            {
                return new Transportador
                {
                    Id = 1,
                    NomeRazaoSocial = "Razao",
                    NumeroDocumento = "432.428.468-77",
                    InscricaoEstadual = "Estadual",
                    Endereco = destinatarioValido,
                    ResponsabilidadeFrete = true
                };
            }
        }

        public static TransportadorAddCommand transportadorAddCommand
        {
            get
            {
                return new TransportadorAddCommand
                {
                    NomeRazaoSocial = "Razao",
                    NumeroDoDocumento = "432.428.468-77",
                    InscricaoEstadual = "Estadual",
                    ResponsabilidadeFrete = true
                };
            }
        }


        public static TransportadorUpdateCommand transportadorUpdateCommand
        {
            get
            {
                return new TransportadorUpdateCommand
                {
                    NomeRazaoSocial = "Razao",
                    NumeroDoDocumento = "432.428.468-77",
                    InscricaoEstadual = "Estadual",
                    ResponsabilidadeFrete = true
                };
            }
        }

        public static TransportadorDeleteCommand transportadorDeleteCommand
        {
            get
            {
                return new TransportadorDeleteCommand
                {
                    TransportadorIds = new int[] { 1 }
                };
            }
        }
    }
}
