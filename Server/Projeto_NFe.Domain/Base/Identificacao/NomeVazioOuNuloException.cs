using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Domain.Base.Identificacao
{
    public class NomeVazioOuNuloException : DomainException
    {
        public NomeVazioOuNuloException() : base("Nome não pode ser vazio ou nulo.")
        {
        }

    }
}