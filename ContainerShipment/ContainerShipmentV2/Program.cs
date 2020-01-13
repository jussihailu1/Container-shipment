﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    class Program
    {
        static void Main(string[] args)
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 40,
                [ContainerType.Normal] = 600,
                [ContainerType.Valuable] = 30
            };

            Console.WriteLine("Hello World!");

            var sm = new ShipManager(5, 10);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();

            var ship = sm.Ship;

            Console.WriteLine($"Total containers = {containersToCreate.Values.Sum(i => i)}");
            Console.WriteLine($"Placed containers = {ship.PlacedContainers.Count()}");
            Console.WriteLine($"Not placed containers = {sm.NotPlacedContainers.Count}");
            Console.WriteLine($"Not placed Cooled containers = {sm.NotPlacedContainers.Count(c => c.ContainerType == ContainerType.Cooled)}");
            Console.WriteLine($"Not placed Normal containers = {sm.NotPlacedContainers.Count(c => c.ContainerType == ContainerType.Normal)}");
            Console.WriteLine($"Not placed Valuable containers = {sm.NotPlacedContainers.Count(c => c.ContainerType == ContainerType.Valuable)}");
            Console.WriteLine(" ");
            Console.WriteLine($"Weight: Left = {ship.WeightLeftSide} | Right = {ship.WeightRightSide} | Difference = {ship.WeightLeftSide - ship.WeightRightSide}");
            Console.WriteLine($"Max: {ship.MaxWeight} Current: {ship.CurrentTotalWeight}");
            var halfOfMaxWeightReached = ship.HalfOfMaxWeightReached && ship.IsShipInBalance ? "YES" : "NO";
            Console.WriteLine(" ");
            Console.WriteLine("Can ship leave dock: " + halfOfMaxWeightReached);
            Console.WriteLine(" ");

            var shipString = new StringBuilder();

            int shipHeightContainer = ship.Stacks.Max(s => s.HeighestContainerZ) + 1;

            for (int z = shipHeightContainer - 1; z >= 0; z--)
            {
                for (int x = 0; x < ship.Width; x++)
                {
                    for (int y = 0; y < ship.Length; y++)
                    {
                        Container container = null;
                        var stack = ship.Stacks.FirstOrDefault(s => s.X == x && s.Y == y);
                        if (stack != null && stack.Containers.Count > z)
                        {
                            container = stack.Containers[z];
                        }

                        var stringToAdd = container == null
                            ? " "
                        //: $"{container.WeightAbove}";
                        : $"{container.ContainerType.ToString().Substring(0, 1)}";
                        //: $"{container.Weight}";
                        //: $"{container.Indexer}";
                        //: $"{container.ContainerType.ToString().Substring(0, 1)}{container.Indexer}";

                        for (int i = 0; i < 3 - stringToAdd.Length; i++)
                        {
                            stringToAdd += " ";
                        }

                        shipString.Append($"[{stringToAdd}]");
                    }

                    shipString.AppendLine();
                }
                shipString.AppendLine();
                shipString.AppendLine();
            }

            Console.WriteLine(shipString);

            Console.Read();
        }
    }
}
