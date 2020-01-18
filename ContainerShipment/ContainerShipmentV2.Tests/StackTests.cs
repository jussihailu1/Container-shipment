using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerShipmentV2.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void ContainerCanBeAdded_CooledContainer_ExpectTrue()
        {
            var ship = new Ship(4, 7);
            var containerToAdd = new Container(18, ContainerType.Cooled);
            var stack = ship.Stacks.First(); //x = 0 & y = 0
            for (int i = 0; i < 4; i++)
            {
                var container = new Container(30, ContainerType.Cooled);
                stack.AddContainer(container);
            }
            var result = stack.ContainerCanBeAdded(ship, containerToAdd);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainerCanBeAdded_CooledContainer_ExpectFalse()
        {
            var ship = new Ship(4, 7);
            var containerToAdd = new Container(18, ContainerType.Cooled);
            var stack = ship.Stacks.First(s => s.Y != 0);
            for (int i = 0; i < 3; i++)
            {
                var container = new Container(30, ContainerType.Cooled);
                stack.AddContainer(container);
            }

            var result = stack.ContainerCanBeAdded(ship, containerToAdd);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ContainerCanBeAdded_WeightCheck_ExpectPlacement()
        {
            var ship = new Ship(4, 7);
            var containerToAdd = new Container(18, ContainerType.Normal);
            var stack = ship.Stacks.First(); //x = 0 & y = 0
            for (int i = 0; i < 4; i++)
            {
                var container = new Container(30, ContainerType.Normal);
                stack.AddContainer(container);
            }

            var result = stack.ContainerCanBeAdded(ship, containerToAdd);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ContainerCanBeAdded_WeightCheck_ExpectNoPlacement()
        {
            var ship = new Ship(4, 7);
            var containerToAdd = new Container(18, ContainerType.Normal);
            var stack = ship.Stacks.First(); //x = 0 & y = 0
            for (int i = 0; i < 5; i++)
            {
                var container = new Container(30, ContainerType.Normal);
                stack.AddContainer(container);
            }

            var result = stack.ContainerCanBeAdded(ship, containerToAdd);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void PlaceContainersTest_ContainerMaxWeightAboveExceeded()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 50,
                [ContainerType.Normal] = 50,
                [ContainerType.Valuable] = 60
            };

            var sm = new ShipManager(5, 8);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            var result = sm.Ship.PlacedContainers.Any(container => container.WeightAbove > 120);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PlaceContainersTest_AllValuableContainersReachable()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 100,
                [ContainerType.Normal] = 200,
                [ContainerType.Valuable] = 100
            };

            var sm = new ShipManager(8, 12);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            var result = true;

            foreach (var stack in sm.Ship.Stacks)
            {
                foreach (var container in stack.Containers)
                {
                    var stackBehind = sm.Ship.Stacks.FirstOrDefault(s => s.X == stack.X && stack.Y == stack.Y + 1);
                    var stackInFront = sm.Ship.Stacks.FirstOrDefault(s => s.X == stack.X && stack.Y == stack.Y - 1);

                    if (container.ContainerType != ContainerType.Valuable ||
                        stackBehind == null || stackBehind.HeighestContainerZ <= stack.HeighestContainerZ ||
                        stackInFront == null || stackInFront.HeighestContainerZ <= stack.HeighestContainerZ) continue;
                    result = false;
                    goto END;
                }
            }

            END:

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void PlaceContainersTest_AllValuableContainersOnTop()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 50,
                [ContainerType.Normal] = 50,
                [ContainerType.Valuable] = 60
            };

            var sm = new ShipManager(5, 8);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            var result = true;

            foreach (var stack in sm.Ship.Stacks)
            {
                foreach (var container in stack.Containers)
                {
                    if (container.ContainerType != ContainerType.Valuable ||
                        container == stack.Containers.Last()) continue;
                    result = false;
                    goto END;
                }
            }

            END:

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PlaceContainersTest_AllCooledContainersInFront()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 100,
                [ContainerType.Normal] = 100,
                [ContainerType.Valuable] = 60
            };

            var sm = new ShipManager(5, 8);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            var result = true;

            foreach (var stack in sm.Ship.Stacks)
            {
                foreach (var container in stack.Containers)
                {
                    if (container.ContainerType != ContainerType.Cooled || stack.Y == 0) continue;
                    result = false;
                    goto END;
                }
            }

            END:

            Assert.IsTrue(result);
        }
    }
}
