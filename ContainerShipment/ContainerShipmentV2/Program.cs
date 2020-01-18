using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ContainerShipmentV2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 
            Console.WindowHeight = Console.LargestWindowHeight;

            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 500,
                [ContainerType.Normal] = 500,
                [ContainerType.Valuable] = 100

            };
            var rnd = new Random();

            int width = rnd.Next(3, 10);
            var sm = new ShipManager(width, width * 5);
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
            var halfOfMaxWeightReached = ship.IsShipInBalance() && ship.HalfOfMaxWeightReached ? "YES" : "NO";
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
                            //stringToAdd = $"{container.ContainerType.ToString().Substring(0, 1)}";
                            //stringToAdd = $"{container.Weight.ToString().PadRight(2)}";
                            stringToAdd = $"{container.ContainerType.ToString().Substring(0, 1)}{container.WeightAbove}";
                        }

                        shipString.Append($"[{stringToAdd.ToString().PadRight(3)}]");
                    }

                    shipString.AppendLine();
                }
                shipString.AppendLine();
                shipString.AppendLine();
            }

            Console.WriteLine(shipString);

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            string total = "";

            for (int i = 0; i < ship.Width; i++)
            {
                var row = ship.Stacks.Where(s => s.X == i).ToList();
                foreach (var stack in row)
                {
                    foreach (var container in stack.Containers)
                    {
                        switch (container.ContainerType)
                        {
                            case ContainerType.Cooled: total += "3"; break;
                            case ContainerType.Normal: total += "1"; break;
                            case ContainerType.Valuable: total += "2"; break;
                        }

                        if (container != stack.Containers.Last())
                        {
                            total += "-";
                        }
                    }

                    if (stack != row.Last())
                    {
                        total += ",";
                    }
                }

                if (i != ship.Width - 1)
                {
                    total += "/";
                }
            }

            string unityLink = $"https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?length={ship.Length}&width={ship.Width}&stacks={total}";
            var unityUri = new Uri(unityLink);
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl(unityUri);

            Console.Read();
        }
    }
}
