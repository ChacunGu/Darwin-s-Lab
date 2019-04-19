
namespace Darwin_s_Lab.Simulation
{
    public class StateHunt : State
    {
        public StateHunt()
        {
            Name = "Hunt";
            Duration = 2000;
        }

        /// <summary>
        /// Each creature starts searching food in the danger zone.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            //make creatures moves toward food
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
