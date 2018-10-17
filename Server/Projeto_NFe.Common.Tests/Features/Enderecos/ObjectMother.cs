using Projeto_NFe.Applications.Features.Enderecos;
using Projeto_NFe.Domain.Features.Enderecos;
using System.Collections.Generic;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static Endereco enderecoValido
        {
            get
            {
                return new Endereco()
                {
                    Bairro = "São Cristovão",
                    Logradouro = "Rua Pará",
                    Estado = "SC",
                    Municipio = "Lages",
                    Pais = "Brasil",
                    Numero = 22,
                };
            }
        }


        public static EnderecoAddCommand EnderecoValidoParaRegistrar()
        {
            return new EnderecoAddCommand()
            {
                Bairro = "Pedro",
                Logradouro = "Rua Jão",
                Estado = "SC",
                Municipio = "Lages",
                Pais = "Brasil",
                Numero = 22,
            };
        }

        public static EnderecoAddCommand EnderecoValidoParaAtualizar()
        {
            return new EnderecoAddCommand()
            {
                Bairro = "Pedro",
                Logradouro = "Rua Jão",
                Estado = "SC",
                Municipio = "Lages",
                Pais = "Brasil",
                Numero = 22,
            };
        }
    }
}
