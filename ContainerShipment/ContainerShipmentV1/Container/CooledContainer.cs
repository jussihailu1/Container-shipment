using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Container
{
    public class CooledContainer : IContainer
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public CooledContainer(int weight)
        {
            Weight = weight;
        }
    }
}
