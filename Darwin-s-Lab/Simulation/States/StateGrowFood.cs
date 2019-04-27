
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    public class StateGrowFood : State
    {
        public StateGrowFood()
        {
            Name = "GrowFood";
            Duration = 500;
            FilterColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 128));
        }

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
        /// Stop the execution of state's actions.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void StopAction(Manager manager)
        {

        }

        /// <summary>
        /// Switch to state "hunt".
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void GoNext(Manager manager)
        {
            manager.State = GetNextState();
        }

        public override State GetNextState()
        {
            return new StateHunt();
        }
    }
}
