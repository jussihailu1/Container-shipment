using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2
{
    public class ShipManager
    {
        public Ship Ship { get; set; }
        public List<Container> ContainersToPlace { get; set; }
        public List<Container> NotPlacedContainers { get; set; }

        public ShipManager(int width, int length)
        {
            Ship = new Ship(width, length);
            ContainersToPlace = new List<Container>();
            NotPlacedContainers = new List<Container>();
        }

        //Is het nodig om hiervoor een aparte factory te maken?
        public void CreateContainers(Dictionary<ContainerType, int> containersToCreate)
        {
            foreach (KeyValuePair<ContainerType, int> containerToCreate in containersToCreate)
            {
                for (int i = 0; i < containerToCreate.Value; i++)
                {
                    var weight = new Random().Next(4, 31);

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
            ContainersToPlace = ContainersToPlace.OrderByDescending(container => container.Weight).ToList();

            var cooledContainers = ContainersToPlace.Where(container => container.ContainerType == ContainerType.Cooled).ToList();
            var normalContainers = ContainersToPlace.Where(container => container.ContainerType == ContainerType.Normal).ToList();
            var valuableContainers = ContainersToPlace.Where(container => container.ContainerType == ContainerType.Valuable).ToList();

            foreach (var cooledContainer in cooledContainers)
            {
                if (!Ship.PlaceCooledContainer(cooledContainer))
                {
                    NotPlacedContainers.Add(cooledContainer);
                }
            }

            foreach (var normalContainer in normalContainers)
            {
                if (!Ship.PlaceNormalAndValuableContainer(normalContainer))
                {
                    NotPlacedContainers.Add(normalContainer);
                }
            }

            foreach (var valuableContainer in valuableContainers)
            {
                if (!Ship.PlaceNormalAndValuableContainer(valuableContainer))
                {
                    NotPlacedContainers.Add(valuableContainer);
                }
            }
        }
    }
}
