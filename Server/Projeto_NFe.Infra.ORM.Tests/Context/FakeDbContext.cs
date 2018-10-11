using Projeto_Nfe.Infra.ORM.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_NFe.Infra.ORM.Tests.Context
{
    public class FakeDbContext : ProjetoNFeContext
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {

        }
    }
}
