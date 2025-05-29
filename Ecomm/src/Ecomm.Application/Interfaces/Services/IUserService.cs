using Ecomm.Application.DTOs;
using Ecomm.Application.DTOs.Users;
using Ecomm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task DeleteAsync(Guid id);
        Task<UserDto> UpdateAsync(Guid id, UpdateUserDto dto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
