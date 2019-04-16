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
        private double middleAreaRadiusPourcent;
        public double middleAreaRadius {
            get
            {
                return this.middleAreaRadiusPourcent * mapSize;
            }
            set
            {
                middleAreaRadiusPourcent = value;
            }
        }
        public double HomeRadius
        {
            get
            {
                return mapSize - middleAreaRadius;
            }
            set
            {
                middleAreaRadiusPourcent = 1 - value;
            }
        }
        private const int mapSize = 1000;

        public Map(double safeZoneRadiusPourcent)
        {
            this.middleAreaRadiusPourcent = safeZoneRadiusPourcent;
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
            return new Point(radius * Math.Cos(alpha) + 500, radius * Math.Sin(alpha) + 500);
        }

        static public double DistanceBetweenTwoPoints(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}
