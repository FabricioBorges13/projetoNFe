using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using Projeto_Nfe.API.Controllers.Emitentes;
using Projeto_NFe.Applications.Features.Emitentes;
using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Applications.Features.Emitentes.ViewModel;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Domain.Features.Emitentes;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Projeto_NFe.Controller.Tests.Features.Emitentes
{
    [TestFixture]
    public class EmitenteControllerTest : TestControllerBase
    {
        private EmitenteController _emitenteController;
        private Mock<IEmitenteService> _emitenteServiceMock;
        private Mock<Emitente> _emitente;
        private Mock<EmitenteAddCommand> _emitenteAddCmd;
        private Mock<EmitenteDeleteCommand> _emitenteDeleteCmd;
        private Mock<EmitenteUpdateCommand> _emitenteUpdateCmd;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _emitenteServiceMock = new Mock<IEmitenteService>();
            _emitenteController = new EmitenteController(_emitenteServiceMock.Object)
            {
                Request = request,

            };
            _emitente = new Mock<Emitente>();
            _validator = new Mock<ValidationResult>();
            _emitenteAddCmd = new Mock<EmitenteAddCommand>();
            _emitenteAddCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _emitenteDeleteCmd = new Mock<EmitenteDeleteCommand>();
            _emitenteDeleteCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _emitenteUpdateCmd = new Mock<EmitenteUpdateCommand>();
            _emitenteUpdateCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }


        #region POST
        [Test]
        public void Controller_Emitente_Post_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var emitenteCmd = ObjectMother.GetEmitenteValidoParaRegistrar();
            _emitenteServiceMock.Setup(c => c.Add(emitenteCmd)).Returns(emitente.Id);
            // Action
            IHttpActionResult callback = _emitenteController.Post(emitenteCmd);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(emitente.Id);
            _emitenteServiceMock.Verify(s => s.Add(emitenteCmd), Times.Once);

        }
        #endregion

        #region PUT
        [Test]
        public void Controller_Emitentes_Put_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValidoParaAtualizar();
            var isUpdated = true;
            _emitenteServiceMock.Setup(c => c.Update(emitente)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _emitenteController.Update(emitente);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _emitenteServiceMock.Verify(s => s.Update(emitente), Times.Once);
        }
        #endregion

        #region DELETE
        [Test]
        public void Controller_Emitentes_Delete_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValidoParaDeletar();
            var isRemoved = true;
            _emitenteServiceMock.Setup(c => c.Delete(emitente)).Returns(isRemoved);
            // Action
            IHttpActionResult callback = _emitenteController.Delete(emitente);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _emitenteServiceMock.Verify(s => s.Delete(emitente), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion

        #region GET

        [Test]
        public void Controller_Emitentes_Get_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var response = new List<Emitente>() { emitente }.AsQueryable();
            _emitenteServiceMock.Setup(s => s.GetAll()).Returns(response);
            var odataOptions = GetOdataQueryOptions<Emitente>(_emitenteController);

            // Action
            var callback = _emitenteController.Get(odataOptions);
            //Assert
            _emitenteServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<EmitenteViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(emitente.Id);
        }



        [Test]
        public void Controller_Emitentes_Get_Com_Outros_Filtros_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            var uri = "http://localhost:55365/api/emitente?nome=Jonhson";
            var response = new List<Emitente>() { emitente, emitente, emitente }.AsQueryable();
            _emitenteServiceMock.Setup(s => s.GetAll()).Returns(response);
            _emitenteController.Request = GetUri(uri);
            var odataOptions = GetOdataQueryOptions<Emitente>(_emitenteController);
            // Action
            var callback = _emitenteController.Get(odataOptions);
            //Assert
            _emitenteServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<EmitenteViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(emitente.Id);
        }

        [Test]
        public void Controller_Emitentes_GetById_DevePassar()
        {
            // Arrange
            var emitente = ObjectMother.GetEmitenteValido();
            _emitenteServiceMock.Setup(c => c.GetById(emitente.Id)).Returns(emitente);
            // Action
            IHttpActionResult callback = _emitenteController.GetById(emitente.Id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<EmitenteViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(emitente.Id);
            _emitenteServiceMock.Verify(s => s.GetById(emitente.Id), Times.Once);
        }

        #endregion
    }
}
