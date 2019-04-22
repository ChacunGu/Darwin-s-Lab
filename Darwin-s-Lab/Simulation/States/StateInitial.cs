using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateInitial : State
    {
        public StateInitial()
        {
            Name = "Initial";
            Duration = 500;
        }

        /// <summary>
        /// Creates initial population.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.CreateInitialPopulation();
        }

        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {
            
        }

        /// <summary>
        /// Switch to state "grow food".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = new StateGrowFood();
        }
    }
}
