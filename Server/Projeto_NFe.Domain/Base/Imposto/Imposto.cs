using System;

namespace Projeto_NFe.Domain.Base.Imposto
{
    public class Imposto
    {
        public double ValorIpi { get; set; }
        public double ValorIcms { get; set; }
        public double AliquotaIPI { get { return 0.1; } }
        public double AliquotaICMS { get { return 0.04; } }
        public Imposto()
        {

        }
        public Imposto(double valorTotal)
        {
            this.ValorIcms = valorTotal * this.AliquotaICMS;
            this.ValorIpi = valorTotal * this.AliquotaIPI;
        }

        public virtual void Validar()
        {
            ValidarICMS();
            ValidarIPI();
        }

        private void ValidarIPI()
        {
            if (ValorIpi <= 0)
                throw new ValorImpostoInvalido();
        }

        private void ValidarICMS()
        {
            if (ValorIcms <= 0)
                throw new ValorImpostoInvalido();
        }
    }
}
