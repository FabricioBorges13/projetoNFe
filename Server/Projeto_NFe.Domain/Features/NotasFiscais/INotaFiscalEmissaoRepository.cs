using Projeto_NFe.Domain.Features.NotasFiscais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Features.Emitir
{
    public interface INotaFiscalEmissaoRepository 
    {
        NotaFiscal getByChaveDeAcesso(string chaveDeAcesso);
    }
}
