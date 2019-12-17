using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerShipmentV1
{
    public interface IContainer
    {
         int Weight { get; set; }
         VectorPoint VectorPoint { get; set; }
    }
}
