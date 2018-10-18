using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Applications.Features.Produtos.ViewModels
{
    public class ProdutoInNotaFiscalViewModel
    {
        public long Id { get; set; }
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public double ValorUnitario { get; set; }
    }
}
