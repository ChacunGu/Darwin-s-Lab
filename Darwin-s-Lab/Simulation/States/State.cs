using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Enum that represents the states on the simulation
    /// </summary>
    abstract public class State
    {
        /// <summary>
        /// Changes to the next State
        /// </summary>
        /// <param name="manager">the manager that need to change state</param>
        public abstract void GoNext(Manager manager);

        public abstract void DoAction(Manager manager);
    }
}
