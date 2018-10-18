using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using Projeto_Nfe.API.Controllers.NotasFiscais;
using Projeto_Nfe.API.Exceptions;
using Projeto_NFe.Applications.Features.NotasFiscais;
using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Applications.Features.NotasFiscais.ViewModel;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.NotasFiscais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Projeto_NFe.Controller.Tests.Features.NotasFiscais
{
    [TestFixture]
    public class NotaFiscalControllerTest : TestControllerBase
    {

        private NotaFiscalController _controller;
        private Mock<INotaFiscalService> _service;
        private Mock<NotaFiscal> notaFiscal;
        private Mock<NotaFiscalAddCommand> _transportadorAddCommand;
        private Mock<NotaFiscalUpdateCommand> _notaUpdateCommand;
        private Mock<NotaFiscalDeleteCommand> _transportadorDeleteCommand;
        private Mock<ValidationResult> _ValidationResultMock;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _service = new Mock<INotaFiscalService>();
            _controller = new NotaFiscalController(_service.Object)
            {
                Request = request,
            };
            notaFiscal = new Mock<NotaFiscal>();
            _ValidationResultMock = new Mock<ValidationResult>();
            

            _transportadorAddCommand = new Mock<NotaFiscalAddCommand>();
            _notaUpdateCommand = new Mock<NotaFiscalUpdateCommand>();
            _notaUpdateCommand.Setup(cmd => cmd.Validar()).Returns(_ValidationResultMock.Object);
            _transportadorDeleteCommand = new Mock<NotaFiscalDeleteCommand>();


        }

        #region GET

        [Test]
        public void NotaFiscal_Controller_Get_ShouldOk()
        {
            // Arrange
            var notaFiscal = ObjectMother.NotaFiscalValida;
            var response = new List<NotaFiscal>() { notaFiscal }.AsQueryable();
            _service.Setup(s => s.GetAll()).Returns(response);
            var odataOptions = GetOdataQueryOptions<NotaFiscal>(_controller);
            // Action
            var callback = _controller.Get(odataOptions);
            //Assert
            _service.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<NotaFiscalViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(notaFiscal.Id);
        }

        [Test]
        public void NotaFiscal_Controller_GetById_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            notaFiscal.Setup(p => p.Id).Returns(id);
            _service.Setup(c => c.GetById(id)).Returns(notaFiscal.Object);
            // Action
            IHttpActionResult callback = _controller.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<NotaFiscalExtend>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _service.Verify(s => s.GetById(id), Times.Once);
            notaFiscal.Verify(p => p.Id, Times.Once);
            
        }

        #endregion

        #region POST

        [Test]
        public void NotaFiscal_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            Mock<NotaFiscalAddCommand> notaFiscalCmd = new Mock<NotaFiscalAddCommand>();
            _service.Setup(c => c.Add(notaFiscalCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _controller.Post(notaFiscalCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(id);
            _service.Verify(s => s.Add(notaFiscalCmd.Object), Times.Once);

        }

        #endregion

        #region PUT

        [Test]
        public void NotaFiscal_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<NotaFiscalUpdateCommand> notaFiscalCmd = new Mock<NotaFiscalUpdateCommand>();
            notaFiscalCmd.Setup(x => x.Validar()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Update(notaFiscalCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _controller.Update(notaFiscalCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _service.Verify(s => s.Update(notaFiscalCmd.Object), Times.Once);
        }

        [Test]
        public void NotaFiscal_Controller_Put_ShouldHandleNotFoundexception()
        {
            // Arrange
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<NotaFiscalUpdateCommand> notaFiscalCmd = new Mock<NotaFiscalUpdateCommand>();
            notaFiscalCmd.Setup(x => x.Validar()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Update(notaFiscalCmd.Object)).Throws<NotFoundException>();
            // Action
            IHttpActionResult callback = _controller.Update(notaFiscalCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigosDeErro.NotFound);
            _service.Verify(s => s.Update(notaFiscalCmd.Object), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void NotaFiscal_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            _ValidationResultMock.SetupGet(x => x.IsValid).Returns(true);
            Mock<NotaFiscalDeleteCommand> notaFiscalCmd = new Mock<NotaFiscalDeleteCommand>();
            notaFiscalCmd.Setup(x => x.Validar()).Returns(_ValidationResultMock.Object);
            _service.Setup(c => c.Delete(notaFiscalCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _controller.Delete(notaFiscalCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _service.Verify(s => s.Delete(notaFiscalCmd.Object), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion
    }
}
