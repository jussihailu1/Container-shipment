using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            if (WeightExceeded(container.Weight) || IsTopContainerValuable()) return false;
            if (container.ContainerType == ContainerType.Valuable && !ValuableIsAllowed(ship)) return false;

            return true;
        }

        private bool ValuableIsAllowed(Ship ship)
        {
            var stackInFront = ship.Stacks.Find(s => s.X == X && s.Y == Y - 1);
            if (stackInFront != null)
            {
                if (stackInFront.HeighestContainerZ > HeighestContainerZ) return false;
            }

            var stackBehind = ship.Stacks.Find(s => s.X == X && s.Y == Y + 1);
            if (stackBehind != null)
            {
                if (stackBehind.HeighestContainerZ > HeighestContainerZ) return false;
            }
            return true;
        }

        private bool IsTopContainerValuable()
        {
            return Containers.Count > 0 && Containers.Last().ContainerType == ContainerType.Valuable;
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
