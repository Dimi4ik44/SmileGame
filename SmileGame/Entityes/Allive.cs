using SmileGame.Entityes;
using SmileGame.Entityes.Items;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame
{
    class Allive : Entity, IMovable
    {
        public event EventHandler<CustomEventArgs> onMove;
        public event EventHandler<CustomEventArgs> onAttack;
        public event EventHandler<CustomEventArgs> onTakeDamage;
        public event EventHandler<CustomEventArgs> onDeath;
        public event EventHandler<CustomEventArgs> onUse;
        public Direction Dir { get; set; }
        public int Health { get; set; }
        public bool IsDeath { get; set; }
        public const int defaultHealth = 10;
        public Allive(string name, char renderChar, Cell cLink, ConsoleColor color = ConsoleColor.White, EntityType type = EntityType.Neutral, int health = defaultHealth) : base(name, renderChar, cLink, color, type)
        {
            Health = health;
        }

        public bool IsPlayer { get { return this is Player; } }
        public override void SpawnOnField(Field field)
        {
            base.SpawnOnField(field);
            onMove += FieldLink.GameLink.gl.LogAction;
            onAttack += FieldLink.GameLink.gl.LogAction;
            onDeath += FieldLink.GameLink.gl.LogAction;
            onTakeDamage += FieldLink.GameLink.gl.LogAction;
            onUse += FieldLink.GameLink.gl.LogAction;
        }
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
                    if (item != null)
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
            
        }
        public virtual void Attack(Allive enemy)
        {
            Console.Beep(1200, 10);
            int damage = 1;
            InvokeOnAttackEvent($"(ATTACK EVENT)Allive entity| Name:{Name}, Render char:{RenderChar}, Move Dir:{Dir}, Health:{Health}, IsDeath:{IsDeath}, Is player:{IsPlayer}, pos{CellLink.Pos} | Enemy name:{enemy.Name}, damage:{damage}, renderChar:{enemy.RenderChar}, isDeath:{enemy.IsDeath}, health:{enemy.Health}, isPlayer:{enemy.IsPlayer}, pos{enemy.CellLink.Pos}");
            enemy.TakeDamage(damage);
            
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
                InvokeOnDeathEvent($"(DEATH EVENT) Allive entity| Name:{Name}, renderChar:{RenderChar}, Died!");
                return;
            }
            InvokeOnTakeDamageEvent($"(TAKE DAMAGE EVENT) Allive entity| Name:{Name}, Render char:{RenderChar}, Move Dir:{Dir}, Health:{Health}, IsDeath:{IsDeath}, Is player:{IsPlayer}, pos{CellLink.Pos} | Damage taken:{ammount}");
        }
        public void InvokeOnMoveEvent(string mess)
        {
            onMove?.Invoke(this,new CustomEventArgs() { Message = mess});
        }
        public void InvokeOnAttackEvent(string mess)
        {
            onAttack?.Invoke(this, new CustomEventArgs() { Message = mess });
        }
        public void InvokeOnTakeDamageEvent(string mess)
        {
            onTakeDamage?.Invoke(this, new CustomEventArgs() { Message = mess });
        }
        public void InvokeOnDeathEvent(string mess)
        {
            Console.Beep(4200, 100);
            onDeath?.Invoke(this, new CustomEventArgs() { Message = mess });
        }
        public void InvokeOnUseEvent(string mess)
        {
            Console.Beep(3200, 10);
            onUse?.Invoke(this, new CustomEventArgs() { Message = mess });
        }
    }
}
