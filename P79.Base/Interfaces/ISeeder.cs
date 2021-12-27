using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Interfaces
{
    public interface ISeeder
    {
        IServiceScope ServiceScope { get; set; }
        Task Seed();
    }
}
