using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
#pragma warning disable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestSearchAPI.Contracts;
using TestSearchAPI.Domain;
using TestSearchAPI.Helper;
using TestSearchAPI.Interfaces;


namespace TestSearchAPI.Services
{
    public class UserService : IUserService
    {
        private readonly TestSearchDBContext _dbContext;
        public UserService(TestSearchDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<ApiResponse<UserDTOExtension>> GetByEmailAndPassword(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) { return new ApiResponse<UserDTOExtension> { HasError = true, Message = ErrorMessagesContants.EntityRequired.Replace("@entity","Email") }; }
                if (!Utility.ValidateEmail(email)) { return new ApiResponse<UserDTOExtension> { HasError = true, Message = ErrorMessagesContants.EntityInvalid.Replace("@entity", "Email") }; }
                if (string.IsNullOrEmpty(password)) { return new ApiResponse<UserDTOExtension> { HasError = true, Message = ErrorMessagesContants.EntityRequired.Replace("@entity", "Password") }; }

                var res = await (from usr in _dbContext.Users
                                 join r in _dbContext.Roles on usr.RoleId equals r.Id
                                 where usr.Email.ToLower().Equals(email.ToLower()) && usr.Password.Equals(password)
                                 select new UserDTOExtension
                                 {
                                     Id = usr.Id,
                                     Email = usr.Email,
                                     Name = usr.Name,
                                     CreatedAt = usr.CreatedOn,
                                     Password = usr.Password,
                                     RoleId = r.Id,
                                     RoleName = r.RoleName
                                 }).AsNoTracking().FirstOrDefaultAsync();
                if (res != null)
                {
                    return new ApiResponse<UserDTOExtension> { HasError = false, Message = "", Data = res, Count = 1 };
                }
                return new ApiResponse<UserDTOExtension> { HasError = true, Message = ErrorMessagesContants.InvalidCredentials, Count = 0 };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserDTOExtension> { HasError = true, Message = $"Error occured:{ex.Message}" };
            }
        }
        public async Task<ApiResponse<IEnumerable<UserDTO>>> GetUsers()
        {
            var res = await (from usr in _dbContext.Users
                             join r in _dbContext.Roles on usr.RoleId equals r.Id
                             select new UserDTOExtension
                             {
                                 Id = usr.Id,
                                 Email = usr.Email,
                                 Name = usr.Name,
                                 CreatedAt = usr.CreatedOn,
                                 RoleId = r.Id,
                                 RoleName = r.RoleName
                             }).AsNoTracking().ToListAsync();

            return new ApiResponse<IEnumerable<UserDTO>> { HasError = false, Message = "", Data = res, Count = res.Count() };
        }
    }
}
