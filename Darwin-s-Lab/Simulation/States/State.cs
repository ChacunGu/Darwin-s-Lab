using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Abstract class that represents the states on the simulation
    /// </summary>
    abstract public class State
    {
        public String Name { get; set; }
        public int Duration { get; set; }
        public SolidColorBrush FilterColor { get; set; }

        /// <summary>
        /// Changes to the next State
        /// </summary>
        /// <param name="manager">the manager that need to change state</param>
        public abstract void GoNext(Manager manager);


        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public abstract void StopAction(Manager manager);

        /// <summary>
        /// Performs state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public abstract void DoAction(Manager manager);

        /// <summary>
        /// Return the next state
        /// </summary>
        public abstract State GetNextState();
    }
}
