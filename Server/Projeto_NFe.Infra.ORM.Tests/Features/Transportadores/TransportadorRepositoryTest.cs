using Effort;
using FluentAssertions;
using NUnit.Framework;
using Projeto_Nfe.Infra.ORM.Features.Transportadores;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Transportadores;
using Projeto_NFe.Infra.ORM.Tests.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Infra.ORM.Tests.Features.Transportadores
{
    [TestFixture]
    public class TransportadorRepositoryTest
    {
        TransportadorRepository _repository;
        FakeDbContext _fakeDbContext;
        Transportador _transportador;
        Transportador _transportadorSeed;

        [SetUp]
        public void Set_Transportador()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _fakeDbContext = new FakeDbContext(connection);
            _transportador = new Transportador();
            _repository = new TransportadorRepository(_fakeDbContext);

            _transportadorSeed = ObjectMother.transportadorDefault;

            _fakeDbContext.Transportadores.Add(_transportadorSeed);

            _fakeDbContext.SaveChanges();
        }

        [Test]
        public void TransportadorRepository_DeveAdicionar_DeveSerOk()
        {
            //Action
            var transportadorRegistered = _repository.Add(_transportadorSeed);
            //Verify
            transportadorRegistered.Should().NotBeNull();
            transportadorRegistered.Id.Should().NotBe(0);
            var expectedTransportador = _fakeDbContext.Transportadores.Find(transportadorRegistered.Id);
            expectedTransportador.Should().NotBeNull();
            expectedTransportador.Should().Be(transportadorRegistered);
        }

        [Test]
        public void TransportadorRepository_DeveBuscarTodos_DeveSerOk()
        {
            //Action
            var transportadores = _repository.GetAll().ToList();

            //Assert
            transportadores.Should().NotBeNull();
            transportadores.Should().HaveCount(_fakeDbContext.Transportadores.Count());
            transportadores.First().Should().Be(_transportadorSeed);
        }

        [Test]
        public void TransportadorRepository_DeveBuscarPorId_DeveSerOk()
        {
            //Action
            var transportadorResult = _repository.GetById(_transportadorSeed.Id);

            //Assert
            transportadorResult.Should().NotBeNull();
            transportadorResult.Should().Be(_transportadorSeed);
        }

        [Test]
        public void TransportadorRepository_DeveBuscarPorId_DeveSerNulo()
        {
            //Action
            var notFoundId = 10;
            var transportadorResult = _repository.GetById(notFoundId);

            //Assert
            transportadorResult.Should().BeNull();
        }

        [Test]
        public void TransportadorRepository_DeveDeletar_DeveSerOK()
        {

            // Action
            var isRemoved = _repository.Delete(_transportadorSeed.Id);
            // Assert
            isRemoved.Should().BeTrue();
            _fakeDbContext.Transportadores.Where(p => p.Id == _transportadorSeed.Id).FirstOrDefault().Should().BeNull();
        }

        [Test]
        public void TransportadorRepository_DeveEditar_DeveSerOk()
        {
            // Arrange
            var wasUpdated = false;
            _transportadorSeed.NomeRazaoSocial = "joao";
            //Action
            var actionUpdate = new Action(() => { wasUpdated = _repository.Update(_transportadorSeed); });
            // Verify
            actionUpdate.Should().NotThrow<Exception>();  // O EF não deve lançar exception
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void TransportadorRepository_DeveEditarIdDesconhecido_DeveSerNulo()
        {
            // Arrange
            _transportador = ObjectMother.transportadorDefault; 
            var unknownId = 20;
            _transportador.Id = unknownId;
            //Action
            Action updatedAction = () => _repository.Update(_transportador);
            // Verify
            updatedAction.Should().Throw<DbUpdateConcurrencyException>();
        }
    }
}
