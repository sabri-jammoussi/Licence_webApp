using LicenceApp.Data;
using LicenceApp.models;
using LicenceApp.Services.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography;

namespace LicenceApp.Services
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

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new ApplicationException("Utilisateur n'existe pas");

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            
        }



        public async Task<IList> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
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
            if (!string.IsNullOrEmpty(updateduser.Password))
                {
                // Hash the updated password
                
                CreatePasswordHash(updateduser.Password, out byte[] passwordHash, out byte[] passwordSalt);
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
            }
            
            existingUser.FirstName = updateduser.FirstName;
            existingUser.LastName = updateduser.LastName;
;
            existingUser.Email = updateduser.Email;
            existingUser.Role = updateduser.Role;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<UserDao>> UserListAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }


    }
}
