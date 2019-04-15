using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents simulation's food.
    /// </summary>
    public class Food : Drawable
    {
        public int Energy { get; set; }
        private Random rnd;
        public Food(Map map)
        {
            Random rnd = new Random();

            this.position = Map.PolarToCartesian(
                rnd.NextDouble() * Math.PI * 2,
                rnd.NextDouble() * map.SafeZoneRadius
                );
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
