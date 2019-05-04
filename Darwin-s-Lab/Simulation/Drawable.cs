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

        protected double Width { get; set; } = 30;
        protected double Height { get; set; } = 30;

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        void Update() { }

        public Point Position { get; set; }

        public Ellipse Ellipse { get; set; }

        /// <summary>
        /// Creates an ellipse with given color and adds it to the canvas children.
        /// </summary>
        /// <param name="color">ellipse's color</param>
        public void CreateEllipse(SolidColorBrush color)
        {
            Ellipse = new Ellipse
            {
                Width = this.Width,
                Height = this.Height,

                Fill = color,
                Stroke = new SolidColorBrush(Tools.ChangeColorBrightness(color.Color, -0.5f))
            };

            canvas.Children.Add(Ellipse);
        }

        /// <summary>
        /// Move the Elipse the the new Position
        /// at the center of the Ellipse
        /// </summary>
        public void Move()
        {
            Canvas.SetLeft(Ellipse, Position.X - Ellipse.Width / 2);
            Canvas.SetTop(Ellipse, Position.Y - Ellipse.Height / 2);
        }

        /// <summary>
        /// Take the Ellipse off the canvas
        /// </summary>
        public void Destroy()
        {
            canvas.Children.Remove(Ellipse);
        }

        /// <summary>
        /// Detect if 2 items intersects each other
        /// </summary>
        /// <param name="other">An other Drawable object</param>
        /// <returns>true if they collide</returns>
        public bool CollideWith(Drawable other)
        {
            return 
                Map.DistanceBetweenTwoPointsOpti(Position, other.Position)
                < 
                (Ellipse.Width / 2 + other.Ellipse.Width) / 2 *
                (Ellipse.Width / 2 + other.Ellipse.Width / 2);
        }
    }
}
