using NUnit.Framework;
using Projeto_Nfe.API.Controllers.Destinatarios;
using Projeto_NFe.Applications.Features.Destinatarios;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Destinatarios;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Net.Http;
using System.Web.Http;
using FluentAssertions;
using System.Web.Http.Results;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_Nfe.API.Exceptions;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using FluentValidation.Results;
using Projeto_NFe.Applications.Features.Destinatarios.ViewModels;

namespace Projeto_NFe.Controller.Tests.Features.Destinatarios
{
    [TestFixture]
    public class DestinatarioControllerTest : TestControllerBase
    {
        private DestinatarioController _destinatarioController;
        private Mock<IDestinatarioService> _destinatarioServiceMock;
        private Mock<Destinatario> _Destinatario;
        private Mock<DestinatarioAddCommand> _destinatarioAddCmd;
        private Mock<DestinatarioDeleteCommand> _destinatarioDeleteCmd;
        private Mock<DestinatarioUpdateCommand> _destinatarioUpdateCmd;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _destinatarioServiceMock = new Mock<IDestinatarioService>();
            _destinatarioController = new DestinatarioController(_destinatarioServiceMock.Object)
            {
                Request = request,
            };
            _Destinatario = new Mock<Destinatario>();
            _validator = new Mock<ValidationResult>();
            _destinatarioAddCmd = new Mock<DestinatarioAddCommand>();
            _destinatarioAddCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _destinatarioDeleteCmd = new Mock<DestinatarioDeleteCommand>();
            _destinatarioDeleteCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _destinatarioUpdateCmd = new Mock<DestinatarioUpdateCommand>();
            _destinatarioUpdateCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }



        #region GET

     //   [Test]
     //   public void Destinatarios_Controller_Get_ShouldOk()
     //   {
     //       // Arrange
     //       var Destinatario = ObjectMother.DestinatarioValido;
     //       var response = new List<Destinatario>() { Destinatario }.AsQueryable();
     //       _destinatarioServiceMock.Setup(s => s.GetAll()).Returns(response);
     //       // Action
     ////       var callback = _destinatarioController.Get();
     //       //Assert
     //       _destinatarioServiceMock.Verify(s => s.GetAll(), Times.Once);
     //       var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<DestinatarioViewModel>>>().Subject;
     //       httpResponse.Content.Should().NotBeNullOrEmpty();
     //       httpResponse.Content.First().Id.Should().Be(Destinatario.Id);
     //   }

        [Test]
        public void Destinatarios_Controller_GetById_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            _Destinatario.Setup(p => p.Id).Returns(id);
            _destinatarioServiceMock.Setup(c => c.GetById(id)).Returns(_Destinatario.Object);
            // Action
            IHttpActionResult callback = _destinatarioController.GetById(id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<DestinatarioViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _destinatarioServiceMock.Verify(s => s.GetById(id), Times.Once);
            _Destinatario.Verify(p => p.Id, Times.Once);
        }

        #endregion

        #region POST

        [Test]
        public void Destinatarios_Controller_Post_ShouldBeOk()
        {
            // Arrange
            var id = 1;
            var destCmd = ObjectMother.destinatarioAddCommandValid;
            _destinatarioServiceMock.Setup(c => c.Add(destCmd)).Returns(id);
            // Action
            IHttpActionResult callback = _destinatarioController.Post(destCmd);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<int>>().Subject;
            httpResponse.Content.Should().Be(id);
            _destinatarioServiceMock.Verify(s => s.Add(destCmd), Times.Once);
        }

        #endregion

        #region PUT

        [Test]
        public void Destinatarios_Controller_Put_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            var destCmd = ObjectMother.destinatarioUpdateCommandValidoWithId;
            _destinatarioServiceMock.Setup(c => c.Update(destCmd)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _destinatarioController.Update(destCmd);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _destinatarioServiceMock.Verify(s => s.Update(destCmd), Times.Once);
        }

        [Test]
        public void Destinatarios_Controller_Put_ShouldHandleNotFoundexception()
        {
            // Arrange
            _validator.SetupGet(x => x.IsValid).Returns(true);
            Mock<DestinatarioUpdateCommand> destinatarioCmd = new Mock<DestinatarioUpdateCommand>();
            destinatarioCmd.Setup(x => x.Validar()).Returns(_validator.Object);
            _destinatarioServiceMock.Setup(c => c.Update(destinatarioCmd.Object)).Throws<NotFoundException>();
            // Action
            IHttpActionResult callback = _destinatarioController.Update(destinatarioCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.Content.ErrorCode.Should().Be((int)CodigosDeErro.NotFound);
            _destinatarioServiceMock.Verify(s => s.Update(destinatarioCmd.Object), Times.Once);
        }

        #endregion

        #region DELETE

        [Test]
        public void Destinatarios_Controller_Delete_ShouldBeOk()
        {
            // Arrange
            var isUpdated = true;
            var destCmd = ObjectMother.destinatarioDeleteCommandValidoWithId;
            _destinatarioServiceMock.Setup(c => c.Delete(destCmd)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _destinatarioController.Delete(destCmd);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _destinatarioServiceMock.Verify(s => s.Delete(destCmd), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion
    }
}
