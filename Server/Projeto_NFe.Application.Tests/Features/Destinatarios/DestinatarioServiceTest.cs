using FluentAssertions;
using Moq;
using NUnit.Framework;
using Projeto_NFe.Application.Tests.Initializer;
using Projeto_NFe.Applications.Features.Destinatarios;
using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Application.Tests.Features.Destinatarios
{
    [TestFixture]
    public class DestinatarioServiceTest : TestBase
    {
        Mock<IDestinatarioRepository> _destinatarioRepository;
        DestinatarioService _destinatarioService;

        [SetUp]
        public void Set()
        {
            _destinatarioRepository = new Mock<IDestinatarioRepository>();
            _destinatarioService = new DestinatarioService(_destinatarioRepository.Object);
        }

        [Test]
        public void ApplService_destinatario_Add_Deve_Chamar_O_Metodo_E_Add()
        {
            //Arrange
            long expectedId = 1;
            var dest = ObjectMother.DestinatarioValido;
            dest.Id = expectedId;
            var destRetorno = ObjectMother.destinatarioAddCommandValid;
            _destinatarioRepository.Setup(x => x.Add(It.IsAny<Destinatario>())).Returns(dest);

            //Action
            var obtido = _destinatarioService.Add(destRetorno);

            //Assert
            obtido.Should().Be((int)dest.Id);
            _destinatarioRepository.Verify(x => x.Add(It.IsAny<Destinatario>()), Times.Once);
            _destinatarioRepository.VerifyNoOtherCalls();
        }
        [Test]
        public void ApplService_destinatario_Delete_Deve_Chamar_OMetodo_Delete()
        {
            //Arrange
            var destinatario = ObjectMother.destinatarioDeleteCommandValidoWithId;

            _destinatarioRepository.Setup(x => x.Delete(destinatario.DestinatarioIds[1])).Returns(true);

            //Action
            Action destinatarioDeleteAction = () => _destinatarioService.Delete(destinatario);

            //Assert
            destinatarioDeleteAction.Should().NotThrow<Exception>();
            _destinatarioRepository.Verify(x => x.Delete(destinatario.DestinatarioIds[1]), Times.Once());
            _destinatarioRepository.VerifyNoOtherCalls();
        }
        [Test]
        public void ApplService_destinatario_Update_Deve_Chamar_OsMetodos_Validar_E_Update()
        {
            //Arrange
            var destinatario = ObjectMother.DestinatarioValido;
            var destinatarioCmd = ObjectMother.destinatarioUpdateCommandValidoWithId;
            var atualizado = true;
            _destinatarioRepository.Setup(x => x.GetById(destinatarioCmd.Id)).Returns(destinatario);
            _destinatarioRepository.Setup(pr => pr.Update(destinatario)).Returns(atualizado);
            //Action
            var destinatarioAtualizado = _destinatarioService.Update(destinatarioCmd);
            //Verify
            _destinatarioRepository.Verify(pr => pr.GetById(destinatarioCmd.Id), Times.Once);
            _destinatarioRepository.Verify(pr => pr.Update(destinatario), Times.Once);
            destinatarioAtualizado.Should().BeTrue();
        }
        [Test]
        public void ApplService_destinatario_Get_Deve_Chamar_OMetodo_Get()
        {
            //Arrange
            Destinatario destinatario = ObjectMother.destinatarioValidoWithId;
            _destinatarioRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(destinatario);

            //Action
            Destinatario destinatarioResult = _destinatarioService.GetById(destinatario.Id);

            //Assert
            _destinatarioRepository.Verify(p => p.GetById(It.IsAny<long>()), Times.Once());
            destinatarioResult.Should().NotBeNull();
            destinatarioResult.Id.Should().Be(destinatario.Id);
            _destinatarioRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ApplService_destinatario_GetAll_Deve_Chamar_OMetodo_GetAll()
        {
            //Arrange
            IQueryable<Destinatario> destinatarioList = ObjectMother.destinatarioDefaultList;
            _destinatarioRepository.Setup(x => x.GetAll()).Returns(destinatarioList);

            //Action
            List<Destinatario> resultEnderecoList = _destinatarioService.GetAll().ToList();

            //Assert
            _destinatarioRepository.Verify(x => x.GetAll());
            resultEnderecoList.Should().NotBeNull();
            resultEnderecoList.Should().HaveCount(3);
            _destinatarioRepository.VerifyNoOtherCalls();
        }

        //[Test]
        //public void ApplService_destinatario_Delete_Deve_Retornar_Excessao_Quando_Tiver_Vinculo_Com_Nota()
        //{
        //    //Arrange
        //    _destinatarioRepository.Setup(x => x.TemVinculoNota(It.IsAny<Destinatario>())).Returns(true);

        //    //Action
        //    Action actionDelete = () => _destinatarioService.Delete(ObjectMother.destinatarioValidoWithId);

        //    //Assert
        //    actionDelete.Should().Throw<PossuiVinculoComNotaException>();
        //    _destinatarioRepository.Verify(x => x.TemVinculoNota(It.IsAny<Destinatario>()));
        //}

        //[Test]
        //public void ApplService_destinatario_Delete_Nao_Deve_Retornar_Excessao_Quando_Nao_Tiver_Vinculo_Com_Nota()
        //{
        //    //Arrange
        //    _destinatarioRepository.Setup(x => x.TemVinculoNota(It.IsAny<Destinatario>())).Returns(false);

        //    //Action
        //    Action actionDelete = () => _destinatarioService.Delete(ObjectMother.destinatarioValidoWithId);

        //    //Assert
        //    actionDelete.Should().NotThrow<PossuiVinculoComNotaException>();
        //    _destinatarioRepository.Verify(x => x.TemVinculoNota(It.IsAny<Destinatario>()));
        //}
    }
}
