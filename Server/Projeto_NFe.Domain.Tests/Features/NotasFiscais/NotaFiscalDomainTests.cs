using FluentAssertions;
using NUnit.Framework;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;

namespace Projeto_NFe.Domain.Tests.Features.NotasFiscais
{
    [TestFixture]
    public class NotaFiscalDomainTests
    {
        NotaFiscal NotaFiscalValidaNaoEmitida;

        [SetUp]
        public void SetUp()
        {
            NotaFiscalValidaNaoEmitida = ObjectMother.NotaFiscalValidaNaoEmitida;

        }

        [Test]
        public void NotaFiscal_NaturezaOperacao_Com_valor_Em_Branco_Deve_Ser_Invalido()
        {
            NotaFiscalValidaNaoEmitida.NaturezaOperacao = "";
            Action validar = () => NotaFiscalValidaNaoEmitida.ValidarNaturezaOperacao();
            validar.Should().Throw<ExceptionNaturezaOperacaoVaziaOuNulo>();
        }


        [Test]
        public void NotaFiscal_NaturezaOperacao_Com_valor_corret_não_deve_retornar_exceção()
        {
            NotaFiscalValidaNaoEmitida.NaturezaOperacao = "Natureza de testes";
            Action validar = () => NotaFiscalValidaNaoEmitida.ValidarNaturezaOperacao();
            validar.Should().NotThrow<ExceptionNaturezaOperacaoVaziaOuNulo>();
        }

        [Test]
        public void ValidarDatas_Não_Deve_retornar_exceção_com_datas_validas()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.DataEmissao = DateTime.Now;

            //Action
            Action ValidarDatas = () => NotaFiscalValidaNaoEmitida.ValidarDataDeEntradaMaiorQueDataEmissao();


            //Assert
            ValidarDatas.Should().NotThrow<ExceptionDataDeEntradaMaiorQueDataEmissao>();

        }

        [Test]
        public void ValidarDatas_Deve_retornar_exceção_com_data_de_entrada_maior_que_data_de_emissao()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.DataEmissao = DateTime.Now;
            NotaFiscalValidaNaoEmitida.DataEntrada = DateTime.Now.AddMinutes(20);

            //Action
            Action ValidarDatas = () => NotaFiscalValidaNaoEmitida.ValidarDataDeEntradaMaiorQueDataEmissao();


