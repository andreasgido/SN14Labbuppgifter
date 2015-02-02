using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb5NivaB
{
    public class ButtonSensor
    {
        // Egenskaper.
        public bool IsOn { get; set; }

        // Konstruktorer.

        public ButtonSensor(bool isOn)
        {
            IsOn = isOn;
        }
    }
}
