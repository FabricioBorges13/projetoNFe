using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Produtos
{
    public class ProdutoEntityConfiguration : EntityTypeConfiguration<Produto>
    {
        public ProdutoEntityConfiguration()
        {
            this.ToTable("TBProdutos");
            this.HasKey(o => o.Id);
            this.Property(o => o.CodigoProduto).IsRequired();
            this.Property(o => o.Descricao).IsRequired();
            this.Property(o => o.ValorUnitario).IsRequired();
            this.Property(o => o.Quantidade).IsRequired();
            
        }
    }
}
