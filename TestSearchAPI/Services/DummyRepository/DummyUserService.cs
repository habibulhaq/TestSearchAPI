using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Interfaces;
#pragma warning disable
namespace TestSearchAPI.Services.DummyRepository
{
    public class DummyUserService : IUserService
    {
        public async Task<ApiResponse<UserDTOExtension>> GetByEmailAndPassword(string email, string password)
        {
            if (email.Equals("abc@gmail.com", comparisonType: StringComparison.OrdinalIgnoreCase))
            {
                UserDTOExtension userDTOExtension = new UserDTOExtension()
                {
                    CreatedAt = DateTime.UtcNow,
                    Email = email,
                    Name = "Habib Ul Haque",
                    Id = 10,
                    RoleId = 1,
                    RoleName = "Admin"
                };
                return new ApiResponse<UserDTOExtension> { Count = 1, Data = userDTOExtension, HasError = false, Message = "Success." };
            }
            else
            {
                return new ApiResponse<UserDTOExtension> { Count = 0, Data = null, HasError = true, Message = Helper.ErrorMessagesContants.InvalidCredentials };
            }

        }

        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetUsers()
        {
            List<UserDTO> lstUser = new List<UserDTO>() {
            new UserDTO()
            {
                CreatedAt = DateTime.UtcNow,
                Email = "habib@gmail.com",
                Name = "Habib Ul Haque",
                Id = 10,
                RoleId = 1
            },
            new UserDTO()
            {
                CreatedAt = DateTime.UtcNow.AddDays(-10),
                Email = "newuser@gmail.com",
                Name = "New User",
                Id = 11,
                RoleId = 1
            },
            new UserDTO()
            {
                CreatedAt = DateTime.UtcNow.AddDays(-20),
                Email = "newuser1@gmail.com",
                Name = "New User 1",
                Id = 12,
                RoleId = 1
            }
        };
            return new ApiResponse<IEnumerable<UserDTO>> { Count = 3, Data = lstUser, HasError = false, Message = "Success." };
        }
    }
}
