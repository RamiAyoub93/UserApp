using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using MainApp.Models;
using MainApp.Services;

public class UserServiceTests
{
    [Fact]
    public void Add_ShouldAddUserToListAndSaveToFile()
    {
        // Arrange
        string testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string testFileName = "testUsers.json";
        var fileService = new FileService(testDirectory, testFileName);
        var userService = new UserServiceWrapper(fileService);

        var newUser = new User { Id = "1", FirstName = "Alice" };

        // Act
        userService.Add(newUser);

        // Assert
        var filePath = Path.Combine(testDirectory, testFileName);
        Assert.True(File.Exists(filePath), "File was not created.");

        var savedUsers = fileService.LoadListFromFile();
        Assert.Single(savedUsers);
        Assert.Equal("Alice", savedUsers[0].FirstName);

        // Cleanup
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, true);
    }

    [Fact]
    public void GetAll_ShouldLoadUsersFromFile()
    {
        // Arrange
        string testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string testFileName = "testUsers.json";
        var fileService = new FileService(testDirectory, testFileName);
        var userService = new UserServiceWrapper(fileService);

        var users = new List<User>
        {
            new User { Id = "1", FirstName = "Alice" },
            new User { Id = "2", FirstName = "Bob" }
        };

        // Simulate saving users to file
        fileService.SaveListToFile(users);

        // Act
        var loadedUsers = userService.GetAll();

        // Assert
        Assert.Equal(2, loadedUsers.Count());
        Assert.Equal("Alice", loadedUsers.First().FirstName);
        Assert.Equal("Bob", loadedUsers.Last().FirstName);

        // Cleanup
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, true);
    }
}

public class UserServiceWrapper : UserService
{
    public UserServiceWrapper(FileService fileService)
    {
        // Replace the internal FileService with the provided one for testing
        SetFileService(fileService);
    }

    private void SetFileService(FileService fileService)
    {
        typeof(UserService)
            .GetField("_fileService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, fileService);
    }
}
