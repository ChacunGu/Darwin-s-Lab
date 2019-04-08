using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Interface providing methods to display elements on canvas.
    /// </summary>
    abstract class Drawable
    {

        protected int diameter;

        protected Point position;
        protected Color color;
        protected Ellipse ellipse;


        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        void Update()
        {

        }
    }
}
