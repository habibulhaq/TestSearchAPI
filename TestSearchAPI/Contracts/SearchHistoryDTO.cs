using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable
namespace TestSearchAPI.Contracts
{
    public class SearchHistoryDTO
    {
        public string Query { get; set; }
        public DateTime SearchedOn { get; set; }
        public int UserID { get; set; }
    }

}
