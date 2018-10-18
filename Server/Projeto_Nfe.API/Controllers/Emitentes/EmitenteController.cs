using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Extensions;
using Projeto_Nfe.API.Filters;
using Projeto_NFe.Applications.Features.Emitentes;
using Projeto_NFe.Applications.Features.Emitentes.Commands;
using Projeto_NFe.Applications.Features.Emitentes.ViewModel;
using Projeto_NFe.Domain.Features.Emitentes;
using System.Web.Http;

namespace Projeto_Nfe.API.Controllers.Emitentes
{
    [RoutePrefix("api/emitente")]
    public class EmitenteController : ApiControllerBase
    {
        public IEmitenteService _emitenteService;
        public EmitenteController(IEmitenteService emitenteService) : base()
        {
            _emitenteService = emitenteService;
        }

        #region POST
        [HttpPost]
        public IHttpActionResult Post(EmitenteAddCommand emitente)
        {
            var validator = emitente.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _emitenteService.Add(emitente));
        }
        #endregion

        #region UPDATE
        [HttpPut]
        public IHttpActionResult Update(EmitenteUpdateCommand emitente)
        {
            var validator = emitente.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _emitenteService.Update(emitente));
        }
        #endregion


        #region DELETE
        [HttpDelete]
        public IHttpActionResult Delete(EmitenteDeleteCommand emitente)
        {
            var validator = emitente.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _emitenteService.Delete(emitente));
        }
        #endregion


        #region GET
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Emitente> queryOptions)
        {
            var quantidade = Request.GetQueryQuantidadeExtension();
            return HandleQueryable<Emitente, EmitenteViewModel>(_emitenteService.GetAll(), queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => Mapper.Map<EmitenteViewModel>(_emitenteService.GetById(id)));
        }
        #endregion
    }
}