using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1.Factory
{
    public static class ShipFactory
    {
        public static Ship CreateShip(int width, int length) => new Ship(width, length);
    }
}
