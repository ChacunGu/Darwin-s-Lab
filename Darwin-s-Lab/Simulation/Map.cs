using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents simulation's map.
    /// </summary>
    public class Map : Drawable
    {
        private double safeZoneRadiusPourcent;
        public double SafeZoneRadius {
            get {
                return this.safeZoneRadiusPourcent * mapSize;
            }
            set {
                safeZoneRadiusPourcent = value;
            }
        }
        private const int mapSize = 1000;

        public Map(double safeZoneRadiusPourcent)
        {
            this.safeZoneRadiusPourcent = safeZoneRadiusPourcent;
        }

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        static public Point PolarToCartesian(double alpha, double radius)
        {
            return new Point(radius * Math.Cos(alpha), radius * Math.Sin(alpha));
        }

        static public double DistanceBetweenTwoPoints(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}
