using P79.Base.Interfaces;

using P79.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace P79.Infrastructure.Persistence
{
    public static class SeedManager
    {
        public static async Task<IHost> PerformSeeding(this IHost host)
        {
            System.Reflection.Assembly assemblies = typeof(SeedManager).Assembly;
            var types = assemblies.DefinedTypes.Where(x => x.GetInterfaces().Contains(typeof(ISeeder)));
            using (var scope = host.Services.CreateScope())
            {
                foreach (System.Reflection.TypeInfo type in types)
                {
                    var seeder = assemblies.CreateInstance(type.FullName) as ISeeder;
                    seeder.ServiceScope = scope;
                    await seeder.Seed();
                }
            }
            return host;
        }
    }
}
