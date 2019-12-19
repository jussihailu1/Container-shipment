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
            do
            {
                _undistributedContainers.Clear();

                var cooledContainers = containers.Where(container => container is CooledContainer).ToList();
                var normalContainers = containers.Where(container => container is NormalContainer).ToList();
                var valuableContainers = containers.Where(container => container is ValuableContainer).ToList();

                if (Ships.Count == 0)
                {
                    Ships.Add(ShipFactory.CreateShip(3, 10));
                }

                containers.OrderBy(container => container.Weight);

                foreach (Ship ship in Ships)
                {
                    DistributeCooledContainers(cooledContainers, ship);

                    DistributeNormalContainers(normalContainers, ship);

                    DistributeValuableContainers(valuableContainers, ship);
                }

                if (_undistributedContainers.Count > 0)
                {
                    containers = _undistributedContainers;
                }

            } while (_undistributedContainers.Count > 0);
        }

        private void DistributeCooledContainers(List<Container> containers, Ship ship)
        {
            var shipWidth = ship.Width;

            decimal dec = (decimal)containers.Count / (decimal)shipWidth;
            int expectedHeight = Convert.ToInt32(Math.Ceiling(dec));
            Console.WriteLine("EXHE: " + expectedHeight);

            foreach (var container in containers)
            {
                bool isDistributed = false;
                int offset = 0;

                for (int i = 0; i < expectedHeight; i++)
                {
                    int posZ = i;
                    int posX = containers.IndexOf(container) - offset;
                    
                    if (posX <= shipWidth)
                    {
                        container.VectorPoint = new VectorPoint(posX, 0, posZ);
                        ship.Containers.Add(container);
                        isDistributed = true;
                        break;
                    }

                    offset = shipWidth + 1;
                }

                if (!isDistributed)
                {
                    _undistributedContainers.Add(container);
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
    }
}
