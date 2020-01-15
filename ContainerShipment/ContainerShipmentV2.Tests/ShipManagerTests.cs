using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerShipmentV2.Tests
{
    [TestClass]
    public class ShipManagerTests
    {
        [TestMethod]
        public void CreateContainersTest()
        {
            var CCAmount = 50;
            var NCAmount = 150;
            var VCAmount = 30;
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = CCAmount,
                [ContainerType.Normal] = NCAmount,
                [ContainerType.Valuable] = VCAmount
            };

            var sm = new ShipManager(5, 10);
            sm.CreateContainers(containersToCreate);

            var createdCooledContainers = sm.ContainersToPlace.Count(c => c.ContainerType == ContainerType.Cooled);
            Assert.AreEqual(CCAmount, createdCooledContainers);

            var createdNormalContainers = sm.ContainersToPlace.Count(c => c.ContainerType == ContainerType.Normal);
            Assert.AreEqual(NCAmount, createdNormalContainers);

            var createdValuableContainers = sm.ContainersToPlace.Count(c => c.ContainerType == ContainerType.Valuable);
            Assert.AreEqual(VCAmount, createdValuableContainers);
        }

        [TestMethod]
        public void PlaceContainersTest_ExpectTrue()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 20,
                [ContainerType.Normal] = 100,
                [ContainerType.Valuable] = 30
            };

            var sm = new ShipManager(10, 20);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            CollectionAssert.AreEquivalent(sm.Ship.PlacedContainers.ToList(), sm.ContainersToPlace);
        }

        [TestMethod]
        public void PlaceContainersTest_ExpectFalse()
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = 50,
                [ContainerType.Normal] = 400,
                [ContainerType.Valuable] = 60
            };

            var sm = new ShipManager(5, 8);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            CollectionAssert.AreNotEquivalent(sm.Ship.PlacedContainers.ToList(), sm.ContainersToPlace);
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
            var result = false;

            foreach (var stack in sm.Ship.Stacks)
            {
                foreach (var container in stack.Containers)
                {
                    if (container.WeightAbove <= 120) continue;
                    result = true;
                    goto END;
                }
            }

            END:

            Assert.IsFalse(result);
        }
    }
}
