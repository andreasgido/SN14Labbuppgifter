using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6
{
    public abstract class Solid
    {
        // Fält.
        private double _height;
        private double _radius;

        // Egenskaper.
        public abstract double BaseArea { get; }

        public double Height 
        {
            get { return _height; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                _height = value;
            }           
        }

        public double HeightSquared
        {
            get { return _height * _height; }          
        }

        public double Radius 
        {
            get { return _radius; }  
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                _radius = value;
            }
        }

        public double RadiusSquared
        {
            get { return _radius * _radius; }

        }

        public abstract double SurfaceArea { get; }

        public abstract double Volume { get; }

        // Konstruktor.
        protected Solid(double radius, double height)
        {
            Radius = radius;
            Height = height;
        }

        // Metod som överskuggar klassen Objects ToString metod.
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Radie (r) : {0, 25:f2}\n", Radius);
            sb.AppendFormat("Höjd (h)  : {0, 25:f2}\n", Height);
            sb.AppendFormat("Volym     : {0, 25:f2}\n", Volume);
            sb.AppendFormat("Basarea   : {0, 25:f2}\n", BaseArea);
            sb.AppendFormat("Ytarea    : {0, 25:f2}", SurfaceArea);

            return sb.ToString();
            // return String.Format(" Radie (r) : {0, 25:f2}\n Höjd (h)  : {1, 25:f2}\n Volym     : {2, 25:f2}\n Basarea   :{3, 26:f2}\n Ytarea    : {4, 25:f2}", Radius, Height, Volume, BaseArea, SurfaceArea);
        } 
    }
}
