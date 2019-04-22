using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateBackHome : State
    {
        public StateBackHome()
        {
            Name = "BackHome";
            Duration = 10000;
        }
        
        /// <summary>
        /// Each creature tries to reach the safe zone before night.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            // make creatures go back home
            manager.StartBackHome();
        }

        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {
            manager.EndCreaturesBackHomeProcess();
        }

        /// <summary>
        /// Switch to state "reproduce".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = new StateReproduce();
        }
    }
}
