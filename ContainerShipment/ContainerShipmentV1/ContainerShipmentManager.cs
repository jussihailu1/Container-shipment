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

        private readonly List<Container> _undistributedContainers;

        public List<Container> UndistributedContainers => _undistributedContainers;

        public ContainerShipmentManager()
        {
            Ships = new List<Ship>();
            _undistributedContainers = new List<Container>();
        }

        public List<Container> CreateContainers(Dictionary<ContainerType, int> containerOrder) => ContainerFactory.CreateContainersWithRndWeight(containerOrder);

        public void Distribute(List<Container> containers)
        {
            containers.OrderBy(container => container.Weight);

            while (containers.Count > 0)
            {
                var cooledContainers = containers.Where(container => container is CooledContainer).ToList();
                var normalContainers = containers.Where(container => container is NormalContainer).ToList();
                var valuableContainers = containers.Where(container => container is ValuableContainer).ToList();

                _undistributedContainers.Clear();

                Ships.Add(ShipFactory.CreateShip(5, 10));

                foreach (Ship ship in Ships)
                {
                    DistributeCooledContainers(cooledContainers, ship);

                    DistributeNormalContainers(normalContainers, ship);

                    DistributeValuableContainers(valuableContainers, ship);
                }

                containers = _undistributedContainers;
            }
        }

        private void DistributeCooledContainers(List<Container> containers, Ship ship)
        {
            int shipWidth = ship.Width;
            int posZ = 0;
            int posY = 0;
            int offset = 0;
            bool previousContainerIsDistributed = false;
            int undistributedContainersCount = 0;

            foreach (var container in containers)
            {
                int containerIndex = containers.IndexOf(container) - undistributedContainersCount;
                bool indexIsRound = (containerIndex / (decimal)shipWidth) % 1 == 0;

                if (containerIndex != 0 && indexIsRound)
                {
                    posZ++;

                    if (previousContainerIsDistributed)
                    {
                        offset += shipWidth;
                    }
                }

                int posX = containerIndex - offset;

                if (posZ == 0)
                {
                    previousContainerIsDistributed = true;
                    PlaceContainer(ship, container, posX, posY, posZ);
                }
                else
                {
                    var totalWeightOfStack = ship.Containers.Where(c => c.VectorPoint.X == posX && c.VectorPoint.Y == posY && c.VectorPoint.Z > 0).Sum(c => c.Weight);
                    var bottomContainer = ship.Containers.First(c => c.VectorPoint.X == posX && c.VectorPoint.Y == posY && c.VectorPoint.Z == 0);
                    if (IsBelowMaxWeight(totalWeightOfStack, container.Weight, bottomContainer.MaxWeightAbove))
                    {
                        previousContainerIsDistributed = true;
                        PlaceContainer(ship, container, posX, posY, posZ);
                    }
                    else
                    {
                        undistributedContainersCount++;
                        previousContainerIsDistributed = false;
                        _undistributedContainers.Add(container);
                    }
                }
            }
        }

        //public int SetPosition(int shipWidth, int pContainerX, int ppContainerX)
        //{
        //    int posX;

        //    if (pContainerX > shipWidth / 2)
        //    {
        //        if (ppContainerX > shipWidth / 2)
        //        {
        //            posX = 
        //        }
        //    }
        //}

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
