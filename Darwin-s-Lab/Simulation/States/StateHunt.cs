using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    class StateHunt : State
    {
        public override void DoAction()
        {
            throw new NotImplementedException();
        }

        public override void GoNext(Manager manager)
        {
            manager.State = new StateBackHome();
        }
    }
}
