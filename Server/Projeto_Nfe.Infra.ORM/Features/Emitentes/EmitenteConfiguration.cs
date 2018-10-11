using Projeto_NFe.Domain.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Emitentes
{
    public class EmitenteConfiguration : EntityTypeConfiguration<Emitente>
    {
        public EmitenteConfiguration()
        {
            ToTable("TBEmitente");
            HasKey(e => e.Id);
            Property(e => e.NomeRazaoSocial).HasColumnName("RazaoSocial").HasMaxLength(100).IsRequired();
            Property(e => e.InscricaoEstadual).HasColumnName("InscricaoEstadual").IsRequired();
            Property(e => e.inscricaoMunicipal).HasColumnName("InscricaoMunicipal").IsRequired();
        }
    }
}
