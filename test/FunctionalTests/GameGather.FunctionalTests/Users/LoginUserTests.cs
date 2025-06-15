using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using GameGather.Application.Contracts.Users;
using GameGather.Application.Features.Users.Commands.LoginUser;
using GameGather.FunctionalTests.Abstractions;
using Microsoft.AspNetCore.Mvc.Testing;
using static GameGather.FunctionalTests.Users.TestUtils.LoginUserCommandBuilder;

namespace GameGather.FunctionalTests.Users;

public class LoginUserTests : BaseFunctionalTest
{
    public LoginUserTests(FunctionalTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnConflict_WhenUserWithThatEmailNotFound()
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithEmail("random_email@gmail.com")
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.Conflict);
    }
    
    [Fact]
    public async Task Should_ReturnConflict_WhenPasswordIsWrong()
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithPassword("wrong_password")
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.Conflict);
    }
    
    [Fact]
    public async Task Should_ReturnUnauthorized_WhenEmailNotVerified()
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithNotVerifiedEmail()
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.Unauthorized);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("invalid_email")]
    [InlineData("invalid_email@")]
    [InlineData("@domain")]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid(string email)
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithEmail(email)
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Should_ReturnBadRequest_WhenPasswordIsInvalid(string password)
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithPassword(password)
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Should_ReturnOkWithoutToken_WhenPasswordIsExpired()
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .WithExpiredPassword()
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<LoginUserResponse>();
        
        result
            .Should()
            .NotBeNull();
        result!.Token
            .Should()
            .BeNullOrEmpty();
    }
    
    [Fact]
    public async Task Should_ReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange

        var request = GivenLoginUserCommand()
            .Build();
        
        // Act
        
        var response = await HttpClient.PostAsJsonAsync("/api/login",
            request);
        
        // Assert
        
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<LoginUserResponse>();
        
        result
            .Should()
            .NotBeNull();
        result!.Token
            .Should()
            .NotBeNullOrEmpty();
    }
}