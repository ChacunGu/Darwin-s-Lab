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
                return this.safeZoneRadiusPourcent * MapSize;
            }
            set {
                safeZoneRadiusPourcent = value;
            }
        }
        public static int MapSize = 1000;

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

        /// <summary>
        /// Computes the euclidean distance between two points.
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance between the two points</returns>
        static public double DistanceBetweenTwoPoints(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        /// <summary>
        /// Computes the euclidean distance between two points (without computing the square root).
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance between the two points</returns>
        static public double DistanceBetweenTwoPointsOpti(Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
        }
    }
}
