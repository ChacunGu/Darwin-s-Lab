using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    public class StateMutate : State
    {
        public StateMutate()
        {
            Name = "Mutate";
            Duration = 2000;
            FilterColor = new SolidColorBrush(Color.FromArgb(140, 0, 0, 128));
        }

        /// <summary>
        /// Mutates the creatures.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.Mutate();
        }

        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {
            // pass
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
