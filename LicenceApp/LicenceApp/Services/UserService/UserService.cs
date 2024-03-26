using Azure.Core;
using LicenceApp.Data;
using LicenceApp.Enums;
using LicenceApp.models;
using LicenceApp.models.GlobalDao;
using LicenceApp.Services.Security;
using LicenceApp.Services.UserService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace LicenceApp.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly LicenceDBContext _dbContext;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IEmailValidator _emailValidator;
        public UserService(LicenceDBContext dbContext, IPasswordValidator passwordValidator, IEmailValidator emailValidator)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
            _emailValidator = emailValidator;
        }

        public async Task Create(NewUser newUser)
        {
            if (newUser == null)
                throw new ApplicationException("all fields are required ! ");
            var IsEmailValid = await _emailValidator.IsEmailValid(newUser.Email);
            if (!IsEmailValid)
                throw new ApplicationException("Email invalid ");
            var userInDb = _dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
            if (userInDb != null)
            {
                throw new ApplicationException("Email is already registered.");
            }
            var isPaswwordValid = await _passwordValidator.IsPasswordValid(newUser.Password);

            if (!isPaswwordValid)
                throw new ApplicationException("Mot de passe invalid (Doit avoir au moins  [numéro,8 characters,majuscule,minuscule] )!! ");

            CreatePasswordHash(newUser.Password,
               out byte[] passwordHash,
               out byte[] passwordSalt);
            var data = new UserDao
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

                Role = newUser.Role,

            };

            _dbContext.Users.Add(data);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProfilePassword(UpdateProfilePassword updateProfilePassword)
        {
            var existingUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == updateProfilePassword.Id);

            if (existingUser == null)
                throw new ApplicationException($"The Id : {updateProfilePassword.Id} you have been inserted  is  invalid ");

            CreatePasswordHash(updateProfilePassword.NewPassword,
             out byte[] passwordHash,
             out byte[] passwordSalt);
            if (!VerifyPasswordHash(updateProfilePassword.CurrentPassword, existingUser.PasswordHash, existingUser.PasswordSalt))
                throw new ApplicationException("wrong Current password  !");


            var isPaswwordValid = await _passwordValidator.IsPasswordValid(updateProfilePassword.NewPassword);
            if (!isPaswwordValid)
                throw new ApplicationException("Mot de passe invalid (Doit avoir au moins  [numéro,8 characters,majuscule,minuscule] )!! ");

            existingUser.PasswordSalt = passwordSalt;
            existingUser.PasswordHash = passwordHash;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new ApplicationException("Utilisateur n'existe pas");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<UserDto>> GetAll()
        {
            List<UserDto> userDtos = await _dbContext.Users
                .Where(u => u.Role != 0)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();

            return userDtos;
        }

        public async Task<UserRoleDto> GetUserRole()
        {
            try
            {
                var allRoles = Enum.GetNames(typeof(Role)).ToList();
                var userRoleDto = new UserRoleDto
                {
                    Roles = allRoles
                };

                return userRoleDto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Role n'existe pas");
            }
        }

        public async Task<UserDto> GetUserbyId(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new ApplicationException($"the id : {id} does not  existe  ");
            }
            var userdto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
            };
            return userdto;
        }

        public async Task Update(int id, UpdateUser updateduser)
        {
            var existingUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
                throw new ApplicationException($"The Id : {id} you have been inserted  is  invalid ");

            existingUser.FirstName = updateduser.FirstName;
            existingUser.LastName = updateduser.LastName;
            existingUser.Email = updateduser.Email;
            existingUser.Role = updateduser.Role;

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateProfile(int id, UpdateProfile updateProfile)
        {
            if (updateProfile == null)
                throw new ArgumentNullException(nameof(updateProfile));
            // charger l'utilisateur a modifie 
            var existingUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (existingUser == null)
                throw new ApplicationException($"The Id : {id} you have been inserted  is  invalid ");

            existingUser.FirstName = updateProfile.FirstName;
            existingUser.LastName = updateProfile.LastName;
            existingUser.Email = updateProfile.Email;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<UserDao>> UserListAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
