using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Projeto_Nfe.API.Controllers.Comum;
using Projeto_Nfe.API.Exceptions;
using Projeto_NFe.Domain.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Projeto_NFe.Controller.Tests.Common
{
    public class ApiControllerBaseFake : ApiControllerBase
    {
        public IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
        {
            return base.HandleCallback(func);
        }
        public IHttpActionResult HandleQuery<TSource, TResult>(TSource query)
        {
            return base.HandleQuery<TSource, TResult>(query);
        }

       protected IHttpActionResult HandleQueryable<TSource, TResult>(IQueryable<TSource> query, ODataQueryOptions<TSource> queryOptions)
        {
            var odataQuery = queryOptions.ApplyTo(query).Cast<TSource>();
            var dataQuery = odataQuery.ToList().AsQueryable().ProjectTo<TResult>();
            var pageResult = new PageResult<TResult>(dataQuery,
                                    Request.ODataProperties().NextLink,
                                    Request.ODataProperties().TotalCount);
            // Esse .ToList() é performado no ProjectTo e não mais no EF
            return Ok(pageResult);
        }

        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is BusinessException ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }
    }
    public class ApiControllerBaseDummy { }
    public class ApiControllerBaseDummyViewModel { }
    public class ApiControllerBaseDummyQuery { }
}
