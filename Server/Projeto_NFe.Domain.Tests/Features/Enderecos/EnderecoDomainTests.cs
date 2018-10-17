using FluentAssertions;
using NUnit.Framework;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Tests.Features.Enderecos
{
    [TestFixture]
    public class EnderecoDomainTests
    {
        Endereco _Endereco;

        [SetUp]
        public void set()
        {
            _Endereco = ObjectMother.enderecoValido;
        }

        [Test]
        public void Dominio_Endereco_Deve_Ser_Valido()
        {
            //Arrange-Action
            Action validarCampos = () => _Endereco.Validar();

            //Assert
            validarCampos.Should().NotThrow<Exception>();
        }

        [Test]
        public void Dominio_Endereco_Bairro_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Bairro = "";

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionBairroInvalidoVazioOuNulo>();
        }
        [Test]
        public void Dominio_Endereco_Numero_Zerado_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Numero = 0;

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionNumeroInvalidoMenorDoQueUm>();
        }
        [Test]
        public void Dominio_Endereco_Logradouro_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Logradouro = "";

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionLogradouroInvalidoVazioOuNulo>();
        }
        [Test]
        public void Dominio_Endereco_Estado_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Estado = "";

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionEstadoInvalidoVazioOuNulo>();
        }
        [Test]
        public void Dominio_Endereco_Municipio_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Municipio = "";

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionMunicipioInvalidoVazioOuNulo>();
        }
        [Test]
        public void Dominio_Endereco_Pais_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Endereco.Pais = "";

            //Action
            Action alvo = () => _Endereco.Validar();

            //Assert
            alvo.Should().Throw<ExceptionPaisInvalidoVazioOuNulo>();
        }
    }
}
