using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV2
{
    public class Container
    {
        public int Weight { get; set; }
        public int WeightAbove { get; set; }
        public ContainerType ContainerType { get; }

        public Container(int weight, ContainerType containerType)
        {
            Weight = weight;
            WeightAbove = 0;
            ContainerType = containerType;
        }

        public void AddWeightAbove(int weight)
        {
            this.WeightAbove += weight;
        }
    }

    public enum ContainerType
    {
        Cooled,
        Normal,
        Valuable
    }
}
