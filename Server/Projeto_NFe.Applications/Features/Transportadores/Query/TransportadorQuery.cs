using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Transportadores.Query
{
    public class TransportadorQuery
    {
        public virtual int Quantidade { get; set; }

        public TransportadorQuery(int quantidade)
        {
            Quantidade = quantidade;
        }
    }
}
