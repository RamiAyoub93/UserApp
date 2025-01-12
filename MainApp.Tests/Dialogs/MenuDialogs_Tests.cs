using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using MainApp.Models;
using MainApp.Services;
using MainApp.Dialogs;

public class MenuDialogsTests
{
    [Fact]
    public void CreateUserDialog_ShouldAddNewUser()
    {
        // Arrange
        var userService = new UserServiceWrapper();
        var menuDialogs = new MenuDialogs(userService);

        var input = new StringReader("Alice\nJohnson\nalice@example.com\n123456789\n123 Main St\n10001\n");
        Console.SetIn(input);

        // Act
        menuDialogs.CreateUserDialog();

        // Assert
        var users = userService.GetAll();
        Assert.Single(users);
        var user = users.First();
        Assert.Equal("Alice", user.FirstName);
        Assert.Equal("Johnson", user.LastName);
        Assert.Equal("alice@example.com", user.Email);
        Assert.Equal("123456789", user.Phonenumber);
        Assert.Equal("123 Main St", user.Address);
        Assert.Equal("10001", user.ZipCode);
    }

    [Fact]
    public void ViewAllUsersDialog_ShouldDisplayAllUsers()
    {
        // Arrange
        var userService = new UserServiceWrapper();
        var menuDialogs = new MenuDialogs(userService);

        userService.Add(new User
        {
            Id = "1",
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice@example.com",
            Phonenumber = "123456789",
            Address = "123 Main St",
            ZipCode = "10001"
        });

        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        menuDialogs.ViewAllUsersDialog();

        // Assert
        var consoleOutput = output.ToString();
        Assert.Contains("Alice Johnson", consoleOutput);
        Assert.Contains("alice@example.com", consoleOutput);
        Assert.Contains("123456789", consoleOutput);
        Assert.Contains("123 Main St", consoleOutput);
        Assert.Contains("10001", consoleOutput);
    }

    [Fact]
    public void MenuOptionsDialog_ShouldNavigateToCorrectDialog()
    {
        // Arrange
        var userService = new UserServiceWrapper();
        var menuDialogs = new MenuDialogs(userService);

        var input = new StringReader("1\nAlice\nJohnson\nalice@example.com\n123456789\n123 Main St\n10001\n2\n");
        Console.SetIn(input);

        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        menuDialogs.MenuOptionsDialog();

        // Assert
        var consoleOutput = output.ToString();
        Assert.Contains("MAIN MENU", consoleOutput);
        Assert.Contains("New User", consoleOutput);
        Assert.Contains("View Users", consoleOutput);
        Assert.Contains("Alice Johnson", consoleOutput);
        Assert.Contains("alice@example.com", consoleOutput);
    }
}

// Wrapper for UserService to avoid reliance on FileService
public class UserServiceWrapper : UserService
{
    private readonly List<User> _users = new();

    public UserServiceWrapper()
    {
    }

    public override void Add(User user)
    {
        user.Id = Guid.NewGuid().ToString(); // Simulate ID generation
        _users.Add(user);
    }

    public override IEnumerable<User> GetAll()
    {
        return _users;
    }
}

