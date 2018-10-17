using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Filters;
using Projeto_NFe.Applications.Features.Destinatarios;
using Projeto_NFe.Applications.Features.Destinatarios.Commands;
using Projeto_NFe.Applications.Features.Destinatarios.ViewModels;
using Projeto_NFe.Domain.Features.Destinatarios;
using System.Linq;
using System.Web.Http;
namespace Projeto_Nfe.API.Controllers.Destinatarios
{
    [RoutePrefix("api/destinatario")]
    public class DestinatarioController : ApiControllerBase
    {
        public IDestinatarioService _destinatarioService;

        public DestinatarioController(IDestinatarioService contaService) : base()
        {
            _destinatarioService = contaService;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Destinatario> queryOptions)
        {
            var query = default(IQueryable<Destinatario>);

            query = _destinatarioService.GetAll();

            return HandleQueryable<Destinatario, DestinatarioViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => Mapper.Map<DestinatarioViewModel>(_destinatarioService.GetById(id)));
        }

        [HttpPost]
        public IHttpActionResult Post(DestinatarioAddCommand destinatario)
        {
            var validator = destinatario.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _destinatarioService.Add(destinatario));
        }

        [HttpPut]
        public IHttpActionResult Update(DestinatarioUpdateCommand destinatario)
        {
            var validator = destinatario.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _destinatarioService.Update(destinatario));
        }

        [HttpDelete]
        public IHttpActionResult Delete(DestinatarioDeleteCommand destinatario)
        {
            var validator = destinatario.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _destinatarioService.Delete(destinatario));
        }
    }
}