using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    public class StateReproduce : State
    {
        public StateReproduce()
        {
            Name = "Reproduce";
            Duration = 7500;
            FilterColor = new SolidColorBrush(Color.FromArgb(140, 0, 0, 128));
        }

        /// <summary>
        /// Mutates and reproduces creatures, removes dead corpes and rotten food from the map.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.Mutate();
            manager.StartCross();
        }

        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {
            manager.EndCreaturesMatingProcess();
        }

        /// <summary>
        /// Switch to state "grow food".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = GetNextState();
        }

        public override State GetNextState()
        {
            return new StateGrowFood();
        }
    }
}
