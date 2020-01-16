using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainerShipmentV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2.Tests
{
    [TestClass()]
    public class ShipTests
    {
        [TestMethod]
        public void ShipConstructor_AreStacksMade()
        {
            var width = 7;
            var length = 15;
            var ship = new Ship(width, length);
            var expected = width * length;

            var expectedStacks = new List<Stack>();
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    expectedStacks.Add(new Stack(x, y));
                }
            }

            var result = true;

            foreach (var expectedStack in expectedStacks)
            {
                if (ship.Stacks.Count(stack => stack.X == expectedStack.X && stack.Y == expectedStack.Y) == 1) continue;
                result = false;
                goto END;
            }

            END:

            Assert.AreEqual(expected, ship.Stacks.Count());
            Assert.IsTrue(result);
        }

        private bool FindPlaceForContainerTest(int CCAmount, int NCAmount, int VCAmount, Container container, int shipW, int shipL)
        {
            var containersToCreate = new Dictionary<ContainerType, int>()
            {
                [ContainerType.Cooled] = CCAmount,
                [ContainerType.Normal] = NCAmount,
                [ContainerType.Valuable] = VCAmount
            };

            var sm = new ShipManager(shipW, shipL);
            sm.CreateContainers(containersToCreate);
            sm.PlaceContainers();
            return sm.Ship.FindPlaceForContainer(container);
        }

        [TestMethod()]
        public void FindPlaceForContainer_CooledContainer_ExpectTrue()
        {
            var container = new Container(10, ContainerType.Normal);

            var result = FindPlaceForContainerTest(20, 200, 30, container, 5, 10);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void FindPlaceForContainer_CooledContainer_ExpectFalse()
        {
            var container = new Container(10, ContainerType.Cooled);

            var result = FindPlaceForContainerTest(100, 200, 30, container, 5, 10);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void FindPlaceForContainer_NormalContainer_ExpectTrue()
        {
            var container = new Container(10, ContainerType.Normal);

            var result = FindPlaceForContainerTest(50, 200, 30, container, 5, 10);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void FindPlaceForContainer_NormalContainer_ExpectFalse()
        {
            var container = new Container(10, ContainerType.Normal);

            var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void FindPlaceForContainer_ValuableContainer_ExpectTrue()
        {
            var container = new Container(10, ContainerType.Normal);

            var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void FindPlaceForContainer_ValuableContainer_ExpectFalse()
        {
            var container = new Container(10, ContainerType.Normal);

            var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

            Assert.IsFalse(result);
        }
    }
}