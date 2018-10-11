using Projeto_NFe.Applications.Features.Enderecos;
using Projeto_NFe.Domain.Features.Enderecos;
using System.Collections.Generic;

namespace Projeto_NFe.Common.Tests.Features
{
    public static partial class ObjectMother
    {
        public static Endereco destinatarioValido
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

        public static List<Endereco> DefaultListDeEnderecos
        {
            get
            {
                List<Endereco> destinatarioList = new List<Endereco>();

                destinatarioList.Add(new Endereco()
                {
                    Bairro = "Habitação",
                    Logradouro = "Rua Perdeu Playboy",
                    Estado = "SC",
                    Municipio = "Lages",
                    Pais = "Brasil",
                    Numero = 22,
                });

                destinatarioList.Add(new Endereco()
                {
                    Bairro = "São José",
                    Logradouro = "Rua Cachaça",
                    Estado = "SC",
                    Municipio = "Lages",
                    Pais = "Brasil",
                    Numero = 22,
                });

                destinatarioList.Add(new Endereco()
                {
                    Bairro = "Pedro",
                    Logradouro = "Rua Jão",
                    Estado = "SC",
                    Municipio = "Lages",
                    Pais = "Brasil",
                    Numero = 22,
                });

                return destinatarioList;
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
