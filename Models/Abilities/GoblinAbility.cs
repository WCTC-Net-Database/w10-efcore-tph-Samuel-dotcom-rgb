namespace W10.Models.Abilities;

/// <summary>
/// A goblin-specific ability with a Taunt property.
///
/// This class inherits from Ability and adds its own property (Taunt).
/// In the database, Taunt will be NULL for any non-GoblinAbility rows.
/// </summary>
public class GoblinAbility : Ability
{
    /// <summary>
    /// How effective the taunt is (goblin-specific stat)
    /// </summary>
    public int Taunt { get; set; }

    /// <summary>
    /// Goblin abilities taunt and distract enemies!
    /// </summary>
    public override void Activate(Character user, Character target)
    {
        Console.WriteLine($"{user.Name} uses {Name} on {target.Name}!");
        Console.WriteLine($"  {Description}");
        Console.WriteLine($"  {target.Name} is distracted with taunt level {Taunt}!");
    }
}
