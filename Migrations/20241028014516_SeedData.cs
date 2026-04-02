using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W10.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Enable identity insert for Rooms
            migrationBuilder.Sql("SET IDENTITY_INSERT Rooms ON");

            // Insert Rooms
            migrationBuilder.Sql("INSERT INTO Rooms (Id, Name, Description) VALUES (1, 'Dungeon', 'A dark and damp dungeon')");
            migrationBuilder.Sql("INSERT INTO Rooms (Id, Name, Description) VALUES (2, 'Forest', 'A lush green forest')");

            // Disable identity insert for Rooms
            migrationBuilder.Sql("SET IDENTITY_INSERT Rooms OFF");

            // Enable identity insert for Characters
            migrationBuilder.Sql("SET IDENTITY_INSERT Characters ON");

            // Insert Characters
            migrationBuilder.Sql("INSERT INTO Characters (Id, Name, Level, RoomId, Discriminator, AggressionLevel) VALUES (1, 'Bob Goblin', 1, 1, 'Goblin', 5)");
            migrationBuilder.Sql("INSERT INTO Characters (Id, Name, Level, RoomId, Discriminator, AggressionLevel) VALUES (2, 'Gob Boglin', 2, 1, 'Goblin', 7)");
            migrationBuilder.Sql("INSERT INTO Characters (Id, Name, Level, RoomId, Discriminator, Experience) VALUES (3, 'Sir Lancelot', 1, 2, 'Player', 100)");

            // Disable identity insert for Characters
            migrationBuilder.Sql("SET IDENTITY_INSERT Characters OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete Characters
            migrationBuilder.Sql("DELETE FROM Characters WHERE Id IN (1, 2, 3)");

            // Delete Rooms
            migrationBuilder.Sql("DELETE FROM Rooms WHERE Id IN (1, 2)");
        }
    }
}
