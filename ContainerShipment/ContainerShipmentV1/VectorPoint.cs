using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1
{
    public class VectorPoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public VectorPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
