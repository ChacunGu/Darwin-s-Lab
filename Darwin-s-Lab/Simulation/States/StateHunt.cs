﻿
namespace Darwin_s_Lab.Simulation
{
    public class StateHunt : State
    {
        public StateHunt()
        {
            Name = "Hunt";
            Duration = 75000;
        }

        /// <summary>
        /// Each creature starts searching food in the danger zone.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.StartHunt();
        }

        /// <summary>
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {
            manager.EndCreaturesHuntingProcess();
        }

        /// <summary>
        /// Switch to state "back home".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = new StateBackHome();
        }
    }
}
