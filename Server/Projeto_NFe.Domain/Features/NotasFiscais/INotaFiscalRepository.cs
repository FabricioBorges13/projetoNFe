using Projeto_NFe.Domain.Features.Produtos;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_NFe.Domain.Features.NotasFiscais
{
    public interface INotaFiscalRepository 
    {
        NotaFiscal Add(NotaFiscal produto);
        IQueryable<NotaFiscal> GetAll();
        NotaFiscal GetById(long Id);
        bool Update(NotaFiscal objeto);
        bool Delete(long objeto);
    }
}
