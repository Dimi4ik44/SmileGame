using SmileGame.Entityes;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame
{
    class Allive : Entity, IMovable
    {
        public Direction Dir { get; set; }
        public int Health { get; set; }
        public Allive(string name, char renderChar, Cell cLink, EntityType type = EntityType.Neutral) : base(name, renderChar, cLink, type)
        {
        }

        public bool IsPlayer { get { return this is Player; } }
        public virtual void Move()
        {
            Random rnd = new Random();
            Cell cell = null;
            Dir = (Direction)rnd.Next(0,4);
            switch (Dir)
            {
                case Direction.Up:
                    cell = FieldLink.GetCell(CellLink.Pos - Vector2.UnitY);
                    break;
                case Direction.Down:
                    cell = FieldLink.GetCell(CellLink.Pos + Vector2.UnitY);
                    break;
                case Direction.Left:
                    cell = FieldLink.GetCell(CellLink.Pos - Vector2.UnitX);
                    break;
                case Direction.Right:
                    cell = FieldLink.GetCell(CellLink.Pos + Vector2.UnitX);
                    break;
            }
            if(cell != null && cell?.EntityHolder == null)
            {
                cell.EntityHolder = this;
                CellLink.EntityHolder = null;
                CellLink = cell;
            }
        }
    }
}
