using P79.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Entities
{
    public class Account : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } 
    }
}
