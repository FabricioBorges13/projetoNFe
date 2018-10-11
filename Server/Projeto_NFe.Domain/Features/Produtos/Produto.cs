using System;
using Projeto_NFe.Base.Domain;
using Projeto_NFe.Domain.Base.Imposto;

namespace Projeto_NFe.Domain.Features.Produtos
{
    public class Produto : Entidade
    {
        public string CodigoProduto { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get { return this.Quantidade * this.ValorUnitario; } }
        public Imposto Impostos { get; set; }

        public Produto()
        {
            Impostos = new Imposto();
        }

        public void CalcularImpostoDoProduto()
        {
            Impostos = new Imposto(ValorTotal);
        }
       
    }
}
