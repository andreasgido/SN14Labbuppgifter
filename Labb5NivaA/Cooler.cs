using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb5NivaA
{
    public class Cooler
    {
        // Fält.
        private decimal _insideTemperature;
        private decimal _targetTemperature;
        private const decimal OutsideTemperature = 23.7m;

        // Egenskaper.
        public bool IsOpen { get; set; }

        public decimal InsideTemperature 
        { 
            get { return _insideTemperature; }
            set
            {
                if ( value < 0 || value > 45)
                {
                    string errorInsideTemp = "Innertemperaturen är inte i intervallet 0 - 45°C";
                    throw new ArgumentException(errorInsideTemp);
                }
                _insideTemperature = value;               
            }
        }

        public bool IsOn { get; set; }

        public decimal TargetTemperature 
        { 
            get { return _targetTemperature; }
            set 
            {
                if ( value < 0 || value > 20 )
                {
                    string errorTargetTemp = "Måltemperaturen är inte i intervallet 0 - 20°C.";
                    throw new ArgumentException(errorTargetTemp);               
                }
                _targetTemperature = value;
            }
        }
        
        // Konstruktorer       
        public Cooler()
            : this (0m, 0m)                           
        {

        }

        public Cooler(decimal insideTemperature, decimal targetTemperature)
            :this(insideTemperature, targetTemperature, false, false)
        {

        }

        public Cooler(decimal insideTemperature, decimal targetTemperature, bool isOn, bool isOpen)
        {
            InsideTemperature = insideTemperature;
            TargetTemperature = targetTemperature;
            IsOn = isOn;
            IsOpen = isOpen;                              
        }

        // Metoden Tick(). 
        public void Tick()
        {
            decimal change = 0.0m;

            if (IsOn == true && IsOpen == false)
            {
                change = -0.2m;
            }
            else if (IsOn == true && IsOpen == true)
            {
                change += 0.2m;
            }
            else if (IsOn == false && IsOpen == false)
            {
                change += 0.1m;
            }
            else if (IsOn == false && IsOpen == true)
            {
                change += 0.5m;
            }

            // Kollar om temperaruten inne i kylskåpet går under börvärdet(TargetTemperature)
            if (InsideTemperature + change < TargetTemperature)
            {
                InsideTemperature = TargetTemperature;
            }
            // Kollar om temperaturen inne i kylskåpet går över rumstemperaturen(OutsideTemperature)
            else if (InsideTemperature + change > OutsideTemperature)
            {
                InsideTemperature = OutsideTemperature;
            }
            // Annars gör beräkningen.
            else
            {
                InsideTemperature += change;
            }
        }

        // Metoden ToString. Sätter ihop en sträng till metodanropet från Program-klassen där utskrift sker.
        public override string ToString()
        {
            string on = (IsOn == true) ? "[PÅ]" : "[AV]";
            string open = (IsOpen == true) ? "Öppet" : "Stängt";
            return String.Format("{0} : {1:f1}°C : ({2:f1}°C) - {3}", on, InsideTemperature, TargetTemperature, open);            
        }
    }
}
