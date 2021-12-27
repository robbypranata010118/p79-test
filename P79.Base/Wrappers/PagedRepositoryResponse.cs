using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P79.Base.Wrappers
{
    public class PagedRepositoryResponse<T>
    {
        public PagedInfoRepositoryResponse Info { get; set; }
        public T Results { get; set; }
    }
    public class PagedInfoRepositoryResponse
    { 
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public int Length { get; set; }
    }
}
