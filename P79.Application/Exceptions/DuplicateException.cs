using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        private readonly string _errorCode;

        public DuplicateException(string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in DuplicateException class can not be null or empty", nameof(message));
            }
        }

        public DuplicateException(string message, string errorCode) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message in DuplicateException class can not be null or empty", nameof(message));
            }

            _errorCode = errorCode;
        }

        public string ErrorCode => string.IsNullOrWhiteSpace(_errorCode) ? ErrorCodeConstant.ErrorCodeBadRequest : _errorCode;
    }
}
