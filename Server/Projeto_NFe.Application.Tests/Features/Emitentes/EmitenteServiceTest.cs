using FluentAssertions;
using Moq;
using NUnit.Framework;
using Projeto_NFe.Application.Tests.Initializer;
using Projeto_NFe.Applications.Features.Emitentes;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_NFe.Application.Tests.Features.Emitentes
{
    [TestFixture]
    public class EmitenteServiceTest : TestBase
    {
        private Mock<IEmitenteRepository> _repositoryFake;
        private IEmitenteService _service;

        [SetUp]
        public void Initialize()
        {
            _repositoryFake = new Mock<IEmitenteRepository>();
            _service = new EmitenteService(_repositoryFake.Object);
        }

        [Test]
        public void Service_Emitente_AdicionarEmitente_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaRegistrar();
            _repositoryFake.Setup(x => x.Add(It.IsAny<Emitente>()))
                .Returns(emitente);
            //Action
            var novoEmitenteId = _service.Add(emitenteCmd);
            //Verify
            _repositoryFake.Verify(x => x.Add(It.IsAny<Emitente>()), Times.Once);
            novoEmitenteId.Should().Be(emitente.Id);
        }

        [Test]
        public void Service_Emitente_AtualizarEmitente_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaAtualizar();
            var atualizado = true;
            _repositoryFake.Setup(x => x.GetById(emitenteCmd.Id)).Returns(emitente);
            _repositoryFake.Setup(pr => pr.Update(emitente)).Returns(atualizado);
            //Action
            var emitenteAtualizado = _service.Update(emitenteCmd);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(emitenteCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(emitente), Times.Once);
            emitenteAtualizado.Should().BeTrue();
        }

        [Test]
        public void Service_Emitente_AtualizarEmitente_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaAtualizar();
            _repositoryFake.Setup(x => x.GetById(emitenteCmd.Id)).Returns((Emitente)null);
            //Action
            Action act = () => _service.Update(emitenteCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(emitenteCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Emitente>()), Times.Never);
        }

        [Test]
        public void Service_Emitente_Delete_DevePassar()
        {
            //Arrange
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaDeletar();
            var removido = true;
            _repositoryFake.Setup(pr => pr.Remove(emitenteCmd.EmitenteIds[1])).Returns(removido);
            //Action
            var emitenteRemovido = _service.Delete(emitenteCmd);
            //Assert
            _repositoryFake.Verify(pr => pr.Remove(emitenteCmd.EmitenteIds[1]), Times.Once);
            emitenteRemovido.Should().BeTrue();
        }

        [Test]
        public void Service_Emitente_Delete_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaDeletar();
            _repositoryFake.Setup(x => x.Remove(emitenteCmd.EmitenteIds[1])).Throws<NotFoundException>();
            //Action
            Action act = () => _service.Delete(emitenteCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.Remove(emitenteCmd.EmitenteIds[1]), Times.Once);
        }

        [Test]
        public void Service_Emitente_PegarEmitentePorId_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            _repositoryFake.Setup(pr => pr.GetById(emitente.Id)).Returns(emitente);
            //Action
            var recebido = _service.GetById(emitente.Id);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(emitente.Id), Times.Once);
            recebido.Should().NotBeNull();
            recebido.Id.Should().Be(emitente.Id);
        }

        [Test]
        public void Service_Emitente_PegarEmitentePorId_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            _repositoryFake.Setup(pr => pr.GetById(emitente.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.GetById(emitente.Id);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(emitente.Id), Times.Once);
        }

        [Test]
        public void Service_Emitente_PegarTodosOsEmitentes_DevePassar()
        {
            //Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var listaEmitentes = new List<Emitente>() { emitente }.AsQueryable();
            _repositoryFake.Setup(pr => pr.GetAll(0)).Returns(listaEmitentes);
            //Action
            var recebidos = _service.GetAll();
            //Assert
            _repositoryFake.Verify(pr => pr.GetAll(0), Times.Once);
            recebidos.Should().NotBeNull();
            recebidos.Count().Should().Be(listaEmitentes.Count());
            recebidos.First().Should().Be(listaEmitentes.First());
        }
    }
}
