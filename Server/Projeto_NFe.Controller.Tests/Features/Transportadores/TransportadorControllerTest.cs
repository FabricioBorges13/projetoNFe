using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using Projeto_Nfe.API.Controllers.Transportadores;
using Projeto_Nfe.API.Exceptions;
using Projeto_NFe.Applications.Features.Transportadores;
using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Applications.Features.Transportadores.ViewModel;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Controller.Tests.Common;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Projeto_NFe.Controller.Tests.Features.Transportadores
{
    [TestFixture]
    public class TransportadorControllerTest : TestControllerBase
    {
        private TransportadorController _controller;
        private Mock<ITransportadorService> _service;
        private Mock<Transportador> _transportador;
        private Mock<TransportadorAddCommand> _transportadorAddCommand;
        private Mock<TransportadorUpdateCommand> _transportadorUpdateCommand;
        private Mock<TransportadorDeleteCommand> _transportadorDeleteCommand;
        private Mock<ValidationResult> _ValidationResultMock;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _service = new Mock<ITransportadorService>();
            _controller = new TransportadorController(_service.Object)
            {
                Request = request,
            };
            _transportador = new Mock<Transportador>();
            _ValidationResultMock = new Mock<ValidationResult>();

            _transportadorAddCommand = new Mock<TransportadorAddCommand>();
            _transportadorAddCommand.Setup(cmd => cmd.Validation()).Returns(_ValidationResultMock.Object);
            _transportadorUpdateCommand = new Mock<TransportadorUpdateCommand>();
            _transportadorUpdateCommand.Setup(cmd => cmd.Validation()).Returns(_ValidationResultMock.Object);
            _transportadorDeleteCommand = new Mock<TransportadorDeleteCommand>();
            _transportadorDeleteCommand.Setup(cmd => cmd.Validation()).Returns(_ValidationResultMock.Object);

        }

        #region GET

        [Test]
        public void Transportador_Controller_Get_ShouldOk()
        {
            // Arrange
            var transportador = ObjectMother.transportadorDefault;
            var response = new List<Transportador>() { transportador }.AsQueryable();
            _service.Setup(s => s.GetAll()).Returns(response);
            var odataOptions = GetOdataQueryOptions<Transportador>(_controller);

            // Action
            var callback = _controller.Get(odataOptions);
            //Assert
            _service.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<TransportadorViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(transportador.Id);
        }

        [Test]
        public void Transportador_Controller_GetById_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _transportador.Setup(p => p.Id).Returns(id);
            _service.Setup(c => c.GetById(id)).Returns(_transportador.Object);
            // Action
            IHttpActionResult callback = _controller.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<TransportadorViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _service.Verify(s => s.GetById(id), Times.Once);
            _transportador.Verify(p => p.Id, Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Transportador_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<TransportadorAddCommand> transportadorCmd = new Mock<TransportadorAddCommand>();
            transportadorCmd.Setup(x => x.Validation()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Add(transportadorCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _controller.Post(transportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(id);
            _service.Verify(s => s.Add(transportadorCmd.Object), Times.Once);

        }

        #endregion

        #region PUT

        [Test]
        public void Transportador_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<TransportadorUpdateCommand> trasnportadorCmd = new Mock<TransportadorUpdateCommand>();
            trasnportadorCmd.Setup(x => x.Validation()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Update(trasnportadorCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _controller.Update(trasnportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _service.Verify(s => s.Update(trasnportadorCmd.Object), Times.Once);
        }

        [Test]
        public void Transportador_Controller_Put_ShouldHandleNotFoundexception()
        {
            // Arrange
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<TransportadorUpdateCommand> transportadorCmd = new Mock<TransportadorUpdateCommand>();
            transportadorCmd.Setup(x => x.Validation()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Update(transportadorCmd.Object)).Throws<NotFoundException>();
            // Action
            IHttpActionResult callback = _controller.Update(transportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigosDeErro.NotFound);
            _service.Verify(s => s.Update(transportadorCmd.Object), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void Transportador_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<TransportadorDeleteCommand> transportadorCmd = new Mock<TransportadorDeleteCommand>();
            transportadorCmd.Setup(x => x.Validation()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Delete(transportadorCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _controller.Delete(transportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _service.Verify(s => s.Delete(transportadorCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion
    }
}
