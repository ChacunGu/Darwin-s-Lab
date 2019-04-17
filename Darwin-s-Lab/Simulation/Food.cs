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
