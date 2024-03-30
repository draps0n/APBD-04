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
        var firstName = "";
        var lastName = "Doe";
        var email = "johndoe@gmail.com";
        var dateOfBirth = DateTime.Parse("1982-03-21");
        var clientId = 1;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Incorrect()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "johndoegmailcom";
        var dateOfBirth = DateTime.Parse("1982-03-21");
        var clientId = 1;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Underage_Same_Month_As_Current()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "johndoe@gmail.com";
        var dateOfBirth = DateTime.Parse("2010-03-21");
        var clientId = 1;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Underage_Month_After_Current()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "johndoe@gmail.com";
        var dateOfBirth = DateTime.Parse("2003-04-21");
        var clientId = 1;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Throw_ArgumentException_When_Client_Doesnt_Exist()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Doe";
        var email = "johndoe@gmail.com";
        var dateOfBirth = DateTime.Parse("1982-03-21");
        var clientId = 0;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => userService.AddUser(firstName, lastName, email, dateOfBirth, clientId)
        );
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Normal_Client_And_Credit_Limit_Too_Low()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Kowalski";
        var email = "johndoe@gmail.com";
        var dateOfBirth = DateTime.Parse("1999-04-21");
        var clientId = 1;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

        // Assert
        Assert.False(addResult);
    }

     [Fact]
     public void AddUser_Should_Return_True_When_Normal_Client_And_Credit_Limit_OK()
     {
         // Arrange
         var firstName = "John";
         var lastName = "Kwiatkowski";
         var email = "kwiatkowski@wp.pl";
         var dateOfBirth = DateTime.Parse("1999-04-21");
         var clientId = 5;
         var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());

         // Act
         var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);

         // Assert
         Assert.True(addResult);
     }

    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client_And_Credit_Limit_OK()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Smith";
        var email = "smith@gmail.pl";
        var dateOfBirth = DateTime.Parse("1999-04-21");
        var clientId = 3;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());
    
        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
    
        // Assert
        Assert.True(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_True_When_Client_Is_Very_Important()
    {
        // Arrange
        var firstName = "John";
        var lastName = "Malewski";
        var email = "malewski@gmail.pl";
        var dateOfBirth = DateTime.Parse("1999-04-21");
        var clientId = 2;
        var userService = new UserService(new FakeClientRepository(), new FakeUserCreditService(), new AdapterUserDataAccess());
    
        // Act
        var addResult = userService.AddUser(firstName, lastName, email, dateOfBirth, clientId);
    
        // Assert
        Assert.True(addResult);
    }
}