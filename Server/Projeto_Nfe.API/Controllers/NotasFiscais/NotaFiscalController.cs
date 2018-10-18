using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Filters;
using Projeto_NFe.Applications.Features.NotasFiscais;
using Projeto_NFe.Applications.Features.NotasFiscais.Commands;
using Projeto_NFe.Applications.Features.NotasFiscais.ViewModel;
using Projeto_NFe.Applications.Features.Produtos.ViewModels;
using Projeto_NFe.Domain.Features.NotasFiscais;
using Projeto_NFe.Domain.Features.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Projeto_Nfe.API.Controllers.NotasFiscais
{
    [RoutePrefix("api/notafiscal")]
    public class NotaFiscalController : ApiControllerBase
    {
        public INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService) : base()
        {
            _notaFiscalService = notaFiscalService; 
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<NotaFiscal> queryOptions)
        {
            var query = default(IQueryable<NotaFiscal>);

            query = _notaFiscalService.GetAll();

            return HandleQueryable<NotaFiscal, NotaFiscalViewModel>(query, queryOptions);
        }
        [HttpGet]
        [Route("{id:int}/produtos")]
        public IHttpActionResult GetProdutoFromNotaFiscal(long id, ODataQueryOptions<Produto> queryOptions)
        {
            var query = default(IQueryable<Produto>);
            query = _notaFiscalService.GetListaDeProdutoDaNotaFiscal(id);
            
            return HandleQueryable<Produto, ProdutoInNotaFiscalViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return HandleCallback(() => Mapper.Map<NotaFiscalExtend>(_notaFiscalService.GetById(id)));
        }

        [HttpPost]
        public IHttpActionResult Post(NotaFiscalAddCommand notaFiscal)
        {
            return HandleCallback(() => _notaFiscalService.Add(notaFiscal));
        }

        [HttpPut]
        public IHttpActionResult Update(NotaFiscalUpdateCommand notaFiscal)
        {
            var validator = notaFiscal.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _notaFiscalService.Update(notaFiscal));
        }
        [HttpPatch]
        [Route("produtos")]
        public IHttpActionResult UpdateProdutos(NotaFiscalUpdateProdutosCommand notaFiscal)
        {
            var validator = notaFiscal.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _notaFiscalService.UpdateProdutos(notaFiscal));
        }

        [HttpDelete]
        public IHttpActionResult Delete(NotaFiscalDeleteCommand notaFiscal)
        {
            var validator = notaFiscal.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _notaFiscalService.Delete(notaFiscal));
        }
    }
}