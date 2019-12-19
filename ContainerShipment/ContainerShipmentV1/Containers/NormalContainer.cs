using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Containers
{
    public class NormalContainer : Container
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public NormalContainer(int weight) : base(weight)
        {
            Weight = weight;
        }
    }
}
