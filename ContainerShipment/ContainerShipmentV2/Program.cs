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
                [ContainerType.Normal] = 50
            };

            Console.WriteLine("Hello World!");

            var sm = new ShipManager(5, 10);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();

            var ship = sm.Ship;
            var shipString = new StringBuilder();

            int shipHeightContainer = ship.Stacks.Max(s => s.HeighestContainer);

            for (int z = 0; z < shipHeightContainer; z++)
            {
                for (int x = 0; x < ship.Width; x++)
                {
                    for (int y = 0; y < ship.Length; y++)
                    {
                        Container container = null;
                        if (ship.Stacks.FirstOrDefault(s => s.X == x && s.Y == y).Containers.Count > z )
                        {
                            container = ship.Stacks.FirstOrDefault(s => s.X == x && s.Y == y)?.Containers[z];
                        }

                        shipString.Append(container == null
                            ? "[ ]"
                            : container.ToString(StringValue.ContainerType));
                        //TODO: We kunnen heel die tostring djaaien en gewoon zeggen container.weight.tostring of container.containertype.tostring lol.
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
