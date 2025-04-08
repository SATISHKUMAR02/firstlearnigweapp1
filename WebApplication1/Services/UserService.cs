using WebApplication1.Data.Repository;
using WebApplication1.Data;
using AutoMapper;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly ICollegeRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(ICollegeRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(nameof(id));
            }
            var existinguser = await _userRepository.GetAsync(u => u.Id == id, true);
            if (existinguser == null)
            {
                throw new Exception($"user not found");
            }
            existinguser.isDeleted = true;
           
            
            await _userRepository.UpdateAsync(existinguser);
            return true;
        }




        public async Task<bool> UpdateUserAsync(UserDto dto)
        {
            ArgumentNullException.ThrowIfNull(dto,nameof(dto));
            // to update any data from the database , we first need to fetch the existing data from the database
            var existinguser = await _userRepository.GetAsync(u => u.Id == dto.Id, true);
            if(existinguser == null)
            {
                throw new Exception($"user not found");
            }
            var usertoUpdate = _mapper.Map<User>(dto);
            usertoUpdate.DeletedDate = DateTime.Now; // this should be modified date 
            // change any additional details like isActive , updated by , updated date
            await _userRepository.UpdateAsync(usertoUpdate);
            return true;
        }


        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllByAnyAsync(u=>!u.isDeleted);
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetAsync(user=>user.Id==id && !user.isDeleted);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetUserByNameAsync(string username)
        {
            var users = await _userRepository.GetAsync(user=>user.Username==username && !user.isDeleted);
            return _mapper.Map<UserDto>(users);
        }

        public async Task<bool> CreateUserAsync(UserDto dto)
        {
            if (dto == null)
            {

                throw new Exception("is null");
            }
            //ArgumentNullException.ThrowIfNull(dto,"argument is null");
            var existingUser = await _userRepository.GetAsync(u => u.Username.Equals(dto.Username));
            if (existingUser != null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            User user = _mapper.Map<User>(dto);
            user.isDeleted = false;
            user.CreatedDate = DateTime.Now;
            user.DeletedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHash(dto.Password);
                user.password = passwordHash.PasswordHash;
                user.passwordSalt = passwordHash.Salt;
                
            }
            await _userRepository.CreateAsync(user);
            return true;
        }

        public (string PasswordHash,string Salt) CreatePasswordHash(string password)
        {

            // create salt

            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // create passwordhash
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(

                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount:10000,
                numBytesRequested:256/8

                )
                );


            return (hash,Convert.ToBase64String(salt));
        }
    }
}
