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

            for (int i = 0; i < 10; i++)
            {
                ContainerShipment();
            }

            Console.Read();

            void ContainerShipment()
            {
                Console.WriteLine(" ");

                ContainerShipmentManager containerShipmentManager = new ContainerShipmentManager();

                Dictionary<ContainerType, int> dictionary = new Dictionary<ContainerType, int>()
                {
                    [ContainerType.Cooled] = 50
                };
                var containersToDistribute = containerShipmentManager.CreateContainers(dictionary);
                containerShipmentManager.Distribute(containersToDistribute);

                Console.WriteLine("Total containers to place: " + containersToDistribute.Count);
                Console.WriteLine("Not placed containers: " + containerShipmentManager.NotPlacedContainers.Count());
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                foreach (var ship in containerShipmentManager.Ships)
                {
                    var shipNr = containerShipmentManager.Ships.IndexOf(ship) + 1;

                    Console.WriteLine("SHIP " + shipNr + " ------------------------------------------------------------------------------------");
                    Console.WriteLine("Ship width: " + ship.Width + " and length: " + ship.Length);

                    var heighestContainer = ship.Containers.OrderByDescending(container => container.VectorPoint.Z).ToList()[0].VectorPoint.Z;

                    Console.WriteLine("Heighest level: " + ++heighestContainer);
                    Console.WriteLine("Container count in ship: " + ship.Containers.Count);

                    for (int i = heighestContainer; i >= 0; i--)
                    {
                        string containersInRow = "";
                        foreach (var container in ship.Containers.Where(container => container.VectorPoint.Z == i))
                        {
                            //string containerString = container.Weight.ToString();
                            string containerString = ship.Containers.Where(c => c.VectorPoint.X == container.VectorPoint.X && c.VectorPoint.Y == container.VectorPoint.Y && c.VectorPoint.Z > 0 && c.VectorPoint.Z < container.VectorPoint.Z + 1).Sum(c => c.Weight).ToString();
                            //string containerString = container.ToString();

                            int maxStringLength = 10;

                            int charactersLeft = maxStringLength - containerString.Length;

                            for (int j = 0; j < charactersLeft; j++)
                            {
                                containerString += " ";
                            }

                            containersInRow += containerString;
                        }
                        Console.WriteLine(containersInRow);
                    }


                    string totalWeightForEachRow = "";
                    for (int i = 0; i < ship.Width; i++)
                    {
                        string totalWeightString = ship.Containers.Where(c => c.VectorPoint.X == i && c.VectorPoint.Z > 0).Sum(c => c.Weight).ToString();

                        int maxStringLength = 10;

                        int charactersLeft = maxStringLength - totalWeightString.Length;

                        for (int j = 0; j < charactersLeft; j++)
                        {
                            totalWeightString += " ";
                        }

                        totalWeightForEachRow += totalWeightString;

                        //totalWeightForEachRow += ship.Containers.Where(c => c.VectorPoint.X == i && c.VectorPoint.Z > 0).Sum(c => c.Weight);
                        //totalWeightForEachRow += "         ";
                    }
                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                    Console.WriteLine(totalWeightForEachRow);


                    Console.WriteLine("-------------------------------------------------------------------------------------------");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                }
            }
        }

    }
}