            //Assert
            ValidarDatas.Should().Throw<ExceptionDataDeEntradaMaiorQueDataEmissao>();

        }

        [Test]
        public void ValidarSePossuiChaveDeAcesso_Deve_retornar_exceção_caso_a_chave_esteja_fora_do_formato_ou_ausente()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.ChaveAcesso = "";

            //Action
            Action ValidarChave = () => NotaFiscalValidaNaoEmitida.ValidarSePossuiChaveDeAcesso();


            //Assert
            ValidarChave.Should().Throw<ExceptionChaveDeAcessoInvalidaOuAusente>();

        }

        [Test]
        public void ValidarSeANotaJaEstaEmitida_Deve_Retornar_Exceção_caso_a_Nota_Ja_foi_emitida()
        {
            //Arrange

            NotaFiscalValidaNaoEmitida.NotaEmitida = true;

            //Action
            Action ValidarNotaEmitida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaJaEstaEmitida();

            //Assert
            ValidarNotaEmitida.Should().Throw<ExceptionNotaJaEmitida>();

        }

        [Test]
        public void ValidarSeANotaEValida_Não_Deve_retornar_exceção_com_nota_valida()
        {

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().NotThrow<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_faltando_emitente()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Emitente = null;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_faltando_destinatario()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Destinatario = null;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_faltando_transportador()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Transportador = null;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_sem_ter_um_produto()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Produtos = new List<Produto>();

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_com_emitente_sem_numero_de_documento()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Emitente.NumeroDocumento = null;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_com_destinatario_sem_numero_de_documento()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Destinatario.NumeroDocumento = null;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void ValidarSeANotaEValida_Deve_retornar_exceção_caso_o_emitente_e_destinatario_forem_iguais()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Emitente.NumeroDocumento.NumeroDoDocumento = NotaFiscalValidaNaoEmitida.Destinatario.NumeroDocumento.NumeroDoDocumento;

            //Action
            Action ValidaNotaValida = () => NotaFiscalValidaNaoEmitida.ValidarSeANotaEValida();

            //Assert
            ValidaNotaValida.Should().Throw<ExceptionNotaFiscalInvalida>();
        }

        [Test]
        public void CalcularValorTotalDosProdutosNaNota_Deve_Calcular_Corretamente_O_Valor_Total_de_Todos_Os_Produtos()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Produtos[0].Quantidade = 1;
            NotaFiscalValidaNaoEmitida.Produtos[0].ValorUnitario = 100.0;

            //Action
            NotaFiscalValidaNaoEmitida.CalcularValorTotalDosProdutosEImpostoDaNota();

            //Assert
            NotaFiscalValidaNaoEmitida.ValorTotalDosProdutos.Should().Be(100.00);
        }

        [Test]
        public void CalcularImpostoDosProdutos_Deve_Calcular_Corretamente_O_Imposto_De_Cada_Produto()
        {
            //Arrange
            //Action
            NotaFiscalValidaNaoEmitida.CalcularImpostoDeCadaProduto();

            //Assert
            NotaFiscalValidaNaoEmitida.Produtos[0].Impostos.ValorIcms.Should().Be(20.0);
            NotaFiscalValidaNaoEmitida.Produtos[0].Impostos.ValorIpi.Should().Be(50.0);
        }

        [Test]
        public void CalcularImpostoDaNota_Deve_Calcular_Corretamente_O_Imposto_Da_Nota()
        {
            //Arrange
            NotaFiscalValidaNaoEmitida.Produtos[0].Impostos.ValorIcms = 4.0;
            NotaFiscalValidaNaoEmitida.Produtos[0].Impostos.ValorIpi = 10.0;

            //Action
            NotaFiscalValidaNaoEmitida.CalcularValorTotalDosProdutosEImpostoDaNota();

            //Assert
            NotaFiscalValidaNaoEmitida.ImpostoDaNota.ValorIcms.Should().Be(4);
            NotaFiscalValidaNaoEmitida.ImpostoDaNota.ValorIpi.Should().Be(10);

        }

        [Test]
        public void CalcularValorTotalDaNota_Deve_Calcular_o_Valor_total_da_Nota_corretamente()
        {
            //Arrange

            NotaFiscalValidaNaoEmitida.ValorTotalDosProdutos = 1109.00;
            NotaFiscalValidaNaoEmitida.ImpostoDaNota.ValorIpi = 44.36;
            NotaFiscalValidaNaoEmitida.ImpostoDaNota.ValorIcms = 110.9;
            NotaFiscalValidaNaoEmitida.ValorDoFrete = 100.0;

            //Action
            NotaFiscalValidaNaoEmitida.CalcularValorTotalDaNota();

            //Assert
            NotaFiscalValidaNaoEmitida.ValorTotalDaNota.Should().Be(1364.26);

        }

        [Test]
        public void GerarCodigoDeAcesso_Deve_gerar_o_codigo_de_acesso_corretamente()
        {
            //Arrange

            List<NotaFiscal> notas = new List<NotaFiscal> { NotaFiscalValidaNaoEmitida };

            //Action
            NotaFiscalValidaNaoEmitida.GerarChaveDeAcesso(new DateTime(636643306335845925));

            //Assert
            NotaFiscalValidaNaoEmitida.ChaveAcesso.Should().Be("63664330633584592563664330633584592563664330");
        }



        [Test]
        public void EmitirNotaFiscal_Deve_emitir_a_nota_preenchendo_os_campos_necessarios()
        {
            //Arrange
            var notafiscal = ObjectMother.NotaFiscalValidaNaoEmitida;
            notafiscal.GerarChaveDeAcesso(new DateTime(636643306335845925));
            //Action
            notafiscal.EmitirNotaFiscal();

            //Assert
            notafiscal.DataEmissao.Ticks.Should().BeGreaterThan(NotaFiscalValidaNaoEmitida.DataEntrada.Ticks);
            notafiscal.NotaEmitida.Should().Be(true);

        }

    }
}
