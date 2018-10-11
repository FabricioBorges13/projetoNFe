using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Nfe.Infra.ORM.Context
{
    [ExcludeFromCodeCoverage]
    public class DbContextFactory : IDbContextFactory<ProjetoNFeContext>
    {
        public ProjetoNFeContext Create()
        {
            return new ProjetoNFeContext();
        }
    }
}
