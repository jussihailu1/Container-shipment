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
    }
}
