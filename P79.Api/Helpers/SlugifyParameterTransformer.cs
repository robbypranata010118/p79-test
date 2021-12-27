using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P79.Api.Admin.Helpers
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value)
        {
            return value != null
                ? Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2")
                : null;
        }
    }
}
