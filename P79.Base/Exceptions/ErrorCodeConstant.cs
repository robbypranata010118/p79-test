using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Exceptions
{
    public class ErrorCodeConstant
    {
        public const string ErrorCodeInvalidValidation = "E00x001";
        public const string ErrorCodeUnauthorized = "E00x002";
        public const string ErrorCodeForbidden = "E00x003";
        public const string ErrorCodeBadRequest = "E00x004";
        public const string ErrorCodeNotFound = "E00x005";
        public const string ErrorCodeInternalServer = "E00x999";
    }
}
