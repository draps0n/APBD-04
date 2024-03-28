using System;
using JetBrains.Annotations;
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
    public void AddUser_Should_Return_False_When_Client_Underage_Same_Month_As_Current()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2010-03-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Underage_Month_After_Current()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2003-04-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Doesnt_Exist()
    {
        // Arrange
        var userService = new UserService();

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 0)
        );
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_And_Credit_Limit_Too_Low()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Kowalski", "johndoe@gmail.com", DateTime.Parse("1999-04-21"), 1);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Normal_Client_And_Credit_Limit_OK()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Kwiatkowski", "kwiatkowski@wp.pl", DateTime.Parse("1999-04-21"), 5);

        // Assert
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client_And_Credit_Limit_OK()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Smith", "smith@gmail.pl", DateTime.Parse("1999-04-21"), 3);

        // Assert
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Very_Important()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var addResult = userService.AddUser("John", "Malewski", "malewski@gmail.pl", DateTime.Parse("1999-04-21"), 2);

        // Assert
        Assert.True(addResult);
    }
}