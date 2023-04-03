using ApplicationCore.Entities.UserAggregate;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Users.Api.Models;
using Users.IntegrationTests.Helpers;
using System.Net;
using Xunit;

namespace Users.IntegrationTests.IntegrationTests;

public class UsersControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;

    public UsersControllerTest(CustomWebApplicationFactory<Program> customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;

        _httpClient = customWebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        using var scope = _customWebApplicationFactory.Services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<UserContext>();
        Utilities.ReinitializeDbForTestsAsync(db).Wait();
    }

    [Fact]
    public async Task GetUsers_ReturnsAllUsers()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;        

        // Act
        var response = await _httpClient.GetAsync("/api/users");

        // Arrange
        Assert.Equal(expectedStatusCode, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<QueryResultDto<UserDto>>();
        Assert.NotNull(result);
        Assert.Equal(3, result.TotalItems);
        Assert.Equal("Juan", result.Items.First().Name);
        Assert.Equal("Juan@marmol.com", result.Items.First().Email);
    }    

    [Fact]
    public async Task GetUser_ReturnsAUserById()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.OK;        

        // Act
        var response = await _httpClient.GetAsync("/api/users/1");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.NotNull(result);
        Assert.Equal("Juan", result.Name);
        Assert.Equal("Juan@marmol.com", result.Email);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenWeSearchANonExistentUser()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.NotFound;

        // Act
        var response = await _httpClient.GetAsync("/api/users/4");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ReturnsTheNewlyCreatedUser()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.Created;        

        var user = new UserDto
        {
            Address = "Alberdi 1555",
            Email = "gabriel.rodriguezcastellini@outlook.com",
            Money = 100,
            Name = "Gabriel",
            Phone = "+5493413615916",
            Type = UserType.SuperUser
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users", user);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserDto>();
        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Phone, result.Phone);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenTryingToCreateAUserWithDuplicatedEmail()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.BadRequest;

        var user = new UserDto
        {
            Address = "Alberdi 1555",
            Email = "Juan@marmol.com",
            Money = 100,
            Name = "Gabriel",
            Phone = "+5493413615916",
            Type = UserType.SuperUser
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync("/api/users", user);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);        
    }

    [Fact]
    public async Task Delete_DeletesAUserAndReturnsNoContent()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.NoContent;        

        // Act
        var response = await _httpClient.DeleteAsync("/api/users/1");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }    
}
