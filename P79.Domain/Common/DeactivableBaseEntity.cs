using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P79.Domain.Common
{
    public partial class DeactivableBaseEntity:IDeactivable
    {
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
