using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P79.Api.Admin
{
    public class AppAuthorize : AuthorizeAttribute
    {
        public string Permissions { get; set; }
        public string Feature { get; set; }
    }
}
