using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1
{
    public abstract class Container
    {
        public int Weight { get; }
        public VectorPoint VectorPoint { get; set; }
        public int MaxWeightAbove { get; }

        protected Container(int weight)
        {
            Weight = weight;
            MaxWeightAbove = 120;
        }

        public override string ToString()
        {
            return $"X{VectorPoint.X} Y{VectorPoint.Y} Z{VectorPoint.Z}";
        }
    }
}
