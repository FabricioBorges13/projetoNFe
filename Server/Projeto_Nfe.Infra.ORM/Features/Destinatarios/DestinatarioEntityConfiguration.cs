using Projeto_NFe.Domain.Features.Destinatarios;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Features.Destinatarios
{
    public class DestinatarioEntityConfiguration: EntityTypeConfiguration<Destinatario>
    {
        public DestinatarioEntityConfiguration()
        {
            this.ToTable("TBDestinatario");
            this.HasKey(o => o.Id);

            this.Property(o => o.InscricaoEstadual);
            this.Property(o => o.NomeRazaoSocial);

        }
    }
}
