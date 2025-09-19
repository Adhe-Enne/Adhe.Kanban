using Kanban.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Business.Exceptions
{
    // Excepción de negocio desacoplada de HTTP
    public class BusinessException : Exception
    {
        public BusinessErrorCode ErrorCode { get; }
        public string? Reason { get; } // Permitir null usando tipo nullable

        public BusinessException(string message, BusinessErrorCode errorCode, string? reason = null)
            : base(message)
        {
            ErrorCode = errorCode;
            Reason = reason;
        }
    }
}
