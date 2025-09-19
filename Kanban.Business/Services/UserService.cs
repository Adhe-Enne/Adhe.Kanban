using Kanban.Business.Enums;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Domain.Models;
using Kanban.Domain.Models.Enums;
using Kanban.Repository.Interfaces;
using System.Security.Cryptography;
using Task = System.Threading.Tasks.Task;

namespace Kanban.Business.Services
{
    public class UserService(IUserRepository userRepository) : Service<User>(userRepository), IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            if (await _userRepository.ExistsAsync(u => u.Email == user.Email))
                throw new BusinessException(password, BusinessErrorCode.UserAlreadyExists, "User already exists");

            CreatePasswordHash(password, out byte[] hash, out byte[] salt);
            user.PasswordHash = hash;
            user.PasswordSalt = salt;
            user.CreatedAt = DateTime.UtcNow;

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new BusinessException(email, BusinessErrorCode.UserNotFound, "User not found");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new BusinessException(email, BusinessErrorCode.UserNotFound, "Invalid password");

            return user;
        }

        public async Task UpdateRoleAsync(Guid userId, string newRole)
        {
            if (!Enum.TryParse<UserRole>(newRole, true, out var role))
                throw new BusinessException(newRole, BusinessErrorCode.InvalidRole, "Invalid role");

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new BusinessException(userId.ToString(), BusinessErrorCode.UserNotFound, "User not found");

            user.Role = role;
            await _userRepository.UpdateAsync(user);
        }

        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            using var hmac = new HMACSHA512(salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(hash);
        }

        public async Task InactivateUserAsync(Guid userId)
        {
            await SetStateUser(userId, false);
        }

        public async Task ActivateUserAsync(Guid userId)
        {
            await SetStateUser(userId, true);
        }

        private async Task SetStateUser(Guid userId, bool isActive)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new BusinessException("Usuario no encontrado", BusinessErrorCode.UserNotFound);

            user.IsActive = isActive;
            await _userRepository.UpdateAsync(user);
        }

    }
}
