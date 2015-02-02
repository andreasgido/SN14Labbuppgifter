using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaB
{
    public class TemperatureSensor
    {
        // Fält.
        private decimal _temperature;

        // Egenskaper.
        public decimal Temperature 
        {
            get { return _temperature; }
            set     // private?? 
            {
                if (value < 0 || value > 45)
                {
                    string errorInsideTemp = "Innertemperaturen är inte i intervallet 0 - 45°C";
                    throw new ArgumentException(errorInsideTemp);
                }
                _temperature = value;
            }          
        }

        // Konstruktor.
        public TemperatureSensor(decimal temperature)
        {
            _temperature = temperature;
        }

        // Metod för att simulera 1 minuts körning av kylskåpet.
        public void Simulate(decimal targetTemperature, decimal outsideTemperature, bool IsOn, bool IsOpen)
        {
            decimal change = 0.0m;

            if (IsOn && IsOpen == false)
            {
                change = -0.2m;
            }
            else if (IsOn && IsOpen == true)
            {
                change += 0.2m;
            }
            else if (IsOn == false && IsOpen == false)
            {
                change += 0.1m;
            }
            else if (IsOn == false && IsOpen)
            {
                change += 0.5m;
            }
            // Kollar om temperaruten inne i kylskåpet går under börvärdet(TargetTemperature)
            if (_temperature + change < targetTemperature)
            {
                _temperature = targetTemperature;
            }
            // Kollar om temperaturen inne i kylskåpet går över rumstemperaturen(OutsideTemperature)
            else if (_temperature + change > outsideTemperature)
            {
                _temperature = outsideTemperature;
            }
            // Annars gör beräkningen.
            else
            {
                _temperature += change;
            }
        }
    }
}
