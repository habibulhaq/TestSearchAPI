using System;
using System.Collections.Generic;

#nullable disable
#pragma warning disable
namespace TestSearchAPI.Domain
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public int RoleId { get; set; }
    }
}
