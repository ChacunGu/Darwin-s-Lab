
using System.Windows.Media;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents the State where the creatures go hunting for food
    /// </summary>
    public class StateHunt : State
    {
        public StateHunt()
        {
            Name = "Hunt";
            Duration = 15000;
            FilterColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 128));
        }

        /// <summary>
        /// Each creature starts searching food in the danger zone.
        /// </summary>
        /// <param name="manager">simulation's manager</param>
        public override void DoAction(Manager manager)
        {
            manager.StartHunt();
            manager.StartBackHome();
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
            manager.State = GetNextState();
        }

        public override State GetNextState()
        {
            return new StateBackHome();
        }
    }
}
