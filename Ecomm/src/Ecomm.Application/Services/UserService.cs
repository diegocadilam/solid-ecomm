using AutoMapper;
using Ecomm.Application.DTOs;
using Ecomm.Application.Interfaces;
using Ecomm.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Ecomm.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository repository, IMapper mapper, IJwtService jwtService)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(Guid id, CreateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id) ?? throw new Exception("User not found");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.PasswordHash = HashPassword(dto.Password);

            await _repository.UpdateAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var users = await _repository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);

            return new LoginResponseDto { Token = token };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(sha256.ComputeHash(bytes));
        }

        private bool VerifyPassword(string password, string hash)
            => HashPassword(password) == hash;
    }
}
