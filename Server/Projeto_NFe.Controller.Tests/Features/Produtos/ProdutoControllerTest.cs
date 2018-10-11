using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Projeto_Nfe.API.Controllers.Produtos;
using Projeto_NFe.Applications.Features.Produtos;
using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.ViewModels;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Domain.Features.Produtos;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Projeto_NFe.Controller.Tests.Features.Produtos
{
    [TestFixture]
    public class ProdutoControllerTest : TestControllerBase
    {
        private ProdutoController _produtoController;
        private Mock<IProdutoService> _produtoServiceMock;
        private Mock<Produto> _produto;
        private Mock<ProdutoAddCommand> _produtoAddCmd;
        private Mock<ProdutoDeleteCommand> _produtoDeleteCmd;
        private Mock<ProdutoUpdateCommand> _produtoUpdateCmd;
        private Mock<ValidationResult> _validator;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _produtoServiceMock = new Mock<IProdutoService>();
            _produtoController = new ProdutoController(_produtoServiceMock.Object)
            {
                Request = request,

            };
            _produto = new Mock<Produto>();
            _validator = new Mock<ValidationResult>();
            _produtoAddCmd = new Mock<ProdutoAddCommand>();
            _produtoAddCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _produtoDeleteCmd = new Mock<ProdutoDeleteCommand>();
            _produtoDeleteCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            _produtoUpdateCmd = new Mock<ProdutoUpdateCommand>();
            _produtoUpdateCmd.Setup(cmd => cmd.Validar()).Returns(_validator.Object);
            var isValid = true;
            _validator.Setup(v => v.IsValid).Returns(isValid);
        }


        #region POST
        [Test]
        public void Controller_Produto_Post_DevePassar()
        {
            // Arrange
            var id = 1;
            _validator.SetupGet(x => x.IsValid).Returns(true);
            Mock<ProdutoAddCommand> transportadorCmd = new Mock<ProdutoAddCommand>();
            transportadorCmd.Setup(x => x.Validar()).Returns(_validator.Object);
            _produtoServiceMock.Setup(c => c.Add(transportadorCmd.Object)).Returns(id);
            // Action
            IHttpActionResult callback = _produtoController.Post(transportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(id);
            _produtoServiceMock.Verify(s => s.Add(transportadorCmd.Object), Times.Once);

        }
        #endregion

        #region PUT
        [Test]
        public void Controller_Produtos_Put_DevePassar()
        {
            // Arrange
            var isUpdated = true;
            _validator.SetupGet(x => x.IsValid).Returns(true);
            Mock<ProdutoUpdateCommand> trasnportadorCmd = new Mock<ProdutoUpdateCommand>();
            trasnportadorCmd.Setup(x => x.Validar()).Returns(_validator.Object);
            _produtoServiceMock.Setup(c => c.Update(trasnportadorCmd.Object)).Returns(isUpdated);
            // Action
            IHttpActionResult callback = _produtoController.Update(trasnportadorCmd.Object);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _produtoServiceMock.Verify(s => s.Update(trasnportadorCmd.Object), Times.Once);
        }
        #endregion

        #region DELETE
        [Test]
        public void Controller_Produtos_Delete_DevePassar()
        {
            // Arrange
            var produto = ObjectMother.GetProdutoValidoParaDeletar();
            var isRemoved = true;
            _produtoServiceMock.Setup(c => c.Delete(produto)).Returns(isRemoved);
            // Action
            IHttpActionResult callback = _produtoController.Delete(produto);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _produtoServiceMock.Verify(s => s.Delete(produto), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        #endregion

        #region GET

        [Test]
        public void Controller_Produtos_Get_DevePassar()
        {
            // Arrange
            var produto = ObjectMother.ProdutoDefault();
            var response = new List<Produto>() { produto }.AsQueryable();
            _produtoServiceMock.Setup(s => s.GetAll()).Returns(response);

            var odataOptions = GetOdataQueryOptions<Produto>(_produtoController);
            // Action
            var callback = _produtoController.Get(odataOptions);
            //Assert
            _produtoServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ProdutoViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(produto.Id);
        }
        

        [Test]
        public void Controller_Produtos_Get_Com_Outros_Filtros_DevePassar()
        {
            // Arrange
            var produto = ObjectMother.ProdutoDefault();
            var uri = "http://localhost:55365/api/produto?nome=Jonhson";
            var response = new List<Produto>() { produto, produto, produto }.AsQueryable();
            _produtoServiceMock.Setup(s => s.GetAll()).Returns(response);
            _produtoController.Request = GetUri(uri);
            // Action
            var odataOptions = GetOdataQueryOptions<Produto>(_produtoController);
            var callback = _produtoController.Get(odataOptions);

            //Assert
            _produtoServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<ProdutoViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(produto.Id);
        }

        [Test]
        public void Controller_Produtos_GetById_DevePassar()
        {
            // Arrange
            var produto = ObjectMother.ProdutoDefaultWithId;
            _produtoServiceMock.Setup(c => c.GetById(produto.Id)).Returns(produto);
            // Action
            IHttpActionResult callback = _produtoController.GetById((int)produto.Id);
            // Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ProdutoViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(produto.Id);
            _produtoServiceMock.Verify(s => s.GetById(produto.Id), Times.Once);
        }

        #endregion
    }
}
