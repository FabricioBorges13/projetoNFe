using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData;
using Microsoft.OData.UriParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Projeto_Nfe.API.Models;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Projeto_Nfe.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // TODO: Não usar o * em produção. Alterar para o domínio do cliente de publicação / obter da web.config
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapApiRoutes();
            config.EnableOdata();
            config.ConfigureJsonSerialization();
        }

        private static void EnableOdata(this HttpConfiguration config)
        {
            // Web API Enable OData
            config.Count().Select().Filter().OrderBy().MaxTop(null);
            config.AddODataQueryFilter();
            config.EnableDependencyInjection(builder =>
            {
                /* string as enum, substitui o antigo EnableEnumPrefixFree. Converte a String que vem no FiltroOdata para o Enum correspondente*/
                builder.AddService<ODataUriResolver>(ServiceLifetime.Singleton, sp => new StringAsEnumResolver() { EnableCaseInsensitive = true });
            });
        }


        public static void MapApiRoutes(this HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    action = RouteParameter.Optional
                }
            );
        }

        /// <summary>
        /// Método responsável por configurar a forma de serialização em JSON
        /// </summary>
        /// <param name="config"></param>
        private static void ConfigureJsonSerialization(this HttpConfiguration config)
        {
            var jsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.Formatting = Formatting.None;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            // Define que o Content-Type na resposta é o application/json
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypes.Json));
        }

    }
}
