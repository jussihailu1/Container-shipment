using System;
using System.Collections.Generic;
using System.Text;
using ContainerShipmentV1.Containers;

namespace ContainerShipmentV1.Factory
{
    public static class ContainerFactory
    {
        public static List<Container> CreateContainersWithRndWeight(Dictionary<ContainerType, int> containersToCreate)
        {
            var containersToReturn = new List<Container>();

            foreach (KeyValuePair<ContainerType, int> containerToCreate in containersToCreate)
            {
                for (int i = 0; i < containerToCreate.Value; i++)
                {
                    containersToReturn.Add(CreateContainer(containerToCreate.Key));
                }
            }

            return containersToReturn;
        }

        private static Container CreateContainer(ContainerType containerType)
        {
            var weight = new Random().Next(4, 31);

            switch (containerType)
            {
                case ContainerType.Cooled:
                    return new CooledContainer(weight);
                case ContainerType.Normal:
                    return new NormalContainer(weight);
                case ContainerType.Valuable:
                    return new ValuableContainer(weight);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
