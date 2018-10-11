using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Transportadores
{
    public class TransportadorEntityConfiguration : EntityTypeConfiguration<Transportador>
    {
        public TransportadorEntityConfiguration()
        {
            this.ToTable("TBTransportadores");
            this.HasKey(o => o.Id);
            this.Property(o => o.InscricaoEstadual).IsRequired();
            this.Property(o => o.NomeRazaoSocial).IsRequired();
            this.Property(o => o.ResponsabilidadeFrete).IsRequired();           
        }
    }
}
