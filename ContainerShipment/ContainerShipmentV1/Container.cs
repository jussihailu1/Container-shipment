using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1
{
    public abstract class Container
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        protected Container(int weight)
        {
            Weight = weight;
        }

        public override string ToString()
        {
            return $"X{VectorPoint.X} Y{VectorPoint.Y} Z{VectorPoint.Z}";
        }
    }
}
