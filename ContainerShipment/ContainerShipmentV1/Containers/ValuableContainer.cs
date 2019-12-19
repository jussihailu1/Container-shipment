using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Containers
{
    public class ValuableContainer : Container
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public ValuableContainer(int weight) : base(weight)
        {
            Weight = weight;
        }
    }
}
