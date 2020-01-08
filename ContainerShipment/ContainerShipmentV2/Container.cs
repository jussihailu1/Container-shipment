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

        public string ToString(StringValue containerStringValue)
        {
            switch (containerStringValue)
            {
                case StringValue.ContainerType:
                    switch (ContainerType)
                    {
                        case ContainerType.Cooled:
                            return "[C]";
                        case ContainerType.Normal:
                            return "[N]";
                        case ContainerType.Valuable:
                            return "[V]";
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case StringValue.Weight:
                    return Weight.ToString();
                case StringValue.CurrentWeightAboveBottomContainer:
                    return WeightAbove.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(containerStringValue), containerStringValue, null);
            }

            //if (a == 0)
            //    switch (ContainerType)
            //    {
            //        case ContainerType.Cooled:
            //            return $"[C] {Weight}";
            //        case ContainerType.Normal:
            //            return $"[N] {Weight}";
            //        case ContainerType.Valuable:
            //            return $"[V] {Weight}";
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }
            //return a == 1 ? $"{this.Weight}" : $"{this.WeightAbove}";
        }

    }

    public enum StringValue
    {
        Weight,
        ContainerType,
        CurrentWeightAboveBottomContainer
    }

    public enum ContainerType
    {
        Cooled,
        Normal,
        Valuable
    }
}
