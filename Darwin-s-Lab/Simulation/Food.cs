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
        public int Energy { get; set; }
        
        public Food(Canvas canvas, Map map)
        {
            this.canvas = canvas;

            Position = Map.PolarToCartesian(
                Tools.rdm.NextDouble() * Math.PI * 2,
                Tools.rdm.NextDouble() * map.MiddleAreaRadius/2 - 100 // 100 -> margin
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
