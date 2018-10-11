using Effort;
using FluentAssertions;
using NUnit.Framework;
using Projeto_Nfe.Infra.ORM.Features.Produtos;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Infra.ORM.Tests.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Infra.ORM.Tests.Features.Produtos
{
    [TestFixture]
    public class ProdutoRepositoryTest 
    {
        private FakeDbContext _context;
        private ProdutoRepository _repository;
        private Produto _produto;
        private Produto _produtoSeed;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new ProdutoRepository(_context);
            _produto = ObjectMother.ProdutoDefault();
            //Seed
            _produtoSeed = ObjectMother.ProdutoDefault();
            _context.Produtos.Add(_produtoSeed);
            _context.SaveChanges();
        }

        #region ADD
        [Test]
        public void Repositorio_Produto_Adicionar_Corretamente()
        {
            //Action
            var produtoRegistrado = _repository.Add(_produto);
            //Assert
            produtoRegistrado.Should().NotBeNull();
            produtoRegistrado.Should().Be(_produto);
        }
        #endregion

        #region UPDATE

        [Test]
        public void Repositorio_Produto_Atualizar_Corretamente()
        {
            var wasUpdated = false;
            var newNome = "Caneta Mágica";
            _produtoSeed.Descricao = newNome;
            var action = new Action(() => { wasUpdated = _repository.Update(_produtoSeed); });
            // O EF não deve lançar exception
            action.Should().NotThrow<Exception>();
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void Repositorio_Produto_Atualizar_DeveJogarExcessao_UnknownId()
        {
            _produto = ObjectMother.ProdutoDefault();
            _produto.Id = 20;
            var action = new Action(() => _repository.Update(_produto));

            action.Should().Throw<DbUpdateConcurrencyException>();
        }
        #endregion

        #region DELETE
        [Test]
        public void Repositorio_Produto_Deletar_Corretamente()
        {
            // Action
            var wasRemoved = _repository.Delete(_produtoSeed.Id);
            //Verify
            wasRemoved.Should().BeTrue();
            _context.Produtos.Where(p => p.Id == _produtoSeed.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Repositorio_Destinatario_Deletar_DeveJogarExcessao_NotFoundException()
        {
            //Assert
            var notFoundId = 10;
            // Action
            Action callbackDelete = () => _repository.Delete(notFoundId);
            //Verify
            callbackDelete.Should().Throw<NotFoundException>();
        }
        #endregion

        #region GETs

        [Test]
        public void Repositorio_Produto_PegarTodos_DevePassar()
        {
            //Action
            var produtos = _repository.GetAll().ToList();
            //Assert
            produtos.Should().NotBeNull();
            produtos.Should().HaveCount(_context.Produtos.Count());
            produtos.First().Should().Be(_produtoSeed);
        }

        [Test]
        public void Repositorio_Produto_PegarPorId_DevePassar()
        {
            //Action
            var destinatarioResult = _repository.GetById(_produtoSeed.Id);
            //Assert
            destinatarioResult.Should().NotBeNull();
            destinatarioResult.Should().Be(_produtoSeed);
        }

        [Test]
        public void Repositorio_Produto_PegarPorId_DeveRetornarNulo()
        {
            //Action
            var notFoundId = 10;
            var produtoResult = _repository.GetById(notFoundId);
            //Assert
            produtoResult.Should().BeNull();
        }

        #endregion
    }
}
