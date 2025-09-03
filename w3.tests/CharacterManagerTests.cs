using w3.console;

namespace w3.tests;

public class CharacterManagerTests
{
    [Fact]
    public void DisplayCharacters_ShouldOutputAllCharacters()
    {
        // Arrange
        var input = new MockInput();
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.DisplayCharacters();

        // Assert
        Assert.Contains("John, Brave", output.Output);
        Assert.Contains("Jane", output.Output);
        Assert.Contains("Bob, Sneaky", output.Output);
        Assert.Contains("Alice", output.Output);
        Assert.Contains("Reginald III, Sir", output.Output);
    }

    [Fact]
    public void AddCharacter_ShouldAppendCharacterToFile()
    {
        // Arrange
        var input = new MockInput(new[] { "Eve", "Sorcerer", "2", "wand", "cloak", "ring", "" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.AddCharacter();

        // Assert
        Assert.Contains("Eve,Sorcerer,2,wand|cloak|ring", output.Output);
        // Integration test would verify file update
    }

    [Fact]
    public void LevelUpCharacter_ShouldIncreaseCharacterLevel()
    {
        // Arrange
        var input = new MockInput(new[] { "Reginald III, Sir" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.LevelUpCharacter();

        // Assert
        Assert.Contains("Reginald III, Sir is now level 6.", output.Output);
    }

    [Fact]
    public void FindCharacter_ShouldReturnCharacterData_WhenFound()
    {
        // Arrange
        var input = new MockInput(new[] { "Bob, Sneaky" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.FindCharacter();

        // Assert
        Assert.Contains("Bob, Sneaky", output.Output);
        Assert.Contains("Rogue", output.Output);
        Assert.Contains("3", output.Output);
        Assert.Contains("dagger|lockpick|cloak", output.Output);
    }

    [Fact]
    public void FindCharacter_ShouldNotify_WhenNotFound()
    {
        // Arrange
        var input = new MockInput(new[] { "Nonexistent" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.FindCharacter();

        // Assert
        Assert.Contains("Character not found", output.Output);
    }
}
