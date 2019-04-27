using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    public class StateBackHome : State
    {
        public StateBackHome()
        {
            Name = "BackHome";
            Duration = 10000;
            FilterColor = new SolidColorBrush(Color.FromArgb(70, 0, 0, 128));
        }
        
        /// <summary>
        /// Each creature tries to reach the safe zone before night.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            // pass
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
            manager.State = GetNextState();
        }

        public override State GetNextState()
        {
            return new StateReproduce();
        }
    }
}
