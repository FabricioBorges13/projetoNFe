using FluentAssertions;
using Moq;
using NUnit.Framework;
using Projeto_NFe.Application.Tests.Initializer;
using Projeto_NFe.Applications.Features.Produtos;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_NFe.Application.Tests.Features.Produtos
{
    [TestFixture]
    public class ProdutoServiceTest : TestBase
    {
        private Mock<IProdutoRepository> _repositoryFake;
        private IProdutoService _service;

        [SetUp]
        public void Initialize()
        {
            _repositoryFake = new Mock<IProdutoRepository>();
            _service = new ProdutoService(_repositoryFake.Object);
        }

        [Test]
        public void Service_Produto_AdicionarProduto_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ProdutoDefault();
            var produtoCmd = ObjectMother.GetProdutoValidoParaRegistrar();
            _repositoryFake.Setup(x => x.Add(It.IsAny<Produto>()))
                .Returns(produto);
            //Action
            var novoProdutoId = _service.Add(produtoCmd);
            //Verify
            _repositoryFake.Verify(x => x.Add(It.IsAny<Produto>()), Times.Once);
            novoProdutoId.Should().Be(produto.Id);
        }

        [Test]
        public void Service_Produto_AtualizarProduto_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ProdutoDefault();
            var produtoCmd = ObjectMother.GetProdutoValidoParaAtualizar();
            var atualizado = true;
            _repositoryFake.Setup(x => x.GetById(produtoCmd.Id)).Returns(produto);
            _repositoryFake.Setup(pr => pr.Update(produto)).Returns(atualizado);
            //Action
            var produtoAtualizado = _service.Update(produtoCmd);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(produtoCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(produto), Times.Once);
            produtoAtualizado.Should().BeTrue();
        }

        [Test]
        public void Service_Produto_AtualizarProduto_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var produtoCmd = ObjectMother.GetProdutoValidoParaAtualizar();
            _repositoryFake.Setup(x => x.GetById(produtoCmd.Id)).Returns((Produto)null);
            //Action
            Action act = () => _service.Update(produtoCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(produtoCmd.Id), Times.Once);
            _repositoryFake.Verify(pr => pr.Update(It.IsAny<Produto>()), Times.Never);
        }

        [Test]
        public void Service_Produto_Delete_DevePassar()
        {
            //Arrange
            var produtoCmd = ObjectMother.GetProdutoValidoParaDeletar();
            var removido = true;
            _repositoryFake.Setup(pr => pr.Delete(produtoCmd.ProdutoIds[0])).Returns(removido);
            //Action
            var produtoRemovido = _service.Delete(produtoCmd);
            //Assert
            _repositoryFake.Verify(pr => pr.Delete(produtoCmd.ProdutoIds[0]), Times.Once);
            produtoRemovido.Should().BeTrue();
        }

        [Test]
        public void Service_Produto_Delete_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var produtoCmd = ObjectMother.GetProdutoValidoParaDeletar();
            _repositoryFake.Setup(x => x.Delete(produtoCmd.ProdutoIds[0])).Throws<NotFoundException>();
            //Action
            Action act = () => _service.Delete(produtoCmd);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.Delete(produtoCmd.ProdutoIds[0]), Times.Once);
        }

        [Test]
        public void Service_Produto_PegarProdutoPorId_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ProdutoDefault();
            _repositoryFake.Setup(pr => pr.GetById(produto.Id)).Returns(produto);
            //Action
            var recebido = _service.GetById(produto.Id);
            //Verify
            _repositoryFake.Verify(pr => pr.GetById(produto.Id), Times.Once);
            recebido.Should().NotBeNull();
            recebido.Id.Should().Be(produto.Id);
        }

        [Test]
        public void Service_Produto_PegarProdutoPorId_DeveJogarExcessao_NotFoundException()
        {
            //Arrange
            var produto = ObjectMother.ProdutoDefault();
            _repositoryFake.Setup(pr => pr.GetById(produto.Id)).Throws<NotFoundException>();
            //Action
            Action act = () => _service.GetById(produto.Id);
            //Assert
            act.Should().Throw<NotFoundException>();
            _repositoryFake.Verify(pr => pr.GetById(produto.Id), Times.Once);
        }

        [Test]
        public void Service_Produto_PegarTodosOsProdutos_DevePassar()
        {
            //Arrange
            var produto = ObjectMother.ProdutoDefault();
            var listaProdutos = new List<Produto>() { produto }.AsQueryable();
            _repositoryFake.Setup(pr => pr.GetAll()).Returns(listaProdutos);
            //Action
            var recebidos = _service.GetAll();
            //Assert
            _repositoryFake.Verify(pr => pr.GetAll(), Times.Once);
            recebidos.Should().NotBeNull();
            recebidos.Count().Should().Be(listaProdutos.Count());
            recebidos.First().Should().Be(listaProdutos.First());
        }
    }
}
