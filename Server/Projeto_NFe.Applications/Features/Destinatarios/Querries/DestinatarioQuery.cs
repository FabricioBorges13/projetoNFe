using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Destinatarios.Querries
{
    public class DestinatarioQuery
    {
        public virtual int Quantidade { get; set; }

        public DestinatarioQuery(int _quantidade)
        {
            Quantidade = _quantidade;
        }

    }
}
