using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContainerShipmentV1.Container;
using ContainerShipmentV1.Factory;

namespace ContainerShipmentV1
{
    public class ContainerShipmentManager
    {
        public List<Ship> Ships { get; set; }

        private readonly List<IContainer> _undistributedContainers;

        public ContainerShipmentManager()
        {
            Ships = new List<Ship>();
            _undistributedContainers = new List<IContainer>();
        }

        public void Distribute(List<IContainer> containers)
        {
            do
            {
                _undistributedContainers.Clear();

                var cooledContainers = containers.Where(container => container is CooledContainer).ToList();
                var normalContainers = containers.Where(container => container is NormalContainer).ToList();
                var valuableContainers = containers.Where(container => container is ValuableContainer).ToList();

                if (Ships.Count == 0)
                {
                    Ships.Add(ShipFactory.CreateShip(6, 10));
                }

                containers.OrderBy(container => container.Weight);
                Console.WriteLine(containers[0] + " " + containers[containers.Count]);

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

        private void DistributeCooledContainers(List<IContainer> containers, Ship ship)
        {
            foreach (var container in containers)
            {
                
            }
        }

        private void DistributeNormalContainers(List<IContainer> containers, Ship ship)
        {

        }

        private void DistributeValuableContainers(List<IContainer> containers, Ship ship)
        {

        }
    }
}
