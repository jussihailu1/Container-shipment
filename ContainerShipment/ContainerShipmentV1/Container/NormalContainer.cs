using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Container
{
    public class NormalContainer : IContainer
    {
        public int Weight { get; set; }
        public VectorPoint VectorPoint { get; set; }

        public NormalContainer(int weight)
        {
            Weight = weight;
        }
    }
}
