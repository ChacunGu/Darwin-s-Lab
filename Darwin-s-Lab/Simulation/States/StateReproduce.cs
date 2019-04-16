using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateReproduce : State
    {
        public override void DoAction(Manager manager)
        {
            // mutation
            // crossing over
            // replace creatures
        }

        public override void GoNext(Manager manager)
        {
            manager.State = new StateGrowFood();
        }
    }
}
