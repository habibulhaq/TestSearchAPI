using System;
using System.Collections.Generic;

#nullable disable
#pragma warning disable
namespace TestSearchAPI.Domain
{
    public partial class SearchHistory
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public DateTime SearchedOn { get; set; }
        public int UserId { get; set; }
    }
}
