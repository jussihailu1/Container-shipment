using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerShipmentV1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(" ");

            ContainerShipmentManager containerShipmentManager = new ContainerShipmentManager();

            Dictionary<ContainerType, int> dictionary = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 25
            };

            var containersToDistribute = containerShipmentManager.CreateContainers(dictionary);
            containerShipmentManager.Distribute(containersToDistribute);

            foreach (var ship in containerShipmentManager.Ships)
            {
                var shipNr = containerShipmentManager.Ships.IndexOf(ship) + 1;

                Console.WriteLine("Ship " + shipNr);

                var heighestContainer = ship.Containers.OrderByDescending(container => container.VectorPoint.Z).ToList()[0].VectorPoint.Z;

                Console.WriteLine("Heighest level: " + ++heighestContainer);
                Console.WriteLine("Container count in ship: " + ship.Containers.Count);
                Console.WriteLine("Undistributed: " + containerShipmentManager.UndistributedContainers.Count);

                for (int i = heighestContainer; i >= 0; i--)
                {
                    string s = "";
                    foreach (var container in ship.Containers.Where(container => container.VectorPoint.Z == i))
                    {
                        string cString = container.ToString();
                        if (cString.Length < 20)
                        {
                            string space = " ";
                            for (int j = 0; j < 20 - cString.Length; j++)
                            {
                                cString += space;
                            }

                            s += cString + " ";
                        }
                        else
                        {
                            s += $"[{container.ToString()}  W = {container.Weight}]";
                        }
                    }
                    Console.WriteLine(s);
                }

                Console.WriteLine("-------------------------------------------------------------------------------------------");
            }

            Console.Read();
        }
    }
}
