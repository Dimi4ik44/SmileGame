using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmileGame
{
    class PlayerDataStorage
    {
        public int Health { get; set; }
        public List<Valute> ValuteList { get; set; } //list of valutes
        public Direction Dir { get; set; }
        public Valute GetValuteByName(string nameOfValute)
        {
            foreach (var item in ValuteList)
            {
                if (item.Name == nameOfValute) return item;
            }
            return null;
        }
        public PlayerDataStorage()
        {
            Task.Run(GetInputDirection);
        }
        public void GetInputDirection()
        {
            while(true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        Dir = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        Dir = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        Dir = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        Dir = Direction.Right;
                        break;
                }
            }
        }
    }
}
