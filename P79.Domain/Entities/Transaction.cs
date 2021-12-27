using P79.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P79.Domain.Entities
{
    public class Transaction : BaseEntity<int>
    {
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        [MaxLength(1)]
        public string DebitCreditStatus { get; set; }
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        public virtual Account Account { get; set; }
    }
}
