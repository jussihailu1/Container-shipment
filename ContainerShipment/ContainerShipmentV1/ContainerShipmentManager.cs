using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContainerShipmentV1.Factory;
using ContainerShipmentV1.Containers;

namespace ContainerShipmentV1
{
    public class ContainerShipmentManager
    {
        public List<Ship> Ships { get; set; }

        private readonly List<Container> _notPlacedContainers;

        public IEnumerable<Container> NotPlacedContainers => _notPlacedContainers;

        public ContainerShipmentManager()
        {
            Ships = new List<Ship>();
            _notPlacedContainers = new List<Container>();
        }

        public List<Container> CreateContainers(Dictionary<ContainerType, int> containerOrder) => ContainerFactory.CreateContainersWithRndWeight(containerOrder);

        public void Distribute(List<Container> containers)
        {
            containers = containers.OrderByDescending(container => container.Weight).ToList();

            var cooledContainers = containers.Where(container => container is CooledContainer).ToList();
            var normalContainers = containers.Where(container => container is NormalContainer).ToList();
            var valuableContainers = containers.Where(container => container is ValuableContainer).ToList();

            var ship = ShipFactory.CreateShip(5, 10);
            Ships.Add(ship);

            DistributeCooledContainers(cooledContainers, ship);

            DistributeNormalContainers(normalContainers, ship);

            DistributeValuableContainers(valuableContainers, ship);
        }

        private void DistributeCooledContainers(List<Container> containers, Ship ship)
        {
            int shipWidth = ship.Width;
            int posZ = 0;
            int posY = 0;
            int offset = 0;
            bool previousContainerIsDistributed = false;

            foreach (var container in containers)
            {
                int containerIndex = containers.IndexOf(container) - _notPlacedContainers.Count;
                bool indexIsRound = (containerIndex / (decimal)shipWidth) % 1 == 0; //Kijken wat hier eigenlijk berekent wordt.
                bool isContainerPlaced = false;

                if (containerIndex != 0 && indexIsRound && previousContainerIsDistributed)
                {
                    posZ++;
                    offset += shipWidth;
                }

                var posX = containerIndex - offset;

                if (IsPlaceAvailable(ship, posX, posY, posZ))
                {
                    if (posZ == 0)
                    {
                        PlaceContainer(ship, container, posX, posY, posZ);
                        previousContainerIsDistributed = true;
                        isContainerPlaced = true;
                    }
                    else
                    {
                        var totalWeightOfStack = ship.Containers.Where(c => c.VectorPoint.X == posX && c.VectorPoint.Y == posY && c.VectorPoint.Z > 0).Sum(c => c.Weight);
                        var bottomContainer = ship.Containers.First(c => c.VectorPoint.X == posX && c.VectorPoint.Y == posY && c.VectorPoint.Z == 0);
                        if (IsBelowMaxWeight(totalWeightOfStack, container.Weight, bottomContainer.MaxWeightAbove))
                        {
                            PlaceContainer(ship, container, posX, posY, posZ);
                            previousContainerIsDistributed = true;
                            isContainerPlaced = true;
                        }
                    }
                }

                if (isContainerPlaced) continue;
                previousContainerIsDistributed = false;
                _notPlacedContainers.Add(container);
            }

            Console.WriteLine(" ");
        }

        private bool IsPlaceAvailable(Ship ship, int x, int y, int z)
        {
            var placeToCheck = new VectorPoint(x, y, z);

            foreach (var container in ship.Containers)
            {
                if (container.VectorPoint == placeToCheck)
                {
                    return false;
                }
            }

            return true;
        }

        private void DistributeNormalContainers(List<Container> containers, Ship ship)
        {

        }

        private void DistributeValuableContainers(List<Container> containers, Ship ship)
        {

        }

        private bool IsBelowMaxWeight(int totalStackWeight, int weightContainerToPlace, int maxWeight)
        {
            return totalStackWeight + weightContainerToPlace <= maxWeight;
        }

        private void PlaceContainer(Ship ship, Container container, int x, int y, int z)
        {
            container.VectorPoint = new VectorPoint(x, y, z);
            ship.Containers.Add(container);
        }
    }
}
