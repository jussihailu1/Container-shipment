using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class Stack
    {
        public int X { get; set; }
        public int Y { get; set; }
        public StackPosition StackPosition { get; set; }
        public List<Container> Containers { get; set; }

        public Stack(int x, int y, StackPosition stackPosition)
        {
            X = x;
            Y = y;
            StackPosition = stackPosition;
            Containers = new List<Container>();
        }

        public bool WeightExceeded(int weight) => Containers.Where(c => Containers.IndexOf(c) != 0).Sum(c => c.Weight) + weight > 120;

        public bool ContainerCanBeAdded(Container container)
        {
            if (container.ContainerType == ContainerType.Valuable && this.StackPosition == StackPosition.Back || this.StackPosition == StackPosition.Front) return false;
            if (container.ContainerType == ContainerType.Cooled && this.StackPosition != StackPosition.Front) return false;
        }

        public void AddContainer(Container container)
        {
            // TODO: Checks aren't done yet 
            foreach (var c in Containers)
            {
                c.AddWeightAbove(container.Weight);
            }
            Containers.Add(container);
        }
    }

    public enum StackPosition
    {
        Front,
        Back,
        Center
    }
}
