using System;
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
                [ContainerType.Normal] = 50,
                [ContainerType.Valuable] = 30

            };

            Console.WriteLine("Hello World!");

            var sm = new ShipManager(5, 6);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();

            var ship = sm.Ship;

            Console.WriteLine($"Total containers = {containersToCreate.Values.Sum(i => i)}");
            Console.WriteLine($"Placed containers = {ship.PlacedContainers.Count}");
            Console.WriteLine($"Not placed containers = {sm.NotPlacedContainers.Count}");
            Console.WriteLine($"Not placed V containers = {sm.NotPlacedContainers.Count(c => c.ContainerType == ContainerType.Valuable)}");
            Console.WriteLine($"Left = {ship.WeightLeftSide} | Right = {ship.WeightRightSide}");
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
                        if (ship.Stacks.FirstOrDefault(s => s.X == x && s.Y == y).Containers.Count > z)
                        {
                            container = ship.Stacks.FirstOrDefault(s => s.X == x && s.Y == y)?.Containers[z];
                        }

                        shipString.Append(container == null ? "[ ]"
                        //: $"[{container.WeightAbove}]");
                        : $"[{container.ContainerType.ToString().Substring(0, 1)}]");
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
