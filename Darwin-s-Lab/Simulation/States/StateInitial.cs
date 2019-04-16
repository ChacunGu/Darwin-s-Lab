using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateInitial : State
    {
        public override void DoAction(Manager manager)
        {
            //Create initial creatures
            manager.CreateCreatures();
        }

        public override void GoNext(Manager manager)
        {
            manager.State = new StateGrowFood();
        }
    }
}
