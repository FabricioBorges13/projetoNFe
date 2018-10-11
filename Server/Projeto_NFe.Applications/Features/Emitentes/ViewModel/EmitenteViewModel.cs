using Projeto_NFe.Applications.Features.Enderecos;
using Projeto_NFe.Applications.Features.Enderecos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Emitentes.ViewModel
{
    public class EmitenteViewModel
    {
        public long Id { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string InscricaoEstadual { get; set; }
        public virtual string NumeroDoDocumento { get; set; }
        public virtual EnderecoViewModel Endereco { get; set; }
    }
}
