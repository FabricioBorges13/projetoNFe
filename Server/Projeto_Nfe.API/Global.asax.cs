using Projeto_Nfe.API.IoC;
using Projeto_NFe.Applications.Config.AutoMapper;
using System.Web.Http;

namespace Projeto_Nfe.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfig.Initialize();
            SimpleInjectorContainer.Initialize();
        }
    }
}
