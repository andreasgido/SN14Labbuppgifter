using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Labb6
{
    public class Cylinder : Solid
    {
        // Egenskaper
        public override double BaseArea 
        {
            get { return Math.PI * Radius * Radius; } 
        }

        public override double SurfaceArea 
        {
            get { return 2 * Math.PI * Radius * (Height + Radius); } 
        }

        public override double Volume 
        {
            get { return Math.PI * Radius * Radius * Height; } 
        }

        // Konstruktor. Kallar på basklassens konstruktor.
        public Cylinder(double radius, double height)
            :base(radius, height)
        {

        }
    }
}
