using SmileGame.Entityes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmileGame
{
    class Game
    {
        public static event EventHandler Update;
        public void StartGame()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PlayerDataStorage pds = new PlayerDataStorage(Allive.defaultHealth);
            Update += (a, b) => { pds.Addscore(); };
            Stats<PlayerDataStorage> mgs = new MainGameStats(pds);
            UIFieldScreen.stats = mgs;
            List<Entity> entityList = new List<Entity>();
            entityList.Add(new Allive("Lol", '1', null));
            entityList.Add(new Allive("Lol", '2', null));
            entityList.Add(new Allive("Lol", '3', null));
            entityList.Add(new Allive("Lol", '4', null));
            entityList.Add(new Player("Lol", '℗', null, pds, ConsoleColor.Red));
            Field f = new Field();
            UIFieldScreen._Field = f;
            foreach (var item in entityList)
            {
                f.SpawnEntity(item);
            }
            f.Show();
            mgs.Show();
            Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(1000).GetAwaiter().GetResult();

                    foreach (var item in entityList)
                    {
                        if (!(item is Player))
                        {
                            (item as Allive)?.Move();
                        }
                    }
                    Update?.Invoke(new object(), new EventArgs());
                    UIFieldScreen.RenderField();

                }

            }).GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
