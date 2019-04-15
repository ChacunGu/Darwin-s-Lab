using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Class managing the simulation. Calls draw on the Objects
    /// </summary>
    public class Manager
    {
        private List<Creature> creatures;
        private List<Food> foods;
        private Map map;
        private Canvas canvas;
        private State state;

        private int foodNumber;

        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private long dt;

        public Manager(State state, Canvas canvas)
        {
            this.canvas = canvas;
            this.state = state;

            this.FoodNumber = 20;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(SimulationTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 17);

            stopwatch = new Stopwatch();
            stopwatch.Start();

            dt = stopwatch.ElapsedMilliseconds;
        }
        
        /// <summary>
        /// Gets or sets the state 
        /// </summary>
        public State State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                Console.WriteLine("Manager's State: " + state.GetType().Name);
            }
        }

        /// <summary>
        /// Gets or sets the number of food generated at start
        /// </summary>
        public int FoodNumber
        {
            get
            {
                return foodNumber;
            }
            set
            {
                foodNumber = value;
            }
        }

        /// <summary>
        /// Start the simulation
        /// </summary>
        public void StartSimulation()
        {
            

        }

        private void SimulationLoop()
        {
            State.DoAction(this);
            State.GoNext(this);
        }

        /// <summary>
        /// Simulate the passage of time
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the arguments</param>
        private void SimulationTick(object sender, EventArgs e)
        {
            dt = stopwatch.ElapsedMilliseconds - dt;
            


        }

        public void generateFood()
        {
            for (int i = 0 ; i < FoodNumber ; i++)
            {
                foods.Add(new Food(map));
            }
        }
    }
}
