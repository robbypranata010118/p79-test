using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P79.Api.Admin.Filters
{
    public class BasePathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer>() { new OpenApiServer() { Url = "hxxt://yoursite" } };
        }
    }
}
