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
        public void AddContainerTest()
        {
            var container = new Container(10, ContainerType.Normal);
            var stack = new Stack(0,0);
            stack.AddContainer(container);
            Assert.AreEqual(container, stack.Containers.First());
        }
    }
}
