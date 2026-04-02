using W10.Models.Abilities;

namespace W10.Models;

/// <summary>
/// Abstract base class for all characters - demonstrates TPH inheritance.
///
/// This class is part of a Table-Per-Hierarchy:
/// - Character (abstract - this class)
///   - Player (concrete - inherits from Character)
///   - Goblin (concrete - inherits from Character)
///
/// All are stored in ONE "Characters" table with a Discriminator column.
/// </summary>
public abstract class Character : ICharacter
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }

    // Foreign key to Room
    public int RoomId { get; set; }

    // Navigation property to Room
    public virtual Room Room { get; set; }

    /// <summary>
    /// MANY-TO-MANY: A character can have multiple abilities.
    /// This creates a join table "CharacterAbilities" in the database.
    ///
    /// Use .Include(c => c.Abilities) when querying to load them.
    /// </summary>
    public virtual ICollection<Ability> Abilities { get; set; } = new List<Ability>();

    public virtual void Attack(ICharacter target)
    {
        Console.WriteLine($"{Name} attacks {target.Name}!");
    }

    /// <summary>
    /// Uses an ability on a target.
    /// This demonstrates polymorphism - the actual ability type determines behavior.
    /// </summary>
    public virtual void UseAbility(Ability ability, ICharacter target)
    {
        if (target is Character characterTarget)
        {
            ability.Activate(this, characterTarget);
        }
    }
}