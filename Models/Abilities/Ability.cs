namespace W10.Models.Abilities;

/// <summary>
/// Abstract base class for all abilities - demonstrates TPH inheritance.
///
/// TABLE-PER-HIERARCHY (TPH) EXPLAINED:
/// ==================================
/// In TPH, all types in the hierarchy are stored in ONE table.
/// EF Core uses a "discriminator" column to know which type each row is.
///
/// Example of what the Abilities table looks like:
/// | Id | Name      | Description              | AbilityType    | Shove | Taunt |
/// |----|-----------|--------------------------|----------------|-------|-------|
/// | 1  | Push      | Push enemy back          | PlayerAbility  | 10    | NULL  |
/// | 2  | Mock      | Taunt the enemy          | GoblinAbility  | NULL  | 5     |
///
/// Notice:
/// - AbilityType is the DISCRIMINATOR - it tells EF Core which class to create
/// - Shove is only used by PlayerAbility (NULL for others)
/// - Taunt is only used by GoblinAbility (NULL for others)
///
/// This is the same pattern used for Character (Player vs Goblin)!
/// </summary>
public abstract class Ability
{
    /// <summary>
    /// Primary key - every entity needs one
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the ability (e.g., "Power Strike", "Sneak Attack")
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of what the ability does
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Navigation property - which characters have this ability?
    /// This creates a MANY-TO-MANY relationship:
    /// - One Character can have MANY Abilities
    /// - One Ability can belong to MANY Characters
    /// </summary>
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    /// <summary>
    /// Abstract method - each ability type must define how it activates.
    /// This is like PerformSpecialAction from Week 6!
    /// </summary>
    public abstract void Activate(Character user, Character target);
}
