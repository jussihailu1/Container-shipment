using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public bool ContainerCanBeAdded(Ship ship, Container container)
        {
            if (WeightExceeded(container.Weight) || IsTopContainerValuable()) return false;
            return container.ContainerType != ContainerType.Valuable || ValuableIsAllowed(ship);
        }

        private bool WeightExceeded(int weight) => Containers.Where(c => Containers.IndexOf(c) != 0).Sum(c => c.Weight) + weight > 120;

        private bool ValuableIsAllowed(Ship ship)
        {
            var stackInFront = ship.Stacks.ToList().Find(s => s.X == X && s.Y == Y - 1);

            if (stackInFront != null)
            {
                if (stackInFront.HeighestContainerZ > HeighestContainerZ) return false;
            }

            var stackBehind = ship.Stacks.ToList().Find(s => s.X == X && s.Y == Y + 1);
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
            container.Index = Indexer.I++;

            Containers.ForEach(c => c.AddWeightAbove(container.Weight));
            Containers.Add(container);
        }
    }
}
