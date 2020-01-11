﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class Ship
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int Middle { get; set; }///
        public int WeightLeftSide { get; set; }
        public int WeightRightSide { get; set; }
        public List<Stack> Stacks { get; set; }
        public List<Container> PlacedContainers => Stacks.SelectMany(s => s.Containers).ToList();

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            Middle = CalcMiddle();
            WeightLeftSide = 0;
            WeightRightSide = 0;
            Stacks = new List<Stack>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    Stacks.Add(new Stack(x, y));
                }
            }
        }

        private int CalcMiddle() => decimal.ToInt32(decimal.Divide(Width, 2)) + 1;

        public bool PlaceNormalAndValuableContainer(Container container)
        {
            for (int y = 0; y < Length; y++)
            {
                if (WeightLeftSide < WeightRightSide)
                {
                    for (int x = Middle - 1; x >= 0; x--)
                    {
                        if (!Place(container, x, y)) continue;
                        WeightLeftSide += container.Weight;
                        return true;
                    }
                }
                else
                {
                    for (int x = Middle - 1; x < Width; x++)
                    {
                        if (!Place(container, x, y)) continue;
                        WeightRightSide += container.Weight;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Place(Container container, int x, int y)
        {
            var stack = Stacks.Find(s => s.X == x && s.Y == y);
            if (Stacks.Any(s => s.X == x && s.Y != y && s.HeighestContainerZ < stack.HeighestContainerZ)) return false;
            if (!stack.ContainerCanBeAdded(this, container)) return false;
            stack.AddContainer(container);
            return true;
        }

        public bool PlaceCooledContainer(Container container)
        {
            //y = 0 want gekoelde containers moeten voor aan de boot.
            //30 want minimale container gewicht = 4 ton en Maximale gewicht boven op een container is 120 ton dus 120 / 4 = 30.

            const int y = 0;

            for (int z = 0; z < 30; z++)
            {
                if (WeightLeftSide > WeightRightSide)
                {
                    if (PlaceRightSide(container, y))
                    {
                        WeightRightSide += container.Weight;
                        return true;
                    }
                }
                else
                {
                    if (PlaceLeftSide(container, y))
                    {
                        WeightLeftSide += container.Weight;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PlaceNormalContainer(Container container)
        {
            for (int z = 0; z < 30; z++)
            {
                for (int y = 0; y < Length; y++)
                {
                    if (WeightLeftSide > WeightRightSide)
                    {
                        if (PlaceRightSide(container, y))
                        {
                            WeightRightSide += container.Weight;
                            return true;
                        }
                    }
                    else
                    {
                        if (PlaceLeftSide(container, y))
                        {
                            WeightLeftSide += container.Weight;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool PlaceValuableContainer(Container container)
        {
            for (int z = 0; z < 30; z++)
            {
                for (int y = 0; y < Length; y++)
                {
                    if (WeightLeftSide > WeightRightSide)
                    {
                        if (PlaceRightSide(container, y))
                        {
                            WeightRightSide += container.Weight;
                            return true;
                        }
                    }
                    else
                    {
                        if (PlaceLeftSide(container, y))
                        {
                            WeightLeftSide += container.Weight;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool PlaceRightSide(Container container, int y)
        {
            for (int x = Middle - 1; x < Width; x++)
            {
                var stack = Stacks.Find(s => s.X == x && s.Y == y);
                if (!stack.ContainerCanBeAdded(this, container)) continue;
                stack.AddContainer(container);
                PlacedContainers.Add(container);
                return true;
            }

            return false;
        }

        private bool PlaceLeftSide(Container container, int y)
        {
            for (int x = Middle - 1; x >= 0; x--)
            {
                var stack = Stacks.Find(s => s.X == x && s.Y == y);
                if (!stack.ContainerCanBeAdded(this, container)) continue;
                PlacedContainers.Add(container);
                stack.AddContainer(container);
                return true;
            }

            return false;
        }
    }
}
