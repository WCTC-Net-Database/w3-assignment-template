Week 3 Assignment: Implementing SOLID Principles - Single Responsibility Principle (SRP) and Introduction to LINQ
=================================================================================================================

### Overview

This week, you'll refactor the RPG character file data I/O program you developed in Week 2 to adhere to the Single Responsibility Principle (SRP). The goal is to separate the concerns of reading and writing character data into distinct classes, `CharacterReader` and `CharacterWriter`. Additionally, you'll enhance the functionality by using LINQ to search for a character by name, making your code more efficient and maintainable.

### Learning Objectives

-   **Understand and implement the Single Responsibility Principle (SRP)**: Refactor your code to ensure each class has a single responsibility.
-   **Gain experience with LINQ (Language Integrated Query)**: Use LINQ to query and manipulate data in C#, specifically to find a character by name.
-   **Practice advanced file I/O operations**: Continue working with CSV files, now with a more structured and maintainable approach.

### Assignment Tasks

#### 1\. Refactor the Code to Adhere to SRP

-   **CharacterReader Class**: Create a `CharacterReader` class responsible for reading character data from a CSV file. This class should include a method to find a specific character by name using LINQ.
-   **CharacterWriter Class**: Create a `CharacterWriter` class responsible for writing character data to a CSV file.
-   **Main Program**: Refactor your main program to use these classes, improving the structure and maintainability of your code.

#### 2\. Implement LINQ for Character Search

-   **Find Character by Name**: Add a method to the `CharacterReader` class that uses LINQ to search for a character by name. This method should return the character's data if found or notify the user if the character does not exist.

#### 3\. Update the Console Application Menu

-   Expand the menu to include the option to find a character by name:
```plaintext
1. Display Characters
2. Find Character
3. Add Character
4. Level Up Character
```
#### 4\. Ensure Proper File Handling

-   Continue to read and write character data in CSV format, ensuring the file operations are handled correctly and efficiently.

### Stretch Goal

-   **Enhance Output Formatting**: Format the output of your application either by using interpolated strings or by integrating a NuGet package to enhance the console output. Examples include:
    - Colorful.Console
    - ConsoleTables
    - Spectre.Console
    
    - You can find more information about interpolated strings here: [Interpolated Strings in C#](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated).

### Example Code Structure

Here's a basic outline of how your project structure might look after refactoring:
```graphql
YourProjectName/
│
├── CharacterReader.cs         # Class responsible for reading character data
├── CharacterWriter.cs         # Class responsible for writing character data
├── Program.cs                 # Main program file with the updated menu and logic
└── input.csv                  # CSV file containing the character data
```

## Tips

### LINQ Queries
**Using `FirstOrDefault`:**  
The `FirstOrDefault` method in LINQ is useful for finding the first element in a sequence that matches a condition. If no element is found, it returns the default value (typically `null` for reference types). For example:
```csharp
var character = characters.FirstOrDefault(c => c.Name == "John");
```
- This query searches for a character named "John" in a collection and returns the first match, or `null` if no match is found.

**Filtering with `Where`:**
The `Where` method allows you to filter elements based on a condition. For example, if you want to find all characters with a level greater than 3:
```csharp
var highLevelCharacters = characters.Where(c => c.Level > 3).ToList();
```
-   This returns a list of characters that are above level 3.

**Projecting Data with `Select`:**
The `Select` method is used to project or transform each element in a collection. For instance, if you only want the names of the characters:
```csharp
var characterNames = characters.Select(c => c.Name).ToList();
```
This returns a list containing only the names of the characters.

**Ordering with `OrderBy`:**
If you need to sort your characters by level, you can use `OrderBy`: 
```csharp
var sortedCharacters = characters.OrderBy(c => c.Level).ToList();
```
-   This sorts the characters in ascending order based on their level.

### String Interpolation Formatting Examples

**Basic String Interpolation:**
String interpolation is a simple way to include variable values inside a string. For example:
```csharp
string name = "John";
int level = 5;
Console.WriteLine($"Character: {name}, Level: {level}");
```
-   This will output: `Character: John, Level: 5`.

**Formatting Numbers:**
You can format numbers within interpolated strings. For example, to format a number as currency:
```csharp
decimal gold = 1234.56m;
Console.WriteLine($"Gold: {gold:C}");
```
-   This will output: `Gold: $1,234.56` (formatting will depend on your locale).

**Padding and Alignment:**
You can specify alignment and padding in interpolated strings. For instance:
```csharp
string name = "John";
int level = 5;
Console.WriteLine($"Name: {name,-10} | Level: {level,5}");
```

This will output:
```csharp
Name: John       | Level:     5
```
Here, `-10` indicates left alignment with padding up to 10 characters, and 5 indicates right alignment with padding up to 5 characters.

### Additional Resources
- LINQ Documentation:
Learn more about LINQ [here](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/).
- String Interpolation in C#:
Check out the full details on string interpolation [here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated).

### Submission Details

1.  **Ensure that your code is well-structured**: Use proper naming conventions and comments to explain your logic.
2.  **Test your application thoroughly**: Make sure all features work as expected, especially the LINQ query and file I/O operations.
3.  **Submit your code**: Commit your changes to GitHub Classroom using the provided assignment link.

## Rubric

| Category                          | Full Marks (100)              | Partial Marks (50-90)                   | Minimal Marks (10-40)                     | No Marks (0)                           |
|-----------------------------------|------------------------------|---------------------------------------|-----------------------------------------|----------------------------------------|
| **Single Responsibility Principle (SRP)**           | Successfully refactors the program into `CharacterReader` and `CharacterWriter` classes, each with a clear, single responsibility. | Refactors into classes but with some overlap in responsibilities or minor issues in class design. | Attempts refactoring but classes still have multiple responsibilities or significant design flaws. | No attempt to refactor or refactor fails to demonstrate SRP. |
| **LINQ Implementation**                | Correctly implements LINQ in `CharacterReader` to find a character by name, with efficient and readable code. | Uses LINQ but with some issues in logic or readability, or the method works but is not efficient. | Attempts to use LINQ but the implementation is flawed or incomplete. | Does not use LINQ or the implementation is non-functional. |
| **File I/O Operations** | The program reads and writes character data to `input.csv` with correct formatting and structure, integrating refactored classes. | Reads/writes data but with minor formatting issues or integration problems with the new classes. | Able to read but not write, or vice versa, or contains significant errors in file handling. | Does not correctly read or write data or fails to integrate new classes. |
| **Program Flow**                  | The program runs smoothly with a clear menu and allows for displaying, finding, adding, and leveling up characters without issues. | Program runs but with occasional issues in flow or logic (e.g., character not found properly). | Program flow is confusing or error-prone, making it difficult to use correctly. | Program does not run or crashes frequently. |
| **Stretch Goal: Enhanced Output Formatting (+10%)**       | Successfully implements enhanced output formatting using interpolated strings or a NuGet package, improving readability and presentation. | Implements enhanced formatting but with some issues in integration or presentation. | Attempts to implement enhanced formatting but the logic is flawed or incomplete. | No attempt to implement enhanced formatting. |
