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
        public List<Container> Containers { get; set; }
        public int HeighestContainerZ => Containers.Count - 1;

        public Stack(int x, int y)
        {
            X = x;
            Y = y;
            Containers = new List<Container>();
        }

        public bool WeightExceeded(int weight) => Containers.Where(c => Containers.IndexOf(c) != 0).Sum(c => c.Weight) + weight > 120;

        public bool ContainerCanBeAdded(Ship ship, Container container)
        {
            if (WeightExceeded(container.Weight)) return false;
            if (HeighestContainerZ > 0)
            {
                if (Containers[HeighestContainerZ].ContainerType == ContainerType.Valuable) return false;
            }
            //TODO: Bovenstaande dubbele if zou in een methode kunnen.
            if (container.ContainerType == ContainerType.Cooled && Y != 0) return false;
            var z = HeighestContainerZ;
            if (container.ContainerType == ContainerType.Valuable && !ValuableIsAllowed(ship, z)) return false;

            return true;
        }

        private bool ValuableIsAllowed(Ship ship, int z)
        {
            if (ship.Stacks.Any(s => s.X == X && s.Y == - 1 && s.Containers.Count - 1 >= z)) return false;
            if (ship.Stacks.Any(s => s.X == X && s.Y == + 1 && s.Containers.Count - 1 >= z)) return false;
            return true;
        }

        public void AddContainer(Container container)
        {
            //TODO: Hiernaar kijken want er hoeft eigenlijk alleen maar gekeken naar de onderste container. Dus misschien Alleen de onderste container c.AddWeightAbove(container.Weight) en daarna gewoon de container toevoegen aan stack.
            foreach (var c in Containers)
            {
                c.AddWeightAbove(container.Weight);
            }
            Containers.Add(container);
        }
    }
}
