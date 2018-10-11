using Effort;
using FluentAssertions;
using NUnit.Framework;
using Projeto_Nfe.Infra.ORM.Features.NotasFiscais;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Infra.ORM.Tests.Context;
using Projeto_NFe.Infra.ORM.Tests.Initializer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Infra.ORM.Tests.Features.NotasFiscais
{
    [TestFixture]
    public class NotaFiscalRepositoryTest : EffortTestBase
    {

        NotaFiscalRepository _repository;
        FakeDbContext _fakeDbContext;
        NotaFiscal _notaFiscal;
        NotaFiscal _notaFiscalSeed;

        [SetUp]
        public void Set_Transportador()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _fakeDbContext = new FakeDbContext(connection);
            _notaFiscal = new NotaFiscal();
            _repository = new NotaFiscalRepository(_fakeDbContext);

            _notaFiscalSeed = ObjectMother.NotaFiscalValida;

            _fakeDbContext.NotasFiscais.Add(_notaFiscalSeed);

            _fakeDbContext.SaveChanges();
        }

        [Test]
        public void NotaFiscalRepository_DeveAdicionar_DeveSerOk()
        {
            var nota = ObjectMother.NotaFiscalValidaWithId;
            //Action
            var notaFiscalRegistered = _repository.Add(nota);
            //Verify
            notaFiscalRegistered.Should().NotBeNull();
            notaFiscalRegistered.Id.Should().NotBe(0);
            var expectedTransportador = _fakeDbContext.NotasFiscais.Find(notaFiscalRegistered.Id);
            expectedTransportador.Should().NotBeNull();
            expectedTransportador.Should().Be(notaFiscalRegistered);
        }

        [Test]
        public void NotaFiscalRepository_DeveBuscarTodos_DeveSerOk()
        {
            //Action
            var notaFiscal = _repository.GetAll().ToList();

            //Assert
            notaFiscal.Should().NotBeNull();
            notaFiscal.Should().HaveCount(_fakeDbContext.NotasFiscais.Count());
            notaFiscal.First().Should().Be(_notaFiscalSeed);
        }

        [Test]
        public void NotaFiscalRepository_DeveBuscarPorId_DeveSerOk()
        {
            //Action
            var notaFiscalResult = _repository.GetById(_notaFiscalSeed.Id);

            //Assert
            notaFiscalResult.Should().NotBeNull();
            notaFiscalResult.Should().Be(_notaFiscalSeed);
        }

        [Test]
        public void NotaFiscalRepository_DeveBuscarPorId_DeveSerNulo()
        {
            //Action
            var notFoundId = 10;
            var notaFiscalResult = _repository.GetById(notFoundId);

            //Assert
            notaFiscalResult.Should().BeNull();
        }

        [Test]
        public void TransportadorRepository_DeveDeletar_DeveSerOK()
        {
            // Action
            var isRemoved = _repository.Delete(_notaFiscalSeed.Id);
            // Assert
            isRemoved.Should().BeTrue();
            _fakeDbContext.NotasFiscais.Where(p => p.Id == _notaFiscalSeed.Id).FirstOrDefault().Should().BeNull();
        }

        [Test]
        public void NotaFiscalRepository_DeveEditar_DeveSerOk()
        {
            // Arrange
            var wasUpdated = false;
            _notaFiscalSeed.NaturezaOperacao = "joao";
            //Action
            var actionUpdate = new Action(() => { wasUpdated = _repository.Update(_notaFiscalSeed); });
            // Verify
            actionUpdate.Should().NotThrow<Exception>();  // O EF não deve lançar exception
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void NotaFiscalRepository_DeveEditarIdDesconhecido_DeveSerNulo()
        {
            // Arrange
            _notaFiscal = ObjectMother.NotaFiscalValida;
            var unknownId = 20;
            _notaFiscal.Id = unknownId;
            //Action
            Action updatedAction = () => _repository.Update(_notaFiscal);
            // Verify
            updatedAction.Should().Throw<DbUpdateConcurrencyException>();
            
        }
    }
}
