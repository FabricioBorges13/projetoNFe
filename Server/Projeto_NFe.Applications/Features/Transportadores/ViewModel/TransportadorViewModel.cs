using Projeto_NFe.Applications.Features.Enderecos.ViewModel;
using Projeto_NFe.Domain.Base.NumeroDeDocumentos;
using Projeto_NFe.Domain.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Transportadores.ViewModel
{
    public class TransportadorViewModel
    {
        public virtual long Id { get; set; }
        public virtual string NomeRazaoSocial { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual string NumeroDocumento { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
        public virtual bool ResponsabilidadeFrete { get; set; }
    }
}
