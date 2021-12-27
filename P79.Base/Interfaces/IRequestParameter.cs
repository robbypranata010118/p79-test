using P79.Base.Parameters;
using System.Collections.Generic;

namespace P79.Base.Interfaces
{
    public interface IRequestParameter
    {
        int Page { get; set; }
        int Length { get; set; }
        List<string> Orders { get; set; }
        string SortType { get; set; }
        List<RequestFilterParameter> Filters { get; set; }
    }
}
