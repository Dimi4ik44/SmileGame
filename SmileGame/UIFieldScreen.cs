using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame
{
    static class UIFieldScreen
    {
        static public object lockObj = new object();
        static public Stats<PlayerDataStorage> stats { get; set; }
        static public Field _Field { get; set; }
        public static void RenderField()
        {
            lock (lockObj)
            {
                Console.Beep(300, 100);
                Console.Clear();
                _Field.Show();
                stats.Show();
            }
        }
    }
}
