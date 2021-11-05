using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame.Entityes.Items
{
    class ShineCrystal : BaseItem
    {
        public ShineCrystal(Cell cLink, string name = "ShineCrystal", char renderChar = '@', ConsoleColor color = ConsoleColor.Blue, EntityType type = EntityType.Neutral) : base(cLink, name, renderChar, color, type)
        {
            Description = $"Teleport user at random pos";
        }
        public override void Use(Allive user)
        {
            base.Use(user);
            user.CellLink.EntityHolder = null;
            user.FieldLink.SpawnEntity(user);
        }
    }
}
