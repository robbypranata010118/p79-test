using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P79.Domain.Entities
{
    public class AccountPoint
    {
        [ForeignKey(nameof(Account))]
        public int AccountPointId { get; set; }
        public int Point { get; set; }
        [MaxLength(50)]
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        [MaxLength(50)]
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
        public virtual Account Account { get; set; }
    }
}
