using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb5NivaB
{
    public class DoorSensor
    {
        // Egenskaper.
        public bool DoorIsOpen { get; set; }

        // Konstruktorer

        public DoorSensor(bool doorIsOpen)
        {
            DoorIsOpen = doorIsOpen;
        }
    }
}
