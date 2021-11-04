using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame
{
    class Entity
    {
        public EntityType Type { get; set; }
        public ConsoleColor Color { get; set; }
        public Field FieldLink { get; set; }
        public Cell CellLink { get; set; }
        public string Name { get; set; }
        public char RenderChar { get; set; }
        public Entity(string name, char renderChar, Cell cLink, ConsoleColor color = ConsoleColor.White, EntityType type = EntityType.Neutral)
        {
            Name = name;
            RenderChar = renderChar;
            CellLink = cLink;
            Type = type;
            Color = color;
        }
        public void SpawnOnField(Field field)
        {
            Random rnd = new Random();
            Cell cell;
            int counter = 0;
            do
            {
                cell = field.GetCell(rnd.Next(0, field.SizeX), rnd.Next(0, field.SizeY));
                if (cell.EntityHolder == null)
                {
                    cell.EntityHolder = this;
                    FieldLink = field;
                    CellLink = cell;
                    break;
                }
                counter++;
            }
            while (counter<100);
            if(counter>=99) // Debug if
            {
                throw new Exception("Entity cant spanw entity");
            }

        }
    }
}
