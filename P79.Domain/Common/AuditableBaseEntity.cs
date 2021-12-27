using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P79.Domain.Common
{
    public partial class AuditableBaseEntity<T>
    {
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public T CreatedBy { get; set; }
        [Required]
        [MaxLength(200)]
        public string CreatedByName { get; set; }
    }
}
