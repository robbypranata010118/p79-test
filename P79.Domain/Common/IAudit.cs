using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Common
{
    public interface IAudit
    {
        [Required]
        [MaxLength(50)]
        public string UserIn { get; set; }
        [Required]
        public DateTime DateIn { get; set; }
        [MaxLength(50)]
        public string UserUp { get; set; }
        public DateTime? DateUp { get; set; }
    }
}
