using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTimeTracker
{
    public  class EventArgsGen<T> : EventArgs
    {
        public EventArgsGen(T value)
        {
            Value = value;
        }
        public T Value { get;  }
    }
}
