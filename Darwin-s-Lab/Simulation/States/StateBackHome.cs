using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateBackHome : State
    {
        public override void DoAction(Manager manager)
        {
            // make creatures go back home
        }

        public override void GoNext(Manager manager)
        {
            manager.State = new StateReproduce();
        }
    }
}
