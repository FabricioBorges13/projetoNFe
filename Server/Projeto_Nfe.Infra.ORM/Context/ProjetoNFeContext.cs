using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Domain.Features.Transportadores;
using Projeto_NFe.Domain.Features.Emitentes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using Projeto_NFe.Domain.Features.NotasFiscais;

namespace Projeto_Nfe.Infra.ORM.Context
{
    [ExcludeFromCodeCoverage]
    public class ProjetoNFeContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Destinatario> Destinatarios { get; set; }

        public DbSet<Emitente> Emitentes { get; set; }

        public DbSet<Transportador> Transportadores { get; set; }

        public DbSet<NotaFiscal> NotasFiscais{ get; set; }

        public ProjetoNFeContext() : base("SolidOpsNFeDb")
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public ProjetoNFeContext(DbConnection connection) : base(connection, true) { }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
