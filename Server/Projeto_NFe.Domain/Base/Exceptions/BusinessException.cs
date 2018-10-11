using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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