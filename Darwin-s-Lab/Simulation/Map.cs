using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents simulation's map.
    /// </summary>
    public class Map : Drawable
    {
        private double middleAreaRadiusPourcent;
        public double MiddleAreaRadius {
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
                return mapSize - MiddleAreaRadius;
            }
            set
            {
                middleAreaRadiusPourcent = 1 - value;
            }
        }
        private const int mapSize = 1000;

        public Map(double safeZoneRadiusPourcent, Canvas canvas)
        {
            this.middleAreaRadiusPourcent = safeZoneRadiusPourcent;

            this.canvas = canvas;

            this.Width = MiddleAreaRadius;
            this.Height = MiddleAreaRadius;

            CreateEllipse(Brushes.Green);
            Position = new Point(mapSize / 2, mapSize / 2);
            Move();
        }

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts polar coordinates to cartesian coordinates.
        /// </summary>
        /// <param name="alpha">polar coordinates's angle</param>
        /// <param name="radius">polar coordinates's radius</param>
        /// <returns></returns>
        static public Point PolarToCartesian(double alpha, double radius)
        {
            return new Point(radius * Math.Cos(alpha) + 500, radius * Math.Sin(alpha) + 500);
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
