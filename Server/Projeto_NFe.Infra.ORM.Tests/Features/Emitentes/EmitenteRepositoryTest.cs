using Effort;
using FluentAssertions;
using NUnit.Framework;
using Projeto_Nfe.Infra.ORM.Features.Emitentes;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Base.Exceptions;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Infra.ORM.Tests.Context;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Projeto_NFe.Infra.ORM.Tests.Features.Emitentes
{
    [TestFixture]
    public class EmitenteRepositoryTest
    {
        private FakeDbContext _context;
        private EmitenteRepository _repository;
        private Emitente _emitente;
        private Emitente _emitenteSeed;

        [SetUp]
        public void Setup()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _context = new FakeDbContext(connection);
            _repository = new EmitenteRepository(_context);
            _emitente = ObjectMother.GetEmitenteValido();
            //Seed
            _emitenteSeed = ObjectMother.GetEmitenteValido();
            _context.Emitentes.Add(_emitenteSeed);
            _context.SaveChanges();
        }


        #region ADD
        [Test]
        public void Repositorio_Emitente_Adicionar_Corretamente()
        {
            //Action
            var emitenteRegistrado = _repository.Add(_emitente);
            //Assert
            emitenteRegistrado.Should().NotBeNull();
            emitenteRegistrado.Should().Be(_emitente);
        }
        #endregion

        #region UPDATE

        [Test]
        public void Repositorio_Emitente_Atualizar_Corretamente()
        {
            var wasUpdated = false;
            var newNome = "Johson";
            _emitenteSeed.NomeRazaoSocial = newNome;
            var action = new Action(() => { wasUpdated = _repository.Update(_emitenteSeed); });
            // O EF não deve lançar exception
            action.Should().NotThrow<Exception>();
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void Repositorio_Emitente_Atualizar_DeveJogarExcessao_UnknownId()
        {
            _emitente = ObjectMother.GetEmitenteValido();
            _emitente.Id = 20;
            var action = new Action(() => _repository.Update(_emitente));

            action.Should().Throw<DbUpdateConcurrencyException>();
        }
        #endregion

        #region DELETE
        [Test]
        public void Repositorio_Emitente_Deletar_Corretamente()
        {
            // Action
            var wasRemoved = _repository.Remove(_emitenteSeed.Id);
            //Verify
            wasRemoved.Should().BeTrue();
            _context.Emitentes.Where(p => p.Id == _emitenteSeed.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Repositorio_Destinatario_Deletar_DeveJogarExcessao_NotFoundException()
        {
            //Assert
            var notFoundId = 10;
            // Action
            Action callbackDelete = () => _repository.Remove(notFoundId);
            //Verify
            callbackDelete.Should().Throw<NotFoundException>();
        }
        #endregion

        #region GETs

        [Test]
        public void Repositorio_Emitente_PegarTodos_DevePassar()
        {
            //Action
            var emitentes = _repository.GetAll().ToList();
            //Assert
            emitentes.Should().NotBeNull();
            emitentes.Should().HaveCount(_context.Emitentes.Count());
            emitentes.First().Should().Be(_emitenteSeed);
        }

        [Test]
        public void Repositorio_Emitente_PegarPorId_DevePassar()
        {
            //Action
            var destinatarioResult = _repository.GetById(_emitenteSeed.Id);
            //Assert
            destinatarioResult.Should().NotBeNull();
            destinatarioResult.Should().Be(_emitenteSeed);
        }

        [Test]
        public void Repositorio_Emitente_PegarPorId_DeveRetornarNulo()
        {
            //Action
            var notFoundId = 10;
            var emitenteResult = _repository.GetById(notFoundId);
            //Assert
            emitenteResult.Should().BeNull();
        }

        #endregion
    }
}
