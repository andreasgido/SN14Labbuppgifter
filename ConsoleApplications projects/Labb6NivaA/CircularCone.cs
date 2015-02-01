using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb6
{
    public class CircularCone : Solid
    {
        // Egenskaper.
        public override double BaseArea 
        {
            get { return Math.PI * RadiusSquared; }
        }

        public override double SurfaceArea 
        { 
            get { return Math.PI * Radius * (Radius + Math.Sqrt(RadiusSquared + HeightSquared)); }
        }

        public override double Volume 
        {
            get { return (1 / 3d) * Math.PI * RadiusSquared * Height; } 
        }

        // Konstruktor. Kallar på basklassens konstruktor.
        public CircularCone(double radius, double height)
            :base(radius, height)
        {
          
        }
    }
}
