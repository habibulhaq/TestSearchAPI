using System;
using System.Collections.Generic;

#nullable disable
#pragma warning disable
namespace TestSearchAPI.Domain
{
    public partial class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public int Liked { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
