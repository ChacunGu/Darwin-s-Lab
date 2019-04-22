
namespace Darwin_s_Lab.Simulation
{
    public class StateHunt : State
    {
        public StateHunt()
        {
            Name = "Hunt";
            Duration = 1000000;
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
        /// Switch to state "back home".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.EndCreaturesHuntingProcess();
            manager.State = new StateBackHome();
        }
    }
}
