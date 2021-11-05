using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame.Entityes.Items
{
    abstract class BaseItem : Entity
    {
        public string Description { get; set; }
        public BaseItem(Cell cLink, string name = "someItem", char renderChar = '?', ConsoleColor color = ConsoleColor.White, EntityType type = EntityType.Neutral) : base(name, renderChar, cLink, color, type)
        {
        }
        public virtual void Use(Allive user)
        {
            CellLink.EntityHolder = null;
            CellLink = null;
        }
    }
}
