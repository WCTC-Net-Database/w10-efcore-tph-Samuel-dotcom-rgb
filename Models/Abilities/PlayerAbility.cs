namespace W10.Models.Abilities;

/// <summary>
/// A player-specific ability with a Shove property.
///
/// This class inherits from Ability and adds its own property (Shove).
/// In the database, Shove will be NULL for any non-PlayerAbility rows.
/// </summary>
public class PlayerAbility : Ability
{
    /// <summary>
    /// How far the shove pushes the enemy (player-specific stat)
    /// </summary>
    public int Shove { get; set; }

    /// <summary>
    /// Player abilities push enemies back!
    /// </summary>
    public override void Activate(Character user, Character target)
    {
        Console.WriteLine($"{user.Name} uses {Name} on {target.Name}!");
        Console.WriteLine($"  {Description}");
        Console.WriteLine($"  {target.Name} is pushed back {Shove} feet!");
    }
}
