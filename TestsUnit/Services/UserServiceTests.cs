using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Core.Repository;
using Trans.Infrastructure.Dto;
using Trans.Infrastructure.Services;
using Xunit;

namespace Tests.Services
{
    public class UserServiceTests
    {

        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var passwordEncrypterMock = new Mock<IPasswordEncrypter>();
            var autoMapperMock = new Mock<IMapper>();
            passwordEncrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            passwordEncrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");
            var userService = new UserService(userRepositoryMock.Object, autoMapperMock.Object, passwordEncrypterMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(), "test@email.com", "secret", "user", "user", "user");

            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public async Task when_invoking_get_async_this_invoke_get_async_on_user_repository()
        {
            //Arrange
            var user = new User(Guid.NewGuid(), "test@email.com", "secret", "user", "user", "salt", "user");
            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };


            var userRepositoryMock = new Mock<IUserRepository>();
            var autoMapperMock = new Mock<IMapper>();
            var passwordEncrypterMock = new Mock<IPasswordEncrypter>();
            var userService = new UserService(userRepositoryMock.Object, autoMapperMock.Object, passwordEncrypterMock.Object);

            userRepositoryMock.Setup(x => x.GetAsync(user.Email)).ReturnsAsync(user);
            autoMapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);

            //Act
            var returnUserDto = await userService.GetAsync(user.Email);
            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Email), Times.Once());
            userDto.Should().NotBeNull();
            userDto.Email.Should().Be(user.Email);
        }
    }
}
