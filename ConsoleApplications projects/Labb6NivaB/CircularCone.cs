using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb6NivaB
{
    public class CircularCone : Solid
    {
        // Implentation av den abstrakta basklassen Solid:s egenskaper
        public override double BaseArea { get { return Math.PI * RadiusSquared; } }
        public override double SurfaceArea { get { return Math.PI * Radius * ( Radius + Math.Sqrt(RadiusSquared + HeightSquared) ); } }
        public override double Volume { get { return (1 / 3d) * Math.PI * RadiusSquared * Height; } }

        // Konstruktor
        public CircularCone(double height, double radius)
            : base(height, radius)
        {

        }
    }
}
