using SmileGame.Entityes.Items;
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
            if(!IsDeath)
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
                if (cell != null && (cell?.EntityHolder == null || cell?.EntityHolder is BaseItem))
                {
                    BaseItem item = null;
                    if (cell?.EntityHolder is BaseItem)
                    {
                        item = cell.EntityHolder as BaseItem;
                    }
                    cell.EntityHolder = this;
                    CellLink.EntityHolder = null;
                    Cell tempOldPos = CellLink;
                    CellLink = cell;
                    InvokeOnMoveEvent($"(MOVE EVENT) Allive entity| Name:{Name}, Render char:{RenderChar}, Move Dir:{Dir}, Health:{Health}, IsDeath:{IsDeath}, Is player:{IsPlayer} | Move to pos:{CellLink.Pos}, Old pos:{tempOldPos.Pos}");
                    if (item!=null)
                    {
                        item.Use(this);
                        InvokeOnUseEvent($"(USE EVENT) Allive entity| Name:{Name}, Render char:{RenderChar}, Move Dir:{Dir}, Health:{Health}, IsDeath:{IsDeath}, Is player:{IsPlayer} | Use:{item.Name}, Desc:{item.Description}");
                    }
                }
                else if (cell?.EntityHolder != null)
                {
                    if (cell.EntityHolder is Allive)
                    {
                        switch (cell.EntityHolder.Type)
                        {
                            case EntityType.Enemy:
                                break;
                            case EntityType.Friendly:
                                break;
                            case EntityType.Neutral:
                                Attack(cell.EntityHolder as Allive);
                                break;
                            default:
                                break;
                        }
                    }
                }
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
