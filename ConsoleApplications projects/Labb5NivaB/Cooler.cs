using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaB
{
    public class Cooler
    {
        // Fält. 
        private TemperatureDisplay _temperatureDisplay;
   
        // Egenskaper.
        public bool DoorIsOpen { get { return _temperatureDisplay.DoorIsOpen; } }

        public decimal InsideTemperature { get { return _temperatureDisplay.InsideTemperature; } }

        public bool IsOn { get { return _temperatureDisplay.IsOn; } }

        public decimal TargetTemperature { get; set; }

        // Konstruktorer.
        public Cooler()
            : this(0m, 0m)
        {

        }

        public Cooler(decimal temperature, decimal targetTemperature)
            : this(temperature, targetTemperature, false, false) 
        {

        }

        public Cooler(decimal temperature, decimal targetTemperature, bool IsOn, bool DoorIsOpen)            
        {
            //TargetTemperature = targetTemperature;
            _temperatureDisplay = new TemperatureDisplay(temperature, targetTemperature, IsOn, DoorIsOpen);
            TargetTemperature = targetTemperature;
        }

        // Metoder.
        public void Tick()
        {           
            _temperatureDisplay.Tick();                                    
        }

        public override string ToString()
        {
            string on = IsOn ? "[PÅ]" : "[AV]";
            string open = DoorIsOpen ? "Öppet" : "Stängt";
            return String.Format("{0} : {1:f1}°C : ({2:f1}°C) - {3}", on, InsideTemperature, TargetTemperature, open);
        }
    }
}
