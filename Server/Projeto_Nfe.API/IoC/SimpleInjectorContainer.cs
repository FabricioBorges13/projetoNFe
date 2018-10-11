using Projeto_Nfe.Infra.ORM.Context;
using Projeto_Nfe.Infra.ORM.Features.Destinatarios;
using Projeto_Nfe.Infra.ORM.Features.Emitentes;
using Projeto_Nfe.Infra.ORM.Features.Produtos;
using Projeto_NFe.Applications.Features.Destinatarios;
using Projeto_NFe.Applications.Features.Emitentes;
using Projeto_Nfe.Infra.ORM.Features.Transportadores;
using Projeto_NFe.Applications.Features.Produtos;
using Projeto_NFe.Domain.Features.Destinatarios;
using Projeto_NFe.Applications.Features.Transportadores;
using Projeto_NFe.Domain.Features.Emitentes;
using Projeto_NFe.Domain.Features.Produtos;
using Projeto_NFe.Domain.Features.Transportadores;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web.Http;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Applications.Features.NotasFiscais;
using Projeto_Nfe.Infra.ORM.Features.NotasFiscais;

namespace Projeto_Nfe.API.IoC
{
    public class SimpleInjectorContainer
    {
        public static void Initialize()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            //Registrando as Implementações

            RegisterServices(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                         new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterServices(Container container)
        {
            container.Register<IProdutoService, ProdutoService>();
            container.Register<IProdutoRepository, ProdutoRepository>();
            container.Register<IEmitenteService, EmitenteService>();
            container.Register<IEmitenteRepository, EmitenteRepository>();
            container.Register<ITransportadorRepository, TransportadorRepository>();
            container.Register<ITransportadorService, TransportadorService>();
            container.Register<IDestinatarioService, DestinatarioService>();
            container.Register<IDestinatarioRepository, DestinatarioRepository>();
            container.Register<INotaFiscalService, NotaFiscalService>();
            container.Register<INotaFiscalRepository, NotaFiscalRepository>();

            container.Register(() => new ProjetoNFeContext(), Lifestyle.Scoped);

        }

    }
}