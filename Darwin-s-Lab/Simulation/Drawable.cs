using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Interface providing methods to display elements on canvas.
    /// </summary>
    abstract public class Drawable
    {
        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        void Update() { }

        public Point Position { get; set; }

        public Ellipse Ellipse { get; set; }

    }
}
