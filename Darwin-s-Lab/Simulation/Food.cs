using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents simulation's food.
    /// </summary>
    public class Food : Drawable
    {
        public int Energy { get; set; }
        
        public Food(Canvas canvas, Map map)
        {

            Position = Map.PolarToCartesian(
                Tools.rdm.NextDouble() * Math.PI * 2,
                Tools.rdm.NextDouble() * map.middleAreaRadius/2
            );
            

            Ellipse = new Ellipse();
            Ellipse.Width = 30;
            Ellipse.Height = 30;

            Ellipse.Fill = Brushes.Red;
            
            canvas.Children.Add(Ellipse);
            Canvas.SetLeft(Ellipse, Position.X);
            Canvas.SetTop(Ellipse, Position.Y);
            

        }

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
