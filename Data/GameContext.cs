using Microsoft.EntityFrameworkCore;
using W10.Models;
using W10.Models.Abilities;

namespace W10.Data;

/// <summary>
/// GameContext - The bridge between C# objects and the database.
///
/// This class demonstrates:
/// 1. DbSet properties for each table
/// 2. TPH (Table-Per-Hierarchy) configuration
/// 3. Many-to-many relationship configuration
/// </summary>
public class GameContext : DbContext
{
    /// <summary>
    /// The Rooms table
    /// </summary>
    public DbSet<Room> Rooms { get; set; }

    /// <summary>
    /// The Characters table - stores BOTH Players AND Goblins (TPH)
    /// Use _context.Characters.OfType<Player>() to get only Players
    /// </summary>
    public DbSet<Character> Characters { get; set; }

    /// <summary>
    /// The Abilities table - stores BOTH PlayerAbility AND GoblinAbility (TPH)
    /// </summary>
    public DbSet<Ability> Abilities { get; set; }

    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ============================================
        // TPH #1: Character Hierarchy
        // ============================================
        // This creates ONE table "Characters" with a Discriminator column.
        // When EF reads a row, it looks at Discriminator to know which
        // C# class to create (Player or Goblin).
        modelBuilder.Entity<Character>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Player>("Player")
            .HasValue<Goblin>("Goblin");

        // ============================================
        // TPH #2: Ability Hierarchy (YOUR ASSIGNMENT)
        // ============================================
        // Same pattern! ONE table "Abilities" with an AbilityType discriminator.
        // PlayerAbility rows have AbilityType = "PlayerAbility"
        // GoblinAbility rows have AbilityType = "GoblinAbility"
        modelBuilder.Entity<Ability>()
            .HasDiscriminator<string>("AbilityType")
            .HasValue<PlayerAbility>("PlayerAbility")
            .HasValue<GoblinAbility>("GoblinAbility");

        // ============================================
        // Many-to-Many: Characters <-> Abilities
        // ============================================
        // One character can have MANY abilities.
        // One ability can be known by MANY characters.
        // EF Core creates a join table "CharacterAbilities" automatically!
        modelBuilder.Entity<Character>()
            .HasMany(c => c.Abilities)
            .WithMany(a => a.Characters)
            .UsingEntity(j => j.ToTable("CharacterAbilities"));

        base.OnModelCreating(modelBuilder);
    }
}