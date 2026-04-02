using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W10.Models
{
    public class Player : Character
    {
        public int Experience { get; set; }

        public override void Attack(ICharacter target)
        {
            // Player-specific attack logic
            Console.WriteLine($"{Name} attacks {target.Name} with a sword!");
        }
    }
}
