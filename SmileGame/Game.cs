using SmileGame.Entityes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmileGame
{
    class Game
    {
        public static event EventHandler Update;
        public GameLog gl = new GameLog();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public static CancellationToken ct;
        public void StartGame()
        {
            ct = tokenSource.Token;
            ConsoleShutDownManager.addAction((a)=> {
                tokenSource.Cancel();
                gl.SaveData();
                return true; });
            Console.OutputEncoding = Encoding.UTF8;
            PlayerDataStorage pds = new PlayerDataStorage(Allive.defaultHealth);
            Update += (a, b) => { pds.Addscore(); };
            Stats<PlayerDataStorage> mgs = new MainGameStats(pds);
            UIFieldScreen.stats = mgs;
            UIFieldScreen.logger = gl;
            List<Entity> entityList = new List<Entity>();
            entityList.Add(new Allive("Lol", '1', null));
            entityList.Add(new Allive("Lol", '2', null));
            entityList.Add(new Allive("Lol", '3', null));
            entityList.Add(new Allive("Lol", '4', null));
            entityList.Add(new Allive("Lol", '5', null));
            entityList.Add(new Allive("Lol", '6', null));
            entityList.Add(new Allive("Lol", '7', null));
            entityList.Add(new Allive("Lol", '8', null));
            entityList.Add(new Allive("Lol", '9', null));
            entityList.Add(new Allive("Lol", '0', null));
            entityList.Add(new Player("Lol", '℗', null, pds, ConsoleColor.Red));
            Field f = new Field(this);
            UIFieldScreen._Field = f;
            foreach (var item in entityList)
            {
                item.SpawnOnField(f);
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
                    Update?.Invoke(this, new EventArgs());
                    UIFieldScreen.RenderField();

                }

            },ct).GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
