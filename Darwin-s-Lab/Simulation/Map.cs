using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                return this.middleAreaRadiusPourcent * Map.getMapSize() / 2;
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
                return Map.getMapSize() / 2 - MiddleAreaRadius;
            }
            set
            {
                middleAreaRadiusPourcent = 1 - value;
            }
        }

        /// <summary>
        /// static access to the size of the map
        /// </summary>
        /// <returns>the size of the canvas/map</returns>
        static public int getMapSize()
        {
            return 1000;
        }

        public Map(double safeZoneRadiusPourcent, Canvas canvas)
        {
            this.middleAreaRadiusPourcent = safeZoneRadiusPourcent;

            this.canvas = canvas;

            this.Width = MiddleAreaRadius*2;
            this.Height = MiddleAreaRadius*2;

            CreateEllipse(Brushes.Green);
            Position = new Point(Map.getMapSize() / 2, Map.getMapSize() / 2);
            Move();
        }

        /// <summary>
        /// Converts polar coordinates to cartesian coordinates.
        /// </summary>
        /// <param name="alpha">polar coordinates's angle</param>
        /// <param name="radius">polar coordinates's radius</param>
        /// <returns></returns>
        static public Point PolarToCartesian(double alpha, double radius)
        {
            return new Point(radius * Math.Cos(alpha) + Map.getMapSize()/2, radius * Math.Sin(alpha) + Map.getMapSize()/2);
        }

        /// <summary>
        /// Return the polar coordinates of the cartesian point given. (angle, radius)
        /// </summary>
        /// <param name="point">The point to convert</param>
        /// <returns>two doubles</returns>
        static public Tuple<double, double> CartesianToPolar(Point point)
        {
            Point center = new Point(Map.getMapSize() / 2, Map.getMapSize() / 2);
            double alpha = Math.Atan2(point.Y-center.Y, point.X-center.X);
            double radius = Map.DistanceBetweenTwoPoints(center, point);
            
            return new Tuple<double, double>(alpha, radius);
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
