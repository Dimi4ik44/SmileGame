using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame.Entityes
{
    class Player : Allive
    {
        public PlayerDataStorage DataStorage { get; set; }
        public Player(string name, char renderChar, Cell cLink, PlayerDataStorage pds, ConsoleColor color = ConsoleColor.White, EntityType type = EntityType.Neutral, int health = 100) : base(name, renderChar, cLink, color, type, health)
        {
            DataStorage = pds;
            Health = pds.Health;
            DataStorage.DirChange += (a,b)=> { Move();};
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
            UIFieldScreen.RenderField();
        }
        public override void SetHealth(int ammount)
        {
            base.SetHealth(ammount);
            DataStorage.Health = ammount;
        }
        public override void AddHealth(int ammount)
        {
            base.AddHealth(ammount);
            DataStorage.Health += ammount;
        }
        public override void TakeDamage(int ammount)
        {
            base.TakeDamage(ammount);
            DataStorage.Health = Health;
            DataStorage.IsDeath = IsDeath;
        }
    }
}
