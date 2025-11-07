using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.DTOs.Responses;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Security.Claims;
using System.Text;

namespace ProfServer.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IRoleService roleService, ITokenService tokenService, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _tokenService = tokenService;
            _mapper = mapper;
            _logger = logger;
        }

        private byte[] HashPassword(byte[] password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            return sha256.ComputeHash(password);
        }

        public async Task<TokenResponse> Authenticate(AuthRequest request)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(request.Password);
                var userId = await _userRepository.UserExistsAsync(request.Login, HashPassword(bytes));
                if (userId == 0)
                    throw new AuthException();

                var user = await GetUserByIdAsync(userId);

                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, user.Id.ToString())
                };

                var token = _tokenService.GenerateToken(claims);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during authentication for login {Login}", request.Login);
                throw;
            }
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                var role = await _roleService.GetRoleByIdAsync(request.RoleId);

                User user = _mapper.Map<User>(request);

                var userId = await _userRepository.CreateUserAsync(user);
                return await GetUserByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user with login {Login}", request.Login);
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                await GetUserByIdAsync(id);

                return await _userRepository.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with id {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();

                foreach (var user in users)
                {
                    user.Role = await _roleService.GetRoleByIdAsync(user.RoleId);
                }

                return _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw;
            }
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if(user == null)
                    throw new NotFoundException(nameof(User), id);

                user.Role = await _roleService.GetRoleByIdAsync(user.RoleId);

                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with id {Id}", id);
                throw;
            }
        }

        public async Task<UserDTO> UpdateUserAsync(UpdateUserRequest request)
        {
            try
            {
                await GetUserByIdAsync(request.Id);

                await _roleService.GetRoleByIdAsync(request.RoleId);

                User user = _mapper.Map<User>(request);

                if (!await _userRepository.UpdateUserAsync(user))
                    throw new ConflictException("Failed to update user.");

                return await GetUserByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user with id {Id}", request.Id);
                throw;
            }
        }
    }
}
