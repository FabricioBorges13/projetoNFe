using Effort;
using FluentAssertions;
using NUnit.Framework;
using Projeto_Nfe.Infra.ORM.Features.Destinatarios;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Infra.ORM.Tests.Context;
using Projeto_NFe.Infra.ORM.Tests.Initializer;
using System;
using System.Linq;

namespace Projeto_NFe.Infra.ORM.Tests.Features.Destinatarios
{
    [TestFixture]
    public class DestinatarioRepositoryTest: EffortTestBase
    {
        private FakeDbContext _ctx;
        private DestinatarioRepository _repository;
        private Destinatario _destinatario;
        private Destinatario _destinatarioSeed;


        [SetUp]
        public void PostRepository_Set()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _ctx = new FakeDbContext(connection);
            _repository = new DestinatarioRepository(_ctx);
            _destinatario = ObjectMother.DestinatarioValido;

            _destinatarioSeed = ObjectMother.DestinatarioValido;

            _ctx.Destinatarios.Add(_destinatarioSeed);

            _ctx.SaveChanges();

        }

        [Test]
        public void Repository_destinatario_deveria_gravar_um_novo_destinatario()
        {
            //Arrange
            Destinatario retorno = _repository.Add(_destinatario);

            retorno.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Repository_destinatario_deveria_retornar_um_destinatario()
        {
            //Arrange
            Destinatario Destinatario = _repository.Add(_destinatario);

            //Action
            Destinatario retornoGet = _repository.GetById(Destinatario.Id);

            //Assert
            var _retorno = retornoGet.NomeRazaoSocial.Should().Be(Destinatario.NomeRazaoSocial);
            retornoGet.Id.Should().Be(Destinatario.Id);
        }

        [Test]
        public void Repository_destinatario_deveria_alterar_um_novo_destinatario()
        {
            //Arrange
            Destinatario retornoAdd = _repository.Add(_destinatario);
            retornoAdd.NomeRazaoSocial = "Casa das Maçãs";

            //Action
            _repository.Update(retornoAdd);

            //Assert
            Destinatario retornoGet = _repository.GetById(retornoAdd.Id);
            retornoGet.NomeRazaoSocial.Should().Be(retornoAdd.NomeRazaoSocial);
        }

        [Test]
        public void Repository_destinatario_deveria_deletar_um_destinatario()
        {
            //Arrange
            Destinatario retornoAdd = _repository.Add(_destinatario);

            //Action
            _repository.Delete(retornoAdd.Id);

            //Assert
            Destinatario retornoGet = _repository.GetById(retornoAdd.Id);
            retornoGet.Should().BeNull();
        }

        [Test]
        public void Repository_destinatario_deveria_buscar_todos_os_destinatarios()
        {

            //Action
            var destinatarios = _repository.GetAll().ToList();

            //Assert
            destinatarios.Should().NotBeNull();
            destinatarios.Should().HaveCount(_ctx.Destinatarios.Count());
            destinatarios.First().Should().Be(_destinatarioSeed);

        }

    }  

}

