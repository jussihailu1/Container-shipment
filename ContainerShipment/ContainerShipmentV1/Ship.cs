using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace ContainerShipmentV1
{
    public class Ship
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public List<Container> Containers { get; set; }

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            Containers = new List<Container>();
        }

        //public VectorPoint SearchPossibleLocations(IContainer container)
        //{
            
        //}

        public bool CheckWeight(Container containerToAdd, List<Container> containerStack)
        {
            containerStack.OrderByDescending(c => c.VectorPoint.Z);

            var totalWeight = containerStack.Where(container => containerStack.IndexOf(container) != 0).Sum(container => container.Weight);

            return totalWeight + containerToAdd.Weight > 120;
        }
    }
}
