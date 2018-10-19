using FluentAssertions;
using Moq;
using NUnit.Framework;
using Projeto_NFe.Application.Tests.Initializer;
using Projeto_NFe.Applications.Features.NotasFiscais;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.Emitir;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Application.Tests.Features.NotasFiscais
{
    [TestFixture]
    public class NotaFiscalServiceTest: TestBase
    {
        Mock<INotaFiscalRepository> _notaFiscalRepository;
        Mock<IProdutoRepository> _produtoRepository;
        Mock<ITransportadorRepository> _transportadorRepository;
        Mock<IEmitenteRepository> _emitenteRepository;
        Mock<IDestinatarioRepository> _destinatarioRepository;

        INotaFiscalService _notaFiscalService;


        [SetUp]
        public void Set()
        {
            _notaFiscalRepository = new Mock<INotaFiscalRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _transportadorRepository = new Mock<ITransportadorRepository>();
            _emitenteRepository = new Mock<IEmitenteRepository>();
            _destinatarioRepository = new Mock<IDestinatarioRepository>();

            _notaFiscalService = new NotaFiscalService(_notaFiscalRepository.Object, 
                _produtoRepository.Object, _transportadorRepository.Object, 
                _emitenteRepository.Object, _destinatarioRepository.Object);
            
        }

        [Test]
        public void ApplService_NotaFiscal_Add_Deve_Incluir_Uma_NotaFiscal()
        {
            //Arrange
            long expectedId = 1;
            var nota = ObjectMother.NotaFiscalValida;
            nota.Id = expectedId;
            var notaRetorno = ObjectMother.NotaFiscalValidaParaRegistro;
            _notaFiscalRepository.Setup(x => x.Add(It.IsAny<NotaFiscal>())).Returns(nota);

            //Action
            var obtido = _notaFiscalService.Add(notaRetorno);

            //Assert
            obtido.Should().Be((int)nota.Id);
            _notaFiscalRepository.Verify(x => x.Add(It.IsAny<NotaFiscal>()), Times.Once);
            _notaFiscalRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ApplService_NotaFiscal_Delete_Deve_Deletar_NotaFiscal()
        {
            //Arrange
            var nota = ObjectMother.NotaFiscalValidaParaDeletar;

            _notaFiscalRepository.Setup(x => x.Delete(nota.NotaFiscalIds[0])).Returns(true);

            //Action
            Action notaDeleteAction = () => _notaFiscalService.Delete(nota);

            //Assert
            notaDeleteAction.Should().NotThrow<Exception>();
            _notaFiscalRepository.Verify(x => x.Delete(nota.NotaFiscalIds[0]), Times.Once());
            _notaFiscalRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void ApplService_NotaFiscal_Update_deve_Atualizar_Um_NotaFiscal()
        {
            //Arrange
            var nota = ObjectMother.NotaFiscalValida;
            var notaCmd = ObjectMother.NotaFiscalValidaParaAtualizar;
            var atualizado = true;
            _notaFiscalRepository.Setup(x => x.GetById(notaCmd.Id)).Returns(nota);
            _notaFiscalRepository.Setup(pr => pr.Update(nota)).Returns(atualizado);
            //Action
            var emitenteAtualizado = _notaFiscalService.Update(notaCmd);
            //Verify
            _notaFiscalRepository.Verify(pr => pr.GetById(notaCmd.Id), Times.Once);
            _notaFiscalRepository.Verify(pr => pr.Update(nota), Times.Once);
            emitenteAtualizado.Should().BeTrue();
        }

        [Test]
        public void ApplService_NotaFiscal_GetAll_Deve_Listar_Todos_NotaFiscals()
        {
            //Arrange
            var notafiscal = ObjectMother.NotaFiscalValidaComId;
            var repositoryMockValue = new List<NotaFiscal>() { notafiscal }.AsQueryable();
            _notaFiscalRepository.Setup(odr => odr.GetAll()).Returns(repositoryMockValue);
            //Action
            var notafiscalCB = _notaFiscalService.GetAll();
            //Assert
            _notaFiscalRepository.Verify(nf => nf.GetAll(), Times.Once);
            notafiscalCB.Should().NotBeNull();
            notafiscalCB.Count().Should().Be(repositoryMockValue.Count());
            //Perceba que Equals de Entity já compara os Id's
            notafiscalCB.First().Should().Be(repositoryMockValue.First());
        }

        [Test]
        public void ApplService_NotaFiscal_Get_Deve_Retornar_Uma_NotaFiscal()
        {
            //Arrange
            var notafiscal = ObjectMother.NotaFiscalValidaComId;
            _notaFiscalRepository.Setup(nf => nf.GetById(notafiscal.Id)).Returns(notafiscal);
            //Action
            var notaResult = _notaFiscalService.GetById(notafiscal.Id);
            //Assert
            _notaFiscalRepository.Verify(nf => nf.GetById(notafiscal.Id), Times.Once);
            notaResult.Should().NotBeNull();
            notaResult.Should().BeOfType<NotaFiscal>();
            notaResult.Id.Should().Be(notafiscal.Id);
        }
    }
}
