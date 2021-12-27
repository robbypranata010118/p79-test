using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
