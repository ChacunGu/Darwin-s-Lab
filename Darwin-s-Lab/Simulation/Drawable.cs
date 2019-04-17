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
    /// Interface providing methods to display elements on canvas.
    /// </summary>
    abstract public class Drawable
    {
        protected Canvas canvas;

        protected double width = 30;
        protected double height = 30;

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        void Update() { }

        public Point Position { get; set; }

        public Ellipse Ellipse { get; set; }

        public void CreateEllipse(SolidColorBrush color)
        {
            Ellipse = new Ellipse();
            Ellipse.Width = width;
            Ellipse.Height = height;

            Ellipse.Fill = color;

            canvas.Children.Add(Ellipse);
        }

        /// <summary>
        /// Move the Elipse the the new Position
        /// at the center of the Ellipse
        /// </summary>
        public void Move()
        {
            Canvas.SetLeft(Ellipse, Position.X - width / 2);
            Canvas.SetTop(Ellipse, Position.Y - height / 2);
        }

        /// <summary>
        /// Take the Ellipse off the canvas
        /// </summary>
        public void Destroy()
        {
            canvas.Children.Remove(Ellipse);
        }
    }
}
