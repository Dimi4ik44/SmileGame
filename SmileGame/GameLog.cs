using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SmileGame
{
    class GameLog
    {
        public List<string> messages = new List<string>();
        public void LogAction(string mes)
        {
            messages.Add(mes);
        }
        public void ShowLog()
        {
            foreach (var item in messages.ToArray())
            {
                Console.WriteLine(item);
            }
        }
        public void SaveData()
        {
            using (StreamWriter sw = new StreamWriter($"J:/LogData_{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year} {DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.txt"))
            {
                foreach (string data in messages)
                {
                    sw.WriteLine(data);
                }
            }           
        }
    }
}
