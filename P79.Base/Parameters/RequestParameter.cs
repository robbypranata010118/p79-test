using P79.Base.Interfaces;
using System.Collections.Generic;

namespace P79.Base.Parameters
{
    public class RequestParameter:IRequestParameter
    {
        public int Page { get; set; }
        public int Length { get; set; }
        public List<string> Orders { get; set; }
        public string SortType { get; set; }
        public List<RequestFilterParameter> Filters { get; set; }
        public RequestParameter()
        {
            this.Page = 0;
            this.Length = 0;
            this.Orders = new List<string>();
            this.SortType = "ASC";
            this.Filters = new List<RequestFilterParameter>();
        }
        public RequestParameter(int page, int length)
        {
            this.Page = Page < 1 ? 1 : Page;
            this.Length = Length > 50 ? 50 : Length;
        }
        public int CalculateOffset()
        {
            return (Page - 1) == 0 || (Page - 1) < 0 ? 0 : (Page - 1) * Length;
        }
    }
}
