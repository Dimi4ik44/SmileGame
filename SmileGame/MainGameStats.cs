using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame
{
    class MainGameStats : Stats<PlayerDataStorage>
    {
        public MainGameStats(PlayerDataStorage data) : base(data)
        {
        }

        public override void Show()
        {
            Console.WriteLine($"Cur direction: {Data.Dir} - Cur Health: {Data.Health} - Cur SCORE: {Data.Score}");
        }
    }
}
