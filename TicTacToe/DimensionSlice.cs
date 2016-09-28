using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class YXZ
    {
        byte y;
        byte x;
        byte z;

        public byte this[char dimension]
        {
            get
            {
                if (Char.ToUpper(dimension) == 'Y')
                    return y;
                if (Char.ToUpper(dimension) == 'X')
                    return X;
                if (Char.ToUpper(dimension) == 'Z')
                    return Z;

                throw new System.ArgumentOutOfRangeException(dimension.ToString(), "Must be X, Y, or Z");
            }
        }

        public byte Y
        {
            get
            {
                return y;
            }
        }

        public byte X
        {
            get
            {
                return x;
            }
        }

        public byte Z
        {
            get
            {
                return z;
            }
        }

        public YXZ(byte _y, byte _x, byte _z)
        {
            y = _y;
            x = _x;
            z = _z;
        }
    }

    public enum SliceBounds
    {
        Middle = 1,
        Upper = 2,
        Lower = 0
    }
}
