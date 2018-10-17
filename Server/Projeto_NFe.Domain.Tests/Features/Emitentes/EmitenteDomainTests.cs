using FluentAssertions;
using NUnit.Framework;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Identificacao;
using Projeto_NFe.Domain.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Tests.Features.Emitentes
{
    [TestFixture]
    public class EmitenteDomainTests
    {
        Emitente _Emitente;

        [SetUp]
        public void Set()
        {
            _Emitente = ObjectMother.GetEmitenteValido();
        }

        [Test]
        public void Dominio_Emitente_Deve_Ser_Valido()
        {
            //Arrange-Action
            Action validarCampos = () => _Emitente.Validar();

            //Assert
            validarCampos.Should().NotThrow<Exception>();
        }

        [Test]
        public void Dominio_Emitente_RazaoSocial_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Emitente.NomeRazaoSocial = "";
            //Action
            Action alvo = () => _Emitente.Validar();
            //Assert
            alvo.Should().Throw<NomeVazioOuNuloException>();
        }

        [Test]
        public void Dominio_Emitente_Endereco_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Emitente.Endereco = null;
            //Action
            Action alvo = () => _Emitente.Validar();
            //Assert
            alvo.Should().Throw<EnderecoVazioOuNuloException>();
        }

        [Test]
        public void Dominio_Emitente_InscricaoMunicipal_Vazio_Validar_DeveJogarException()
        {
            //Arrange
            _Emitente.inscricaoMunicipal = "";
            //Action
            Action alvo = () => _Emitente.Validar();
            //Assert
            alvo.Should().Throw<InscricaoMunicipalEstaVaziaException>();
        }

        [Test]
        public void Dominio_Emitente_ValidarVinculoComNota_Deve_Retornar_excessao_Quando_Tiver_Vinculo()
        {
            //Arrange-Action
            Action actionValidar = () => _Emitente.ValidarVinculoComNota(true);

            actionValidar.Should().Throw<PossuiVinculoComNotaException>();
        }

        [Test]
        public void Dominio_Destinatario_ValidarVinculoComNota_Nao_Deve_Retornar_excessao_Quando_Nao_Tiver_Vinculo()
        {
            //Arrange-Action
            Action actionValidar = () => _Emitente.ValidarVinculoComNota(false);

            actionValidar.Should().NotThrow<PossuiVinculoComNotaException>();
        }
    }
}
