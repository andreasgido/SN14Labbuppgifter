using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaB
{
    public class TemperatureDisplay
    {
        // Fält.       
        private decimal _targetTemperature;
        private const decimal OutsideTemperature = 23.7m;

        private TemperatureSensor _insideTemperatureSensor;
        private DoorSensor _doorSensor;
        private ButtonSensor _buttonSensor;


        // Egenskaper.
        public bool DoorIsOpen { get { return _doorSensor.DoorIsOpen; } }
        
        public decimal InsideTemperature { get { return _insideTemperatureSensor.Temperature; } }

        public bool IsOn { get { return _buttonSensor.IsOn; } }

        public decimal TargetTemperature 
        { 
            get { return _targetTemperature; }
            set
            {
                {
                    if (value < 0 || value > 20)
                    {
                        string errorTargetTemp = "Måltemperaturen är inte i intervallet 0 - 20°C";
                        throw new ArgumentException(errorTargetTemp);
                    }
                    _targetTemperature = value;
                }
            }
        }

        // Konstruktor.
        public TemperatureDisplay(decimal insideTemperature, decimal targetTemperature, bool isOn, bool isOpen)
        {
            _insideTemperatureSensor = new TemperatureSensor(insideTemperature);
            TargetTemperature = targetTemperature;
            _buttonSensor = new ButtonSensor(isOn);
            _doorSensor = new DoorSensor(isOpen);             
        }

        // Metoder.

        // Metod som kollar om måltemperaturen i kylskåpet är nådd.
        public bool Tick()
        {
            _insideTemperatureSensor.Simulate(TargetTemperature, OutsideTemperature, IsOn, DoorIsOpen);
            return (InsideTemperature == TargetTemperature) ? true : false;
        }

        // Metod som returnerar en sträng.
        public override string ToString()
        {
            string on = IsOn ? "[PÅ]" : "[AV]";
            string open = DoorIsOpen ? "Öppet" : "Stängt";
            return String.Format("{0} : {1:f1}°C : ({2:f1}°C) - {3}", on, InsideTemperature, TargetTemperature, open);
        }
        
    }
}
