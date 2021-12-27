using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P79.Domain.Common
{
    public class BaseEntity<TKey> : IEntity<TKey>, IDeactivable , IAudit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public TKey Id { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(50)]
        public string UserIn { get; set; }
        public DateTime DateIn { get; set; }
        [MaxLength(50)]
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
    public class BaseEntity :BaseEntity<Guid>, IEntity<Guid>, IDeactivable , IAudit
    {
    }
}
