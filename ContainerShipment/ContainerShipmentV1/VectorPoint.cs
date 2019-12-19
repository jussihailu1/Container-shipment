using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1
{
    public class VectorPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public VectorPoint(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
