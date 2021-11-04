using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmileGame
{
    class Cell
    {
        public Field FieldLink { get; set; }
        public Entity EntityHolder { get; set; }
        public Vector2 Pos { get; set; }
        public Cell(int x,int y)
        {
            Pos = new Vector2(x,y);
        }
        public Cell(int x, int y, Field FLink) : this(x, y)
        {
            FieldLink = FLink;
        }
    }
}
 