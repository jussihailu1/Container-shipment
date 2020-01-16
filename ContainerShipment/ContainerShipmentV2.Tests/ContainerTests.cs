using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ContainerShipmentV2.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void Container_AddWeightAbove()
        {
            var container = new Container(25, ContainerType.Normal);
            var weightToAdd = 18;
            container.AddWeightAbove(weightToAdd);

            Assert.AreEqual(weightToAdd, container.WeightAbove);
        }
    }
}
