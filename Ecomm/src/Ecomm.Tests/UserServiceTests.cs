using AutoMapper;
using Ecomm.Application.DTOs;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Services;
using Ecomm.Domain.Entities;
using Moq;
namespace Ecomm.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IJwtService> _jwtServiceMock = new();
        private readonly IMapper _mapper;

        public UserServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TestMappings.TestProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnUserDto()
        {
            // Arrange
            var service = new UserService(_userRepoMock.Object, _mapper, _jwtServiceMock.Object);
            var dto = new CreateUserDto
            {
                FullName = "Test User",
                Email = "test@example.com",
                Password = "123456"
            };

            // Act
            var result = await service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.FullName, result.FullName);
            Assert.Equal(dto.Email, result.Email);
            _userRepoMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnUserList()
        {
            // Arrange
            var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), FullName = "John", Email = "john@example.com", PasswordHash = "hash" }
        };

            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            var service = new UserService(_userRepoMock.Object, _mapper, _jwtServiceMock.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result.First().FullName);
        }

        [Fact]
        public async Task LoginAsync_WithValidUser_ReturnsToken()
        {
            // Arrange
            var password = "123456";
            var hashed = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Luiz",
                Email = "luiz@example.com",
                PasswordHash = hashed
            };

            _userRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User> { user });
            _jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("fake-token");

            var service = new UserService(_userRepoMock.Object, _mapper, _jwtServiceMock.Object);

            // Act
            var result = await service.LoginAsync(new LoginRequestDto
            {
                Email = "luiz@example.com",
                Password = "123456"
            });

            // Assert
            Assert.NotNull(result.Token);
            Assert.Equal("fake-token", result.Token);
        }
    }
}
