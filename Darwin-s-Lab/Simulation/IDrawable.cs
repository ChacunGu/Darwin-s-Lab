using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Interface providing methods to display elements on canvas.
    /// </summary>
    interface IDrawable
    {
        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        void Update();
    }
}
