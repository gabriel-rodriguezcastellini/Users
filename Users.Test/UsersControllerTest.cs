using ApplicationCore.Entities.UserAggregate;
using AutoMapper;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Controllers;
using Users.Api.Features.Commands;
using Users.Api.Features.Queries;
using Users.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Users.Test;

public class UsersControllerTest
{
    private readonly ILogger<UsersController> _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersControllerTest()
    {
        _logger = A.Fake<ILogger<UsersController>>();
        _mapper = A.Fake<IMapper>();
        _mediator = A.Fake<IMediator>();
    }

    [Fact]
    public async Task GetUsers_ReturnsTheCorrectNumberOfUsers()
    {
        // Arrange
        var count = 3;
        var queryDto = new QueryDto { ItemsPerPage = 10, Page = 0 };
        var fakeUsers = A.Fake<QueryResultDto<UserDto>>();
        fakeUsers.TotalItems = count;
        fakeUsers.Items = GetUsersData();
        A.CallTo(() => _mediator.Send(new GetUsersQuery(queryDto.Page, queryDto.ItemsPerPage), default)).WithAnyArguments().Returns(Task.FromResult(fakeUsers));
        var controller = new UsersController(_mediator, _mapper, _logger);

        // Act
        var actionResult = await controller.GetUsersAsync(queryDto);

        // Assert
        var result = actionResult.Result as OkObjectResult;
        var returnUsers = result.Value as QueryResultDto<UserDto>;
        Assert.NotNull(returnUsers);
        Assert.Equal(count, returnUsers.TotalItems);
        Assert.Equal(JsonSerializer.Serialize(returnUsers.Items), JsonSerializer.Serialize(fakeUsers.Items));
        Assert.True(fakeUsers.Items.Equals(returnUsers.Items));
    }

    [Fact]
    public async Task GetUser_ReturnsOneUser()
    {
        // Arrange
        var users = GetUsersData();
        A.CallTo(() => _mediator.Send(new GetUserQuery(1), default)).WithAnyArguments().Returns(Task.FromResult(users.First()));
        var controller = new UsersController(_mediator, _mapper, _logger);

        // Act
        var actionResult = await controller.GetUserAsync(1);

        // Assert
        var result = actionResult.Result as OkObjectResult;
        var returnUser = result.Value as UserDto;
        Assert.NotNull(returnUser);
        Assert.Equal(users.First().Id, returnUser.Id);
    }

    [Fact]
    public async Task AddUser_ReturnsTheAddedUser()
    {
        // Arrange
        var user = GetUsersData().First();
        A.CallTo(() => _mediator.Send(new AddUserCommand(user.Name, user.Email, user.Address, user.Phone, user.Type, user.Money), default)).WithAnyArguments().Returns(Task.FromResult(user));
        var controller = new UsersController(_mediator, _mapper, _logger);

        // Act
        var actionResult = await controller.PostAsync(user);

        // Assert
        var result = actionResult.Result as CreatedAtActionResult;
        var returnUser = result.Value as UserDto;
        Assert.NotNull(returnUser);
        Assert.Equal(user.Id, returnUser.Id);
    }

    [Fact]
    public async Task DeleteUser_DeletesOneUserAndReturnsNoContent()
    {
        // Arrange
        var expected = 204;
        var user = GetUsersData().First();
        A.CallTo(() => _mediator.Send(new DeleteUserCommand(user.Id), default)).WithAnyArguments().Returns(Task.CompletedTask);
        var controller = new UsersController(_mediator, _mapper, _logger);

        // Act
        var actionResult = await controller.DeleteAsync(user.Id);

        // Assert
        var result = actionResult as NoContentResult;
        Assert.Equal(result.StatusCode, expected);
    }

    private static List<UserDto> GetUsersData()
    {
        return new List<UserDto>
        {
            new UserDto { Id = 1, Address = "Peru 2464", Email = "Juan@marmol.com", Money = 1234, Name = "Juan", Phone = "+5491154762312", Type = UserType.Normal},
            new UserDto { Id = 2, Address = "Alvear y Colombres", Email = "Franco.Perez@gmail.com", Money = 112234, Name = "Franco", Phone = "+534645213542", Type = UserType.Premium},
            new UserDto { Id = 3, Address = "Garay y Otra Calle", Email = "Agustina@gmail.com", Money = 112234, Name = "Agustina", Phone = "+534645213543", Type = UserType.SuperUser}
        };
    }
}
