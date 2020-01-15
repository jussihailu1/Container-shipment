using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainerShipmentV2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerShipmentV2Tests
{
    [TestClass()]
    public class ShipTests
    {
        //private bool FindPlaceForContainerTest(int CCAmount, int NCAmount, int VCAmount, Container container, int shipW, int shipL, int seed)
        //{
        //    var containersToCreate = new Dictionary<ContainerType, int>()
        //    {
        //        [ContainerType.Cooled] = CCAmount,
        //        [ContainerType.Normal] = NCAmount,
        //        [ContainerType.Valuable] = VCAmount
        //    };

        //    var sm = new ShipManager(shipW, shipL);
        //    sm.CreateContainers(containersToCreate);
        //    sm.PlaceContainers();
        //    return  sm.Ship.FindPlaceForContainer(container);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_CooledContainer_ExpectTrue()
        //{
        //    var container = new Container(10, ContainerType.Normal);

        //    var result = FindPlaceForContainerTest(20, 200, 30, container, 5, 10);

        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_CooledContainer_ExpectFalse()
        //{
        //    var container = new Container(10, ContainerType.Cooled);

        //    var result = FindPlaceForContainerTest(100, 200, 30, container, 5, 10);

        //    Assert.IsFalse(result);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_NormalContainer_ExpectTrue()
        //{
        //    var container = new Container(10, ContainerType.Normal);

        //    var result = FindPlaceForContainerTest(50, 200, 30, container, 5, 10);

        //    Assert.IsTrue(result);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_ValuableContainer_ExpectTrue()
        //{
        //    var container = new Container(10, ContainerType.Normal);

        //    var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

        //    Assert.IsFalse(result);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_ValuableContainer_ExpectFalse()
        //{
        //    var container = new Container(10, ContainerType.Normal);

        //    var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

        //    Assert.IsFalse(result);
        //}

        //[TestMethod()]
        //public void FindPlaceForContainer_NormalContainer_ExpectFalse()
        //{
        //    var container = new Container(10, ContainerType.Normal);

        //    var result = FindPlaceForContainerTest(50, 600, 30, container, 5, 10);

        //    Assert.IsFalse(result);
        //}
    }
}