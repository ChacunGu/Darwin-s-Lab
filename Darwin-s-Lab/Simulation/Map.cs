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
                return this.middleAreaRadiusPourcent * Map.GetMapSize() / 2;
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
                return Map.GetMapSize() / 2 - MiddleAreaRadius;
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
        static public int GetMapSize()
        {
            return 1000;
        }

        /// <summary>
        /// Returns the maps center point.
        /// </summary>
        /// <returns>center of the map</returns>
        static public Point GetCenter()
        {
            return new Point(GetMapSize() / 2, GetMapSize() / 2);
        }

        public Map(double safeZoneRadiusPourcent, Canvas canvas)
        {
            this.middleAreaRadiusPourcent = safeZoneRadiusPourcent;

            this.canvas = canvas;

            this.Width = MiddleAreaRadius*2;
            this.Height = MiddleAreaRadius*2;

            SolidColorBrush brush = new SolidColorBrush(Brushes.Green.Color);
            brush.Opacity = 0.6;

            CreateEllipse(brush);
            Ellipse.Stroke = null;
            Ellipse.MouseDown += Ellipse_MouseDown;

            Position = GetCenter();
            Move();
        }

        private void Ellipse_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Manager.SelectedCreature != null)
            {
                Manager.SelectedCreature.IsSelected = false;
                Manager.SelectedCreature.Ellipse.StrokeThickness = 1;
            }
            Manager.SelectedCreature = null;
        }

        /// <summary>
        /// Returns true if the given point is inside the map's danger zone false otherwise.
        /// </summary>
        /// <param name="point">Point to test</param>
        /// <returns>true if the given point is inside the map's danger zone false otherwise</returns>
        public bool IsPointInsideDangerZone(Point point)
        {
            return DistanceBetweenTwoPointsOpti(point, GetCenter()) < MiddleAreaRadius * MiddleAreaRadius;
        }

        /// <summary>
        /// Converts polar coordinates to cartesian coordinates.
        /// </summary>
        /// <param name="alpha">polar coordinates's angle</param>
        /// <param name="radius">polar coordinates's radius</param>
        /// <returns></returns>
        static public Point PolarToCartesian(double alpha, double radius)
        {
            return new Point(radius * Math.Cos(alpha) + Map.GetMapSize()/2, radius * Math.Sin(alpha) + Map.GetMapSize()/2);
        }

        /// <summary>
        /// Return the polar coordinates of the cartesian point given. (angle, radius)
        /// </summary>
        /// <param name="point">The point to convert</param>
        /// <returns>two doubles</returns>
        static public Tuple<double, double> CartesianToPolar(Point point)
        {
            Point center = new Point(Map.GetMapSize() / 2, Map.GetMapSize() / 2);
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
