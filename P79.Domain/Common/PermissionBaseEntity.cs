using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P79.Domain.Common
{
    public partial class PermissionBaseEntity
    {
        [Required]
        public bool CanView { get; set; } = false;
        [Required]
        public bool CanEdit { get; set; } = false;
        [Required]
        public bool CanDelete { get; set; } = false;
        [Required]
        public bool CanAdd { get; set; } = false;
        [Required]
        public bool CanApproveContentUnit { get; set; } = false;
        [Required]
        public bool CanApproveContentJLA { get; set; } = false;
    }
}
