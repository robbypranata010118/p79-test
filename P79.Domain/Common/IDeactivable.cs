using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P79.Domain.Common
{
    public interface IDeactivable
    {
        bool IsActive { get; set; }
    }
}
