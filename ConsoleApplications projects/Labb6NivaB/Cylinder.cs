using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6NivaB
{
    public class Cylinder : Solid
    {
        // Implentation av den abstrakta basklassen Solid:s egenskaper
        public override double BaseArea { get { return Math.PI * RadiusSquared; } }
        public override double SurfaceArea { get { return 2 * Math.PI * Radius * (Height + Radius); } }
        public override double Volume { get { return Math.PI * RadiusSquared * Height; } }

        // Konstruktor
        public Cylinder(double height, double radius)
            : base(height, radius)
        {

        }
    }
}
