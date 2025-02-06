using Xunit;

namespace CharacterConsole.Tests;

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
        Assert.Contains("John,Fighter,1,sword|shield", output.Output);
        Assert.Contains("Jane,Wizard,2,staff|robe", output.Output);
    }

    [Fact]
    public void AddCharacter_ShouldAppendCharacterToFile()
    {
        // Arrange
        var input = new MockInput(new[] { "Alice", "Cleric", "3", "mace|armor" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.AddCharacter();

        // Assert
        Assert.Contains("Alice,Cleric,3,mace|armor", output.Output);
        // Additional check to verify if the character is added to the file would be done in integration tests
    }

    [Fact]
    public void LevelUpCharacter_ShouldIncreaseCharacterLevel()
    {
        // Arrange
        var input = new MockInput(new[] { "1" });
        var output = new MockOutput();
        var manager = new CharacterManager(input, output);

        // Act
        manager.LevelUpCharacter();

        // Assert
        Assert.Contains("John is now level 2.", output.Output);
    }
}