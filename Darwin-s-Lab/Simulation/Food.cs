using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents simulation's food.
    /// </summary>
    public class Food : Drawable
    {
        public double Energy { get; set; } = 0.3;
        
        public Food(Canvas canvas, Map map)
        {
            this.canvas = canvas;

            Position = Map.PolarToCartesian(
                Tools.rdm.NextDouble() * Math.PI * 2,
                Tools.rdm.NextDouble() * map.MiddleAreaRadius/2
            );

            CreateEllipse(Brushes.Red);
            
            Move();
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
