using LicenceApp.models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenceApp.Services
{
    public interface IUserService
    {
        Task<IList> GetAll();
        Task<UserDto> GetUserbyId(int id);
        Task<UserRoleDto> GetUserRole();    
        Task Create(NewUser newUser);
        Task Update(int id , UpdateUser updatedUser);
        Task Delete(int id);
    }
}
