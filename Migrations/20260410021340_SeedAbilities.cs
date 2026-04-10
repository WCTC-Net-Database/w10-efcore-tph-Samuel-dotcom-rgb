using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W10.Migrations
{
    /// <inheritdoc />
    public partial class SeedAbilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Enable identity insert for Abilities
            migrationBuilder.Sql("SET IDENTITY_INSERT Abilities ON");

            // Insert PlayerAbilities
            migrationBuilder.Sql("INSERT INTO Abilities (Id, Name, Description, AbilityType, Shove) VALUES (1, 'Power Shove', 'A powerful shove that pushes enemies back', 'PlayerAbility', 15)");
            migrationBuilder.Sql("INSERT INTO Abilities (Id, Name, Description, AbilityType, Shove) VALUES (2, 'Mighty Push', 'An incredibly strong push', 'PlayerAbility', 25)");

            // Insert GoblinAbilities
            migrationBuilder.Sql("INSERT INTO Abilities (Id, Name, Description, AbilityType, Taunt) VALUES (3, 'Mocking Laugh', 'A taunting laugh that distracts enemies', 'GoblinAbility', 8)");
            migrationBuilder.Sql("INSERT INTO Abilities (Id, Name, Description, AbilityType, Taunt) VALUES (4, 'Cruel Taunt', 'A cruel insult that enrages enemies', 'GoblinAbility', 12)");

            // Disable identity insert for Abilities
            migrationBuilder.Sql("SET IDENTITY_INSERT Abilities OFF");

            // Associate abilities with characters
            // Sir Lancelot (Player) gets Power Shove and Mighty Push
            migrationBuilder.Sql("INSERT INTO CharacterAbilities (AbilitiesId, CharactersId) VALUES (1, 3)");
            migrationBuilder.Sql("INSERT INTO CharacterAbilities (AbilitiesId, CharactersId) VALUES (2, 3)");

            // Bob Goblin gets Mocking Laugh and Cruel Taunt
            migrationBuilder.Sql("INSERT INTO CharacterAbilities (AbilitiesId, CharactersId) VALUES (3, 1)");
            migrationBuilder.Sql("INSERT INTO CharacterAbilities (AbilitiesId, CharactersId) VALUES (4, 1)");

            // Gob Boglin gets Mocking Laugh
            migrationBuilder.Sql("INSERT INTO CharacterAbilities (AbilitiesId, CharactersId) VALUES (3, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete associations
            migrationBuilder.Sql("DELETE FROM CharacterAbilities WHERE AbilitiesId IN (1, 2, 3, 4)");

            // Delete Abilities
            migrationBuilder.Sql("DELETE FROM Abilities WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
