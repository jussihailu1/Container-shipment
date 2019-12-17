using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace ContainerShipmentV1
{
    public class Ship
    {
        //public int MaxWeight { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public List<IContainer> Containers { get; set; }

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
        }

        public VectorPoint SearchPossibleLocations(IContainer container)
        {
            
        }

        public bool CheckWeight(IContainer containerToAdd, List<IContainer> containerStack)
        {
            containerStack.OrderByDescending(c => c.VectorPoint.Z);

            var totalWeight = containerStack.Where(container => containerStack.IndexOf(container) != 0).Sum(container => container.Weight);

            return totalWeight + containerToAdd.Weight > 120;
        }
    }
}
