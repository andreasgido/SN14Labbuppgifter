using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6NivaB
{
    public abstract class Solid : IComparable
    {

        // Fält
        private double _height;
        private double _radius;

        // Egenskaper
        public abstract double BaseArea { get; }

        public double Height
        {
            get { return _height; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Värdet på höjden måste vara större än noll");
                }
                _height = value;
            }
        }

        public double HeightSquared { get { return _height * _height; } }

        public double Radius
        {
            get { return _radius; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Värdet på radien måste vara större än noll");
                }
                _radius = value;
            }
        }

        public double RadiusSquared { get { return _radius * _radius; } }

        public abstract double SurfaceArea { get; }

        public abstract double Volume { get; }

        // konstruktor. Tilldelar fälten via egenskaperna
        protected Solid(double height, double radius)
        {
            Height = height;
            Radius = radius;
        }

        // Metoder

        // Metod som jämför två objekt med avseende på deras volymer
        public int CompareTo(object obj)
        {
            Solid other = obj as Solid;

            if (obj == null)    // Om parametern refererar till null
            {
                return 1;
            }
           
            if (other == null)  // Om inte parametern är ett objekt av typen Solid
            {
                throw new ArgumentNullException("Objektet är inte av typen Solid");
            }

            if (this.Volume > other.Volume)     // Om parameterns till ett objekts volym är större än det anropade objektets volym
            {
                return -1;
            }

            if (this.Volume < other.Volume)     // Om parameterns till ett objekts volym är mindre än det anropade objektets volym
            {
                return 1;
            }

            if (this.Volume == other.Volume)    // Om parameterns till ett objekts volym är lika med det anropade objektets volym
            {
                return 0;
            }

            return Volume.CompareTo(other.Volume);
        }

        // Metod som returnerar en sträng med
        public override string ToString()
        {
            return String.Format(" {0, -12} {1, 6:f1} {2, 8:f1} {3, 12:f2} {4, 11:f2} {5, 11:f2}", GetType().Name, Radius, Height, Volume, BaseArea, SurfaceArea);
        }
    }

    public enum SolidType
    {
        CircularCone,
        Cylinder
    }
}
