using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class Ship
    {

        public int Width { get; private set; }
        public int Length { get; private set; }
        private int Middle { get; }
        public int WeightLeftSide { get; private set; }
        public int WeightRightSide { get; private set; }
        private readonly List<Stack> _stacks;
        public IEnumerable<Stack> Stacks => _stacks.AsReadOnly();
        public IEnumerable<Container> PlacedContainers => Stacks.SelectMany(s => s.Containers);


        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            Middle = CalcMiddle();
            WeightLeftSide = 0;
            WeightRightSide = 0;
            _stacks = new List<Stack>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    _stacks.Add(new Stack(x, y));
                }
            }
        }

        private int CalcMiddle() => decimal.ToInt32(decimal.Divide(Width, 2)) + 1;

        public bool Place(Container container)
        {
            if (WeightLeftSide < WeightRightSide)
            {
                for (int y = 0; y < Length; y++)
                {
                    for (int x = Middle - 1; x >= 0; x--)
                    {
                        var stack = Stacks.Find(s => s.X == x && s.Y == y);
                        if (Stacks.Any(s => s.X == x && s.Y != y && s.HeighestContainerZ < stack.HeighestContainerZ)) continue;
                        if (!stack.ContainerCanBeAdded(this, container)) continue;
                        WeightLeftSide += container.Weight;
                        stack.AddContainer(container);
                        return true;
                    }
                }
            }
            else
            {
                for (int y = 0; y < Length; y++)
                {
                    for (int x = Middle - 1; x < Width; x++)
                    {
                        var stack = Stacks.Find(s => s.X == x && s.Y == y);
                        if (Stacks.Any(s => s.X == x && s.Y != y && s.HeighestContainerZ < stack.HeighestContainerZ)) continue;
                        if (!stack.ContainerCanBeAdded(this, container)) continue;
                        WeightRightSide += container.Weight;
                        stack.AddContainer(container);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PlaceCooledContainer(Container container)
        {
            //y = 0 want gekoelde containers moeten voor aan de boot.
            // 30 in the foreach below is the possibility of the maximum amount of containers in one stack (30 containers of each 4 tons => 120 ton (maximum weight))


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
            //for (int z = 0; z < 30; z++)
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
            //for (int z = 0; z < 30; z++)
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
                var stack = _stacks.Find(s => s.X == x && s.Y == y);
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
                var stack = _stacks.Find(s => s.X == x && s.Y == y);
                if (!stack.ContainerCanBeAdded(this, container)) continue;
                PlacedContainers.Add(container);
                stack.AddContainer(container);
                return true;
            }

            return false;
        }
    }
}
