
namespace Darwin_s_Lab.Simulation
{
    public class StateGrowFood : State
    {
        /// <summary>
        /// Generates fresh food in the danger zone.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            //generate new foods
            manager.GenerateFood();
        }

        /// <summary>
        /// Switch to state "hunt".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = new StateHunt();
        }
    }
}
