using FluentAssertions;
using NUnit.Framework;
using Projeto_NFe.Common.Tests.Features;
using Projeto_NFe.Domain.Features.Produtos;
using System;

namespace Projeto_NFe.Domain.Tests.Features.Produtos
{
    [TestFixture]
    public class ProdutoDomainTests
    {
        Produto _ProdutoDefault;

        [SetUp]
        public void SetUp()
        {
            _ProdutoDefault = ObjectMother.ProdutoDefault();
        }

        [Test]
        public void Dominio_Produto_Deve_Calcular_ImpostoProduto_ValorICMS_Deve_Ser_Valido()
        {
            //Arrange - Action
            _ProdutoDefault.CalcularImpostoDoProduto();

            //Assert
            _ProdutoDefault.Impostos.ValorIcms.Should().BeGreaterThan(0);
        }

        [Test]
        public void Dominio_Produto_Deve_Calcular_ImpostoProduto_ValorIPI_Deve_Ser_Valido()
        {
            //Arrange - Action
            _ProdutoDefault.CalcularImpostoDoProduto();

            //Assert
            _ProdutoDefault.Impostos.ValorIpi.Should().BeGreaterThan(0);
        }
    }
}
