using FluentAssertions;
using NUnit.Framework;
using Projeto_NFe.Domain.Base.Imposto;
using System;

namespace Projeto_NFe.Domain.Tests.Features.Impostos
{
    [TestFixture]
    public class ImpostoDomainTest
    {
        Imposto _impostoDefault;

        [SetUp]
        public void ImpostoDominioSet()
        {
            _impostoDefault = new Imposto(10);
        }

        [Test]
        public void Dominio_Imposto_Deve_Calcular_ValorICMS_Deve_Ser_Ok()
        {
            _impostoDefault.Validar();
            _impostoDefault.ValorIcms.Should().Be(0.4);
        }

        [Test]
        public void Dominio_Imposto_Deve_Calcular_ValorIPI_Deve_Ser_Ok()
        {
            _impostoDefault.Validar();
            _impostoDefault.ValorIpi.Should().Be(1);
        }

        [Test]
        public void Dominio_Imposto_Deve_Calcular_ValorICMS_Deve_RetornarExcessao()
        {
            //Arrange 
            _impostoDefault = new Imposto(0);
            _impostoDefault.ValorIpi = 1;
            //Action
            Action validarCampos = () => _impostoDefault.Validar();

            //Assert
            validarCampos.Should().Throw<ValorImpostoInvalido>();
        }

        [Test]
        public void Dominio_Imposto_Deve_Calcular_ValorIPI_Deve_RetornarExcessao()
        {
            //Arrange 
            _impostoDefault = new Imposto(0);
            _impostoDefault.ValorIcms = 1;

            //Action
            Action validarCampos = () => _impostoDefault.Validar();

            //Assert
            validarCampos.Should().Throw<ValorImpostoInvalido>();
        }
    }
}