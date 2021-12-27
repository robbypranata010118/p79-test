using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Dtos.Transactions
{
    public class TransactionByAccountIdResponse
    {
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Amount { get; set; }
    }
}
