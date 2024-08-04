using Application.Services;
using Domain.DTOs.Users;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

public class UsersServiceTests
{
  private readonly ILogger<UsersService> _logger;
  private readonly UsersService _usersService;

  public UsersServiceTests()
  {
    DotNetEnv.Env.TraversePath().Load();

    // mocking logger
    _logger = Mock.Of<ILogger<UsersService>>();

    // mocking users repository
    var repositoryMock = ConfigureUsersRepositoryMock();

    _usersService = new UsersService(_logger, repositoryMock.Object);
  }

  #region Tests
  [Fact]
  public async void TestAuthenticate_ReturnUser()
  {
    // Valid credentials
    var request = new AuthenticateUserDTO()
    {
      Email = "teste@email.com",
      Password = "teste"
    };

    var user = await _usersService.Authenticate(request);

    // Assertions
    user.Id.Should().Be(1);
    user.Email.Should().Be("teste@email.com");
    user.Nickname.Should().Be("teste");
  }

  [Fact]
  public async void TestAuthenticate_ThrowException()
  {
    // Invalid credentials
    var request = new AuthenticateUserDTO()
    {
      Email = "teste1@email.com",
      Password = "teste"
    };

    var function = async () => await _usersService.Authenticate(request);

    // Assertions
    await function.Should().ThrowAsync<UnauthorizedException>();
  }
  #endregion

  #region Mocks
  private Mock<IUsersRepository> ConfigureUsersRepositoryMock()
  {
    var repositoryMock = new Mock<IUsersRepository>();

    // setups
    repositoryMock.Setup(x => x.LoadByEmail("teste@email.com"))
      .Returns(async () =>
      {
        await Task.CompletedTask;

        return new User()
        {
          Email = "teste@email.com",
          Id = 1,
          Nickname = "teste",
          Password = "LVNzMjjCPhtRH9q0T7pjaw=="
        };
      });

    return repositoryMock;
  }
  #endregion
}