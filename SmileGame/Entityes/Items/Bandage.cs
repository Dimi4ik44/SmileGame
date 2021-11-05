using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame.Entityes.Items
{
    class Bandage : BaseItem
    {
        public int HealPover { get; set; }
        public Bandage(Cell cLink, string name = "Bandage", char renderChar = '=', ConsoleColor color = ConsoleColor.Green, EntityType type = EntityType.Neutral) : base(cLink, name, renderChar, color, type)
        {
            HealPover = 1;
            Description = $"Heal {HealPover} HP";
        }
        public override void Use(Allive user)
        {
            base.Use(user);
            if(!user.IsDeath)
            {
                user.AddHealth(1);
            }
        }
    }
}
