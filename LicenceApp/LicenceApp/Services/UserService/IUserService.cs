using LicenceApp.models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenceApp.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAll();
        Task<UserDto> GetUserbyId(int id);
        Task<UserRoleDto> GetUserRole();
        Task Create(NewUser newUser);
        Task Update(int id, UpdateUser updatedUser);
        Task UpdateProfile(int id, UpdateProfile updateProfile);
        Task UpdateProfilePassword(UpdateProfilePassword updateProfilePassword);
        Task Delete(int id);
    }
}
