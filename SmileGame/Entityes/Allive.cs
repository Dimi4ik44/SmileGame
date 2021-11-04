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
        public bool IsDeath { get; set; }
        public const int defaultHealth = 10;
        public Allive(string name, char renderChar, Cell cLink, ConsoleColor color = ConsoleColor.White, EntityType type = EntityType.Neutral, int health = defaultHealth) : base(name, renderChar, cLink, color, type)
        {
            Health = health;
        }

        public bool IsPlayer { get { return this is Player; } }
        public virtual void Move()
        {
            if(!IsDeath)
            {
                Random rnd = new Random();
                Cell cell = null;
                Dir = (Direction)rnd.Next(0, 4);
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
                if (cell != null && cell?.EntityHolder == null)
                {
                    cell.EntityHolder = this;
                    CellLink.EntityHolder = null;
                    CellLink = cell;
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
            
        }
        public virtual void Attack(Allive enemy)
        {
            enemy.TakeDamage(1);
        }
        public virtual void SetHealth(int ammount)
        {
            if (IsDeath) return;
            Health = ammount;
        }
        public virtual void AddHealth(int ammount)
        {
            if (IsDeath) return;
            Health += ammount;
        }
        public virtual void TakeDamage(int ammount)
        {
            if (IsDeath) return;
            Health -= ammount;
            if (Health <= 0)
            {
                Health = 0;
                IsDeath = true;
                CellLink.EntityHolder = null;
            }           
        }
    }
}
