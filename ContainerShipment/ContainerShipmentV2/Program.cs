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
            Console.WindowHeight = Console.LargestWindowHeight;

            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 50,
                [ContainerType.Normal] = 150,
                [ContainerType.Valuable] = 30
            };

            Console.WriteLine("Hello World!");

            var sm = new ShipManager(6, 10);
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

                        var stringToAdd = " ";

                        if (container != null)
                        {
                            //stringToAdd = $"{container.WeightAbove}";
                            stringToAdd = $"{container.ContainerType.ToString().Substring(0, 1)}";
                            //stringToAdd = $"{container.Weight}";
                            //stringToAdd = $"{container.Index}";
                            //stringToAdd = $"{container.ContainerType.ToString().Substring(0, 1)}{container.Index}";
                        }

                        shipString.Append($"[{stringToAdd:d2}]");
                    }

                    shipString.AppendLine();
                }
                shipString.AppendLine();
                shipString.AppendLine();
            }

            var offset = 0;

            //TODO: WRM WERKT DIT NIET?
            //for (int i = 0; i < shipString.Length; i++)
            //{
            //    var c = shipString[i].ToString();
            //    var left = i;
            //    var top = Console.CursorTop - 1;
            //    if (c == "\r")
            //    {
            //        c = string.Empty;
            //        offset += 30;
            //        left -= offset;
            //        top++;
            //    }

            //    Console.SetCursorPosition(left, top);
            //    Console.WriteLine(c);
            //}

            Console.WriteLine(shipString);
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //var test = "TESTTESTTESTTESTTEST";
            //Console.WriteLine("Please");
            //Console.WriteLine("");
            //int j = 0;
            //foreach (var c in test)
            //{
            //    Console.BackgroundColor = j % 2 == 0 ? ConsoleColor.Red : ConsoleColor.Blue;
            //    Console.SetCursorPosition(Console.CursorLeft + j++, Console.CursorTop - 1);
            //    Console.WriteLine(c);
            //}

            Console.Read();
        }
    }
}
