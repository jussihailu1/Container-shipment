using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class Ship
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int Middle { get; set; }
        public int WeightLeftSide { get; set; }
        public int WeightRightSide { get; set; }
        public List<Stack> Stacks { get; set; }
        public List<Container> PlacedContainers { get; set; }
        public List<Container> NotPlacedContainers { get; set; }

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            Middle = CalcMiddle();
            WeightLeftSide = 0;
            WeightRightSide = 0;
            Stacks = new List<Stack>();
            PlacedContainers = new List<Container>();
            NotPlacedContainers = new List<Container>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    Stacks.Add(new Stack(x, y));
                }
            }
        }

        private int CalcMiddle() => decimal.ToInt32(decimal.Divide(Width, 2)) + 1;

        public void PlaceCooledContainer(Container container)
        {
            const int y = 0;

            //y = 0 want gekoelde containers moeten voor aan de boot.
            //30 want minimale container gewicht = 4 ton en Maximale gewicht boven op een container is 120 ton dus 120 / 4 = 30.

            for (int z = 0; z < 30; z++)
            {
                if (WeightLeftSide > WeightRightSide)
                {
                    PlaceRightSide(container, y);
                    WeightRightSide += container.Weight;
                    return;
                }

                PlaceLeftSide(container, y);
                WeightLeftSide += container.Weight;
                return;
            }
        }

        public void PlaceNormalContainer(Container container)
        {
            for (int z = 0; z < 30; z++)
            {
                for (int y = 0; y < Length; y++)
                {
                    if (WeightLeftSide > WeightRightSide)
                    {
                        PlaceRightSide(container, y);
                        WeightRightSide += container.Weight;
                        return;
                    }

                    PlaceLeftSide(container, y);
                    WeightRightSide += container.Weight;
                    return;
                }
            }
        }

        public void PlaceValuableContainer(Container container)
        {

        }

        private void PlaceRightSide(Container container, int y)
        {
            for (int x = Middle - 1; x < Width; x++)
            {
                var stack = Stacks.Find(s => s.X == x && s.Y == y);
                if (stack.ContainerCanBeAdded(this, container)) continue;
                stack.AddContainer(container);
                PlacedContainers.Add(container);
                return;
            }

            NotPlacedContainers.Add(container);
        }

        private void PlaceLeftSide(Container container, int y)
        {
            for (int x = 0; x < Middle; x++)
            {
                var stack = Stacks.Find(s => s.X == x && s.Y == y);
                if (!stack.ContainerCanBeAdded(this, container)) continue;
                PlacedContainers.Add(container);
                stack.AddContainer(container);
                return;
            }

            NotPlacedContainers.Add(container);
        }
    }
}
