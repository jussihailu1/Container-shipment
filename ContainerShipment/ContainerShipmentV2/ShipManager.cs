using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class ShipManager
    {
        public Ship Ship { get; }
        public List<Container> ContainersToPlace { get; private set; }
        public List<Container> NotPlacedContainers { get; }

        public ShipManager(int width, int length)
        {
            Ship = new Ship(width, length);
            ContainersToPlace = new List<Container>();
            NotPlacedContainers = new List<Container>();
        }

        public void CreateContainers(Dictionary<ContainerType, int> containersToCreate)
        {
            int seed = new Random().Next();
            Console.WriteLine("Seed: " + seed);
            var rnd = new Random(seed);
            Console.WriteLine();
            foreach (KeyValuePair<ContainerType, int> containerToCreate in containersToCreate)
            {
                for (int i = 0; i < containerToCreate.Value; i++)
                {
                    var weight = rnd.Next(4, 31);

                    switch (containerToCreate.Key)
                    {
                        case ContainerType.Cooled:
                            ContainersToPlace.Add(new Container(weight, ContainerType.Cooled));
                            break;
                        case ContainerType.Normal:
                            ContainersToPlace.Add(new Container(weight, ContainerType.Normal));
                            break;
                        case ContainerType.Valuable:
                            ContainersToPlace.Add(new Container(weight, ContainerType.Valuable));
                            break;
                    }
                }
            }
        }

        public void PlaceContainers()
        {
            ContainersToPlace = ContainersToPlace.OrderBy(c => c.ContainerType == ContainerType.Valuable)
                .ThenBy(c => c.ContainerType == ContainerType.Normal)
                .ThenBy(c => c.ContainerType == ContainerType.Cooled)
                .ThenByDescending(container => container.Weight)
                .ToList();

            foreach (var container in ContainersToPlace)
            {
                if (!Ship.FindPlaceForContainer(container))
                {
                    NotPlacedContainers.Add(container);
                }
            }
        }
    }
}
