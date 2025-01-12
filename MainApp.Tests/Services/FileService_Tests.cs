using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using MainApp.Models;
using MainApp.Services;
using System.Text.Json;
using MainApp.Services;

public class FileServiceTests
{
    [Fact]
    public void SaveListToFile_ShouldCreateFileWithCorrectContent()
    {
        // Arrange
        string testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string testFileName = "testList.json";
        var fileService = new FileService(testDirectory, testFileName);

        var users = new List<User>
        {

            new User { Id = "1", FirstName = "Alice" },
            new User { Id = "2", FirstName = "Bob" }
        };

        // Act
        fileService.SaveListToFile(users);

        // Assert
        string filePath = Path.Combine(testDirectory, testFileName);
        Assert.True(File.Exists(filePath), "File was not created.");

        var fileContent = File.ReadAllText(filePath);
        Assert.Contains("Alice", fileContent);
        Assert.Contains("Bob", fileContent);

        // Cleanup
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, true);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnCorrectListWhenFileExists()
    {
        // Arrange
        string testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string testFileName = "testList.json";
        var fileService = new FileService(testDirectory, testFileName);

        var users = new List<User>
        {
            new User { Id = "1", FirstName = "Alice" },
            new User { Id = "2", FirstName = "Bob" }
        };

        string filePath = Path.Combine(testDirectory, testFileName);
        Directory.CreateDirectory(testDirectory);
        File.WriteAllText(filePath, JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true }));

        // Act
        var loadedUsers = fileService.LoadListFromFile();

        // Assert
        Assert.NotNull(loadedUsers);
        Assert.Equal(2, loadedUsers.Count);
        Assert.Equal("Alice", loadedUsers[0].FirstName);
        Assert.Equal("Bob", loadedUsers[1].FirstName);

        // Cleanup
        if (Directory.Exists(testDirectory))
            Directory.Delete(testDirectory, true);
    }

    [Fact]
    public void LoadListFromFile_ShouldReturnEmptyListWhenFileDoesNotExist()
    {
        // Arrange
        string testDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        string testFileName = "nonExistentFile.json";
        var fileService = new FileService(testDirectory, testFileName);

        // Act
        var loadedUsers = fileService.LoadListFromFile();

        // Assert
        Assert.NotNull(loadedUsers);
        Assert.Empty(loadedUsers);

       
    }
}
