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

            _notaFiscalRepository.Setup(x => x.Delete(nota.NotaFiscalIds[1])).Returns(true);

            //Action
            Action notaDeleteAction = () => _notaFiscalService.Delete(nota);

            //Assert
            notaDeleteAction.Should().NotThrow<Exception>();
            _notaFiscalRepository.Verify(x => x.Delete(nota.NotaFiscalIds[1]), Times.Once());
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
            NotaFiscal nota = ObjectMother.NotaFiscalValida;
            List<NotaFiscal> notaFiscalList = new List<NotaFiscal> { nota };
           
            _notaFiscalRepository.Setup(x => x.GetAll()).Returns(notaFiscalList.AsQueryable());

            List<NotaFiscal> resultNotaFiscalList = _notaFiscalService.GetAll().ToList();

            _notaFiscalRepository.Verify(x => x.GetAll());
            resultNotaFiscalList.Should().NotBeNull();
            resultNotaFiscalList.Should().HaveCount(1);
        }

        [Test]
        public void ApplService_NotaFiscal_Get_Deve_Retornar_Um_NotaFiscal()
        {
            NotaFiscal notaFiscal = ObjectMother.NotaFiscalValida;
            _notaFiscalRepository.Setup(x => x.GetById(It.IsAny<long>())).Returns(notaFiscal);

            NotaFiscal notaFiscalResult = _notaFiscalService.GetById(notaFiscal.Id);

            _notaFiscalRepository.Verify(p => p.GetById(It.IsAny<long>()), Times.Once());
            notaFiscalResult.Should().NotBeNull();
            notaFiscalResult.Id.Should().Be(notaFiscal.Id);
        }
    }
}
