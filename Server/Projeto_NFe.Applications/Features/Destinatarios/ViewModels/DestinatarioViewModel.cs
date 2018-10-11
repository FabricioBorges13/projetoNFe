using Projeto_NFe.Applications.Features.Enderecos.ViewModel;
using Projeto_NFe.Domain.Base.NumeroDeDocumentos;
using Projeto_NFe.Domain.Features.Enderecos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Destinatarios.ViewModels
{
    public class DestinatarioViewModel
    {
        public long Id { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public virtual string NumeroDoDocumento { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
    }
}
