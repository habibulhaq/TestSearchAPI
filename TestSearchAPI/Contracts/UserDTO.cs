using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable
namespace TestSearchAPI.Contracts
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int RoleId { get; set; }
    }

    public class UserDTOExtension : UserDTO {
        public string RoleName { get; set; }
    }
}
