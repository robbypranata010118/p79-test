using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Dtos.Transactions
{
    public class TransactionDto
    {
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string DebitCreditStatus { get; set; }
        public decimal Amount { get; set; }
    }
}
