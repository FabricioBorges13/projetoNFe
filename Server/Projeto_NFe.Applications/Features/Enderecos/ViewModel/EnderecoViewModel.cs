using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Enderecos.ViewModel
{
    public class EnderecoViewModel
    {
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
