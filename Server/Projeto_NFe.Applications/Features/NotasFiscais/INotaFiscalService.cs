using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.NotasFiscais
{
    public interface INotaFiscalService
    {
        long Add(NotaFiscalAddCommand notaFiscal);

        bool Update(NotaFiscalUpdateCommand notaFiscal);

        bool Delete(NotaFiscalDeleteCommand notaFiscal);

        NotaFiscal GetById(long id);

        IQueryable<Produto> GetListaDeProdutoDaNotaFiscal(long id);

        bool UpdateProdutos(NotaFiscalUpdateProdutosCommand produtos);

        IQueryable<NotaFiscal> GetAll();
    }
}
