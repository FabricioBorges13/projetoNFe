using Projeto_NFe.Domain.Features.NotasFiscais;
using System.Data.Entity.ModelConfiguration;

namespace Projeto_Nfe.Infra.ORM.Features.NotasFiscais
{
    public class NotaFiscalConfiguration : EntityTypeConfiguration<NotaFiscal>
    {
        public NotaFiscalConfiguration()
        {
            ToTable("TBNotaFiscal");
            HasKey(e => e.Id);
            
            Property(e => e.NaturezaOperacao).IsOptional();
            Property(e => e.ChaveAcesso).IsOptional();
            Property(e => e.DataEmissao).IsOptional();
            Property(e => e.DataEntrada).IsOptional();
            Property(e => e.ValorDoFrete).IsOptional();
            Property(e => e.ValorTotalDaNota).IsOptional();
            Property(e => e.ValorTotalDosProdutos).IsOptional();
            Property(e => e.NotaEmitida).IsOptional();


            Property(a => a.TransportadorId).HasColumnName("Transportador_Id").IsOptional();
            HasOptional(e => e.Transportador).WithMany().HasForeignKey(a => a.TransportadorId);

            Property(a => a.DestinatarioId).HasColumnName("Departamento_Id").IsOptional();
            HasOptional(e => e.Destinatario).WithMany().HasForeignKey(a => a.DestinatarioId);


            Property(a => a.EmitenteId).HasColumnName("Emitente_Id").IsOptional();
            HasOptional(e => e.Emitente).WithMany().HasForeignKey(a => a.EmitenteId);
            
            HasMany(e => e.Produtos).WithMany()
                .Map(cs =>
                {
                    cs.MapLeftKey("NotaFiscal_Id");
                    cs.MapRightKey("Produto_Id");
                    cs.ToTable("NotaFiscalProdutoes");
                });
            
        }
    }
}
