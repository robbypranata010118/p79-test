using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Domain.Common
{
    public interface IEntity<TKey>
    {
        [Key]
        [Required]
        public TKey Id { get; set; }
    }
    public interface IEntity:IEntity<Guid>
    {
    }
}
