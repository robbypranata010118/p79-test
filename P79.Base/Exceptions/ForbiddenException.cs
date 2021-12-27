using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Exceptions
{
    public class ForbiddenException : Exception
    {
        private readonly string _errorCode;

        public ForbiddenException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in ForbiddenException class can not be null or empty", nameof(message));
            }
        }

        public ForbiddenException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in ForbiddenException class can not be null or empty", nameof(message));
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeBadRequest : _errorCode;
    }
}
