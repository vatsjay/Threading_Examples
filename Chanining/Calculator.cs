using System;
using System.Collections.Generic;
using System.Text;

namespace Chanining
{
    public class Calculator
    {
        private int _init;
        public Calculator(int init)
        {
            _init = init;
        }

        public Calculator Add(int x)
        {
            _init = _init + x;
            return this;
        }

        public Calculator Sub(int x)
        {
            _init = _init - x;
            return this;
        }

        public int Result()
        {
            return _init;
        }
    }
}
