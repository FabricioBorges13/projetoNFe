using System;

namespace Projeto_NFe.Domain.Base.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(CodigosDeErro errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public CodigosDeErro ErrorCode { get; }
    }
}