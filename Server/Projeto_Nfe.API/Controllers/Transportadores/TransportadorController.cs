using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Filters;
using Projeto_NFe.Applications.Features.Transportadores;
using Projeto_NFe.Applications.Features.Transportadores.Commands;
using Projeto_NFe.Applications.Features.Transportadores.ViewModel;
using Projeto_NFe.Domain.Features.Transportadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto_Nfe.API.Controllers.Transportadores
{
    [RoutePrefix("api/transportador")]
    public class TransportadorController : ApiControllerBase
    {
        public ITransportadorService _service;

        public TransportadorController(ITransportadorService service) : base()
        {
            _service = service;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Transportador> queryOptions)
        {
            var query = default(IQueryable<Transportador>);

            query = _service.GetAll();

            return HandleQueryable<Transportador, TransportadorViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => Mapper.Map<TransportadorViewModel>(_service.GetById(id)));
        }

        [HttpPost]
        public IHttpActionResult Post(TransportadorAddCommand transportador)
        {
            var validator = transportador.Validation();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _service.Add(transportador));
        }

        [HttpPut]
        public IHttpActionResult Update(TransportadorUpdateCommand transportador)
        {
            var validator = transportador.Validation();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _service.Update(transportador));
        }

        [HttpDelete]
        public IHttpActionResult Delete(TransportadorDeleteCommand transportador)
        {
            var validator = transportador.Validation();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _service.Delete(transportador));
        }
    }
}
