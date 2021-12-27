using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P79.Api.Admin.Filters
{
    public class RemoveQueryApiVersionParamOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters
                .FirstOrDefault(p => p.Name == "api-version" && p.In == ParameterLocation.Query);

            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);

            var XversionParameter = operation.Parameters
               .FirstOrDefault(p => p.Name == "X-version" && p.In == ParameterLocation.Header);

            if (XversionParameter != null)
                operation.Parameters.Remove(XversionParameter);
        }
    }
}
