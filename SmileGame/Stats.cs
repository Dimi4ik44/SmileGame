using System;
using System.Collections.Generic;
using System.Text;

namespace SmileGame
{
    class Stats<T>
    {
        public T Data { get; set; }
        public Stats(T data)
        {
            Data = data;
        }
        public virtual void Show()
        {
            
        }
    }
}
