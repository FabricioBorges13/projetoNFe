﻿using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Projeto_Nfe.API.Exceptions;
using Projeto_Nfe.API.Models;
using Projeto_NFe.Controller.Tests.Initializer;
using Projeto_NFe.Domain.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Projeto_NFe.Controller.Tests.Common
{
    [TestFixture]
    public class ApiControllerBaseTests : TestControllerBase
    {
        /* Perceba que esse fake serve apenas para expor os comportamentos de ApiControllerBase */
        private ApiControllerBaseFake _apiControllerBase;
        private Mock<ApiControllerBaseDummy> _dummy;


        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _apiControllerBase = new ApiControllerBaseFake()
            {
                Request = request
            };
            _dummy = new Mock<ApiControllerBaseDummy>();
        }

        #region HandleCallback

        [Test]
        public void Controller_Base_HandleCallback_ShouldHandleRuntimeException()
        {
            //Arrange
            var message = "message error test";
            var exception = new Exception(message);
            // Action
            var callback = _apiControllerBase.HandleCallback<ApiControllerBaseDummy>(() => throw exception);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
            httpResponse.Content.ErrorCode.Should().Be((int)CodigosDeErro.Unhandled);
            httpResponse.Content.ErrorMessage.Should().Be(message);
        }

        #endregion

        #region HandleQuery

        [Test]
        public void Controller_Base_HandleQuery_ShouldBeOk()
        {
            // Action
            var callback = _apiControllerBase.HandleQuery<ApiControllerBaseDummy, ApiControllerBaseDummyViewModel>(_dummy.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ApiControllerBaseDummyViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
        }

        #endregion

        #region HandleQueryable
        [Test]
        public void Controller_Base_HandleQueryable_ShouldBeOk()
        {
            //Arrange
            var odataOptions = GetOdataQueryOptions<ApiControllerBaseDummy>(_apiControllerBase);
            // Action
            var callback = _apiControllerBase.HandleQuery<ApiControllerBaseDummy, ApiControllerBaseDummyViewModel>(_dummy.Object);
            //Assert
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ApiControllerBaseDummyViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
        }



        #endregion

        #region HandleValidationFailure

        [Test]
        public void Controller_Base_HandleValidationFailure_ShouldBeHandleValidationErrors()
        {
            //Arrange
            var validationFailure = new ValidationFailure("", ((int)CodigosDeErro.Unhandled).ToString());
            IList<ValidationFailure> errors = new List<ValidationFailure>() { validationFailure };
            // Action
            var callback = _apiControllerBase.HandleValidationFailure(errors);
            //Assert
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.Content.FirstOrDefault().Should().Be(validationFailure);
        }

        #endregion
    }
}
