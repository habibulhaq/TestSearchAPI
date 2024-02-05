using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable
namespace TestSearchAPI.Contracts
{
    public class SearchCriteriaDTO
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int? Quantity { get; set; }
        public int? Liked { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string SortBy { get; set; }
        public int SortType { get; set; }
    }

}
