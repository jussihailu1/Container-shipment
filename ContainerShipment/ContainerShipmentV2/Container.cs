using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV2
{
    public class Container
    {
        public int Weight { get; }
        public int WeightAbove { get; private set; }
        public ContainerType ContainerType { get; }
        public int Index { get; set; }

        public Container(int weight, ContainerType containerType)
        {
            Weight = weight;
            WeightAbove = 0;
            ContainerType = containerType;
        }

        public void AddWeightAbove(int weight)
        {
            WeightAbove += weight;
        }
    }

    public enum ContainerType
    {
        Cooled,
        Normal,
        Valuable
    }
}
