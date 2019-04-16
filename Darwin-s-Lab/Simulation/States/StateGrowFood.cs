using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    public class StateGrowFood : State
    {
        public override void DoAction(Manager manager)
        {
            //delete old food ?
            //generate new foods
            manager.GenerateFood();
        }

        public override void GoNext(Manager manager)
        {
            manager.State = new StateHunt();
        }
    }
}
