using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainerShipmentV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2.Tests
{
    [TestClass()]
    public class StackTests
    {
        private Container cooledContainer;
        private Container valuableContainer;
        private Container normalContainer;
        private Stack stack;
        public Ship ship;
        private ShipManager shipManager;

        [TestInitialize()]
        public void Setup()
        {
            var rnd = new Random().Next(4, 31);

            cooledContainer = new Container(rnd, ContainerType.Cooled);
            valuableContainer = new Container(rnd, ContainerType.Valuable);
            normalContainer = new Container(rnd, ContainerType.Normal);
            shipManager = new ShipManager(5, 8);

        }


        [TestMethod()]
        public void ContainerCanBeAddedTest()
        {
            var containersToPlace = new List<Container>()
            {
                cooledContainer, normalContainer, valuableContainer
            };

            shipManager.ContainersToPlace = containersToPlace;
            shipManager.PlaceContainers();

            Assert.IsTrue(shipManager.ContainersToPlace != null);
        }
    }
}