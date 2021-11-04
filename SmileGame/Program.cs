using SmileGame.Entityes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmileGame
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerDataStorage pds = new PlayerDataStorage();
            List<Entity> entityList = new List<Entity>();
            entityList.Add(new Allive("Lol",'1',null));
            entityList.Add(new Allive("Lol", '2', null));
            entityList.Add(new Allive("Lol", '3', null));
            entityList.Add(new Allive("Lol", '4', null));
            entityList.Add(new Player("Lol", '=', null, pds));
            Field f = new Field();
            foreach (var item in entityList)
            {
                f.SpawnEntity(item);
            }
            f.Show();
            Task.Run(() =>
            {
                while (true)
                {                   
                    Task.Delay(1000).GetAwaiter().GetResult();
                    foreach (var item in entityList)
                    {
                        (item as Allive)?.Move();
                    }
                    Console.Clear();
                    f.Show();
                }

            }).GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
