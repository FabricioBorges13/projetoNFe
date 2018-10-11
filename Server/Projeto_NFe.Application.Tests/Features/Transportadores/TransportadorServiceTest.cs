using FluentAssertions;
using Moq;
using NUnit.Framework;
using Projeto_NFe.Application.Tests.Initializer;
using Projeto_NFe.Applications.Features.Transportadores;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Application.Tests.Features.Transportadores
{
    [TestFixture]
    public class TransportadorServiceTest : TestBase
    {
        private Mock<ITransportadorRepository> _repositoryFake;
        private ITransportadorService _service;

        [SetUp]
        public void Initialize()
        {
            _repositoryFake = new Mock<ITransportadorRepository>();
            _service = new TransportadorService(_repositoryFake.Object);
        }

        [Test]
        public void Service_Transportador_AdicionarTransportador_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.transportadorDefault;
            var transportadorCmd = ObjectMother.transportadorAddCommand;
            _repositoryFake.Setup(x => x.Add(It.IsAny<Transportador>()))
                .Returns(transportador);
            //Action
            var novoTransportadorId = _service.Add(transportadorCmd);
            //Verify
            _repositoryFake.Verify(x => x.Add(It.IsAny<Transportador>()), Times.Once);
            novoTransportadorId.Should().Be(transportador.Id);
        }

        [Test]
        public void Service_Transportador_AtualizarTransportador_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.transportadorDefault;
            var transportadorCmd = ObjectMother.transportadorUpdateCommand;
            var atualizado = true;
            _repositoryFake.Setup(x => x.GetById(transportadorCmd.Id)).Returns(transportador);
            _repositoryFake.Setup(pr => pr.Update(transportador)).Returns(atualizado);
            //Action
            var emitenteAtualizado = _service.Update(transportadorCmd);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(transportadorCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(transportador), Times.Once);
            emitenteAtualizado.Should().BeTrue();
        }

        [Test]
        public void Service_Transportador_AtualizarTransportador_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var transportadorCmd = ObjectMother.transportadorUpdateCommand;
            _repositoryFake.Setup(x => x.GetById(transportadorCmd.Id)).Returns((Transportador)null);
            //Action
            Action act = () => _service.Update(transportadorCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(transportadorCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Transportador>()), Times.Never);
        }

        [Test]
        public void Service_Transportador_Delete_DevePassar()
        {
            //Arrange
            var transportadorCmd = ObjectMother.transportadorDeleteCommand;
            var removido = true;
            _repositoryFake.Setup(pr => pr.Delete(transportadorCmd.TransportadorIds[1])).Returns(removido);
            //Action
            var emitenteRemovido = _service.Delete(transportadorCmd);
            //Assert
            _repositoryFake.Verify(pr => pr.Delete(transportadorCmd.TransportadorIds[1]), Times.Once);
            emitenteRemovido.Should().BeTrue();
        }

        [Test]
        public void Service_Transportador_Delete_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var transportadorCmd = ObjectMother.transportadorDeleteCommand;
            _repositoryFake.Setup(x => x.Delete(transportadorCmd.TransportadorIds[1])).Throws<NotFoundException>();
            //Action
            Action act = () => _service.Delete(transportadorCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.Delete(transportadorCmd.TransportadorIds[1]), Times.Once);
        }

        [Test]
        public void Service_Transportador_PegarEmitentePorId_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.transportadorDefault;
            _repositoryFake.Setup(pr => pr.GetById(transportador.Id)).Returns(transportador);
            //Action
            var recebido = _service.GetById(transportador.Id);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(transportador.Id), Times.Once);
            recebido.Should().NotBeNull();
            recebido.Id.Should().Be(transportador.Id);
        }

        [Test]
        public void Service_Transportador_PegarTransportadorPorId_DeveJogarExcessao_NotFoundException()
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
        public void Service_Transportador_PegarTodosOsTransportadores_DevePassar()
        {
            //Arrange
            var transportador = ObjectMother.transportadorDefault;
            var listaTransportadores = new List<Transportador>() { transportador }.AsQueryable();
            _repositoryFake.Setup(pr => pr.GetAll()).Returns(listaTransportadores);
            //Action
            var recebidos = _service.GetAll();
            //Assert
            _repositoryFake.Verify(pr => pr.GetAll(), Times.Once);
            recebidos.Should().NotBeNull();
            recebidos.Count().Should().Be(listaTransportadores.Count());
            recebidos.First().Should().Be(listaTransportadores.First());
        }
    }
}
