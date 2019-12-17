using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Container
{
    public class ValuableContainer : IContainer
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public ValuableContainer(int weight)
        {
            Weight = weight;
        }
    }
}
