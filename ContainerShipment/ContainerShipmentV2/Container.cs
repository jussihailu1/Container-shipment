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

        public string ToString(int a)
        {
            if (a == 1)
                switch (ContainerType)
                {
                    case ContainerType.Cooled:
                        return $"[C] {this.Weight}";
                    case ContainerType.Normal:
                        return $"[N] {this.Weight}";
                    case ContainerType.Valuable:
                        return $"[V] {this.Weight}";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            return a == 2 ? $"{this.Weight}" : $"{this.WeightAbove}";
        }
    }

    public enum ContainerType
    {
        Cooled,
        Normal,
        Valuable
    }
}
