using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame
{
    class Field
    {
        public Game GameLink { get; set; }
        private Cell[,] Cells { get; set; }
        public int SizeX { get { return Cells.GetLength(0); } }
        public int SizeY { get { return Cells.GetLength(1); } }
        public Field(Game gameLink, int x = 10,int y = 10)
        {
            GameLink = gameLink;
            Cells = new Cell[x, y];
            Fill();
        }
        private void Fill()
        {
            for (int i = 0; i < SizeY; i++)
            {
                for (int k = 0; k < SizeX; k++)
                {
                    Cells[k, i] = new Cell(k,i,this);
                }
            }
        }
        public Cell GetCell(int x,int y)
        {
            if((x<0||x>=SizeX) ||(y<0||y>= SizeY))
            {
                return null;
            }    
            return Cells[x, y];
        }
        public Cell GetCell(Vector2 pos)
        {
            if ((pos.X < 0 || pos.X >= SizeX) || (pos.Y < 0 || pos.Y >= SizeY))
            {
                return null;
            }
            return Cells[(int)pos.X, (int)pos.Y];
        }
        public void Show()
        {
            StringBuilder spliter = new StringBuilder();
            for (int f = 0; f < 4 * SizeX + 1; f++)
            {
                spliter.Append('-');
            }
            for (int i = 0; i < SizeY; i++)
            {
                Console.Write(spliter);
                Console.WriteLine("");
                for (int k = 0; k < SizeX; k++)
                {
                    Console.Write($"| ");
                    Console.ForegroundColor = Cells[k, i].EntityHolder == null ? ConsoleColor.White : Cells[k, i].EntityHolder.Color;
                    Console.Write($"{(Cells[k, i].EntityHolder != null ? Cells[k, i].EntityHolder.RenderChar : '-')} ");
                    Console.ResetColor();
                }
                Console.Write($"|\n");
            }
            Console.WriteLine(spliter);
        }
        public bool CanSpawn(int x, int y)
        {
            if(GetCell(x, y).EntityHolder != null) return false;
            return true;
        }
        public bool CanSpawn(Cell cell)
        {
            if (cell.EntityHolder != null) return false;
            return true;
        }
        public void SpawnEntity(Entity ent)
        {
            Random rnd = new Random();
            Cell cell;
            int counter = 0;
            do
            {
                cell = GetCell(rnd.Next(0, SizeX), rnd.Next(0, SizeY));
                if (CanSpawn(cell))
                {
                    cell.EntityHolder = ent;
                    ent.FieldLink = this;
                    ent.CellLink = cell;
                    Console.Beep(2000,10);
                    break;
                }
                counter++;
            }
            while (counter < 100);
            if (counter >= 99) // Debug if
            {
                throw new Exception("Field cant spanw entity");
            }
        }
    }
}
