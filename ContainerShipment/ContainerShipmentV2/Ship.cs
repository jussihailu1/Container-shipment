﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace ContainerShipmentV2
{
    public class Ship
    {
        private readonly List<Stack> _stacks;
        public IEnumerable<Stack> Stacks => _stacks.AsReadOnly();
        public int MaxWeight => Width * Length * 150;
        private int Middle => decimal.ToInt32(decimal.Divide(Width, 2));
        public int Width { get; }
        public int Length { get; }
        public int WeightLeftSide => CalcWeightLeftSide();
        public int WeightRightSide => CalcWeightRightSide();
        private bool Even =>  Width % 2 == 0;
        public IEnumerable<Container> PlacedContainers => Stacks.SelectMany(s => s.Containers);
        public int CurrentTotalWeight => PlacedContainers.Sum(c => c.Weight);
        public bool HalfOfMaxWeightReached => CurrentTotalWeight > MaxWeight / 2;

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            _stacks = new List<Stack>();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _stacks.Add(new Stack(x, y));
                }
            }
        }

        public bool IsShipInBalance()
        {
            var currentTotalWeight = !Even ? WeightLeftSide + WeightRightSide : CurrentTotalWeight;
            var capsizeLimit = 0.2 * currentTotalWeight;
            var difference = WeightLeftSide - WeightRightSide < 0
                ? (WeightLeftSide - WeightRightSide) * -1
                : WeightLeftSide - WeightRightSide;
            return difference <= capsizeLimit;
        }

        private int CalcWeightLeftSide() => _stacks.Where(s => s.X < Middle).SelectMany(s => s.Containers).Sum(c => c.Weight);

        private int CalcWeightRightSide() => _stacks.Where(s => s.X > Middle - Convert.ToInt32(Even)).SelectMany(s => s.Containers).Sum(c => c.Weight);

        public bool FindPlaceForContainer(Container container)
        {
            var containerIsNormal = container.ContainerType == ContainerType.Normal;

            if (container.ContainerType == ContainerType.Cooled)
            {
                const int y = 0;
                if (FindPlace(y))
                {
                    return true;
                }
            }
            else
            {
                for (int y = 0; y < Length; y++)
                {
                    if (FindPlace(y))
                    {
                        return true;
                    }
                }
            }

            bool FindPlace(int y)
            {
                if (WeightLeftSide < WeightRightSide)
                {
                    for (int x = Middle - 1; x >= 0; x--)
                    {
                        var stack = _stacks.Find(s => s.X == x && s.Y == y);
                        if (OtherPlacesAvailable(stack, containerIsNormal, x, y)) continue;
                        if (!PlaceContainer(stack, container)) continue;
                        return true;
                    }
                }
                else
                {
                    for (int x = Middle; x < Width; x++)
                    {
                        var stack = _stacks.Find(s => s.X == x && s.Y == y);
                        if (OtherPlacesAvailable(stack, containerIsNormal, x, y)) continue;
                        if (!PlaceContainer(stack, container)) continue;
                        return true;
                    }
                }
                return false;
            }

            return false;
        }

        private bool OtherPlacesAvailable(Stack stack, bool containerIsNormal, int x, int y)
        {
            if (_stacks.Any(s => s.X != x && s.Y == y && s.HeighestContainerZ < stack.HeighestContainerZ)) return true;
            if (containerIsNormal && _stacks.Any(s => s.X == x && s.Y > y && s.HeighestContainerZ < stack.HeighestContainerZ)) return true;
            return false;
        }

        private bool PlaceContainer(Stack stack, Container container)
        {
            if (!stack.ContainerCanBeAdded(this, container)) return false;

            stack.AddContainer(container);
            return true;
        }
    }
}
