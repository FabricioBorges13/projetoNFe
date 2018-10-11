using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Extensions;
using Projeto_Nfe.API.Filters;
using Projeto_NFe.Applications.Features.Produtos;
using Projeto_NFe.Applications.Features.Produtos.Commands;
using Projeto_NFe.Applications.Features.Produtos.ViewModels;
using Projeto_NFe.Domain.Features.Produtos;
using System.Web.Http;

namespace Projeto_Nfe.API.Controllers.Produtos
{

    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiControllerBase
    {
        public IProdutoService _produtoService;

        public ProdutoController(IProdutoService contaService) : base()
        {
            _produtoService = contaService;
        }

        #region POST
        [HttpPost]
        public IHttpActionResult Post(ProdutoAddCommand produto)
        {
            var validator = produto.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _produtoService.Add(produto));
        }
        #endregion

        #region UPDATE
        [HttpPut]
        public IHttpActionResult Update(ProdutoUpdateCommand produto)
        {
            var validator = produto.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _produtoService.Update(produto));
        }
        #endregion


        #region DELETE
        [HttpDelete]
        public IHttpActionResult Delete(ProdutoDeleteCommand produto)
        {
            var validator = produto.Validar();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);
            return HandleCallback(() => _produtoService.Delete(produto));
        }
        #endregion


        #region GET
        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Produto> queryOptions)
        {
         return HandleQueryable<Produto, ProdutoViewModel>(_produtoService.GetAll(), queryOptions);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => Mapper.Map<ProdutoViewModel>(_produtoService.GetById(id)));
        }
        #endregion
    }
}