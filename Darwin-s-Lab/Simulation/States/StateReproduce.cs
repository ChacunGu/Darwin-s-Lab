using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateReproduce : State
    {
        public StateReproduce()
        {
            Name = "Reproduce";
            Duration = 10000;
        }

        /// <summary>
        /// Mutates and reproduces creatures, removes dead corpes and rotten food from the map.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.Mutate();
            manager.Cross();
            manager.RemoveDeadCreatures();
            manager.RemoveRottenFood();
        }

        /// <summary>
        /// Switch to state "grow food".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.EndCreaturesMatingProcess();
            manager.State = new StateGrowFood();
        }
    }
}
