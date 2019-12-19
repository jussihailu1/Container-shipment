using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Containers
{
    public class CooledContainer : Container
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public CooledContainer(int weight) : base(weight)
        {
            Weight = weight;
        }
    }
}
