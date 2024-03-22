using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Missing()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Incorrect()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Underage()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2010-03-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Doesnt_Exist()
    {
        // Arrange
        var userService = new UserService();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 0);
        });
    }
}