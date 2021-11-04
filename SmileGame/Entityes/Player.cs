using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame.Entityes
{
    class Player : Allive
    {
        public PlayerDataStorage DataStorage { get; set; }
        public Player(string name, char renderChar, Cell cLink, PlayerDataStorage pds, EntityType type = EntityType.Neutral) : base(name, renderChar, cLink, type)
        {
            DataStorage = pds;
            Health = pds.Health;
        }
        public override void Move()
        {
            Cell cell = null;
            Dir = DataStorage.Dir;
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
            if (cell!=null && cell?.EntityHolder == null)
            {
                cell.EntityHolder = this;
                CellLink.EntityHolder = null;
                CellLink = cell;
            }
        }
    }
}
