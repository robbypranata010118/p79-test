using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Common
{
    public interface IAuditable
    {
        [NotMapped]
        public string ModuleName { get; set; }
        [NotMapped]
        public string FeatureName { get; set; }
    }
}
