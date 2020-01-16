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

        [TestMethod]
        public void Ship_Is_In_Balance_ExpectTrue()
        {
            var sm = new ShipManager(2, 2);

            for (int i = 0; i < 2; i++)
            {
                sm.Ship.Stacks.First(s => s.X == 0).AddContainer(new Container(30, ContainerType.Normal));
            }

            for (int i = 0; i < 2; i++)
            {
                sm.Ship.Stacks.First(s => s.X == 1).AddContainer(new Container(30, ContainerType.Normal));
            }

            Assert.IsTrue(sm.Ship.IsShipInBalance());
        }

        [TestMethod]
        public void Ship_Is_In_Balance_ExpectFalse()
        {
            var sm = new ShipManager(2, 2);
            for (int i = 0; i < 5; i++)
            {
                sm.Ship.Stacks.First(s => s.X == 0).AddContainer(new Container(30, ContainerType.Normal));
            }
            Assert.IsFalse(sm.Ship.IsShipInBalance());
        }

        [TestMethod]
        public void Ship_Half_Of_MaxWeight_Reached_ExpectTrue()
        {
            var sm = new ShipManager(1, 1);
            for (int i = 0; i < 4; i++)
            {
                sm.Ship.Stacks.First().AddContainer(new Container(30, ContainerType.Normal));
            }
            Assert.IsTrue(sm.Ship.HalfOfMaxWeightReached);
        }

        [TestMethod]
        public void Ship_Half_Of_MaxWeight_Reached_ExpectFalse()
        {
            var sm = new ShipManager(1, 1);
            for (int i = 0; i < 2; i++)
            {
                sm.Ship.Stacks.First().AddContainer(new Container(30, ContainerType.Normal));
            }
            Assert.IsFalse(sm.Ship.HalfOfMaxWeightReached);
        }

        [TestMethod]
        public void ShipConstructor_MaxWeight_Correctly_Calculated()
        {
            var width = 6;
            var length = 10;
            var expected = width * length * 150;

            var ship = new Ship(width, length);

            Assert.AreEqual(expected, ship.MaxWeight);
        }

        [TestMethod]
        public void Ship_CurrentWeight_Correctly_Calculated_Even_Width()
        {
            var sm = new ShipManager(4, 5);

            for (int i = 0; i < 5; i++)
            {
                sm.ContainersToPlace.Add(new Container(15, ContainerType.Normal));
            }

            sm.PlaceContainers();

            var expected = 15 * 5;

            Assert.AreEqual(expected, sm.Ship.CurrentTotalWeight);
        }

        [TestMethod]
        public void Ship_CurrentWeight_Correctly_Calculated_Uneven_Width()
        {
            var sm = new ShipManager(5, 10);

            for (int i = 0; i < 5; i++)
            {
                sm.ContainersToPlace.Add(new Container(15, ContainerType.Normal));
            }

            sm.PlaceContainers();

            var expected = 15 * 5;

            Assert.AreEqual(expected, sm.Ship.CurrentTotalWeight);
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