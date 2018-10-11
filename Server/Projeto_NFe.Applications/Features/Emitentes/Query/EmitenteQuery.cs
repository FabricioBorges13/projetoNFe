using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Emitentes.Query
{
    public class EmitenteQuery
    {
        public virtual int Quantidade { get; set; }

        public EmitenteQuery(int _quantidade)
        {
            Quantidade = _quantidade;
        }
    }
}
