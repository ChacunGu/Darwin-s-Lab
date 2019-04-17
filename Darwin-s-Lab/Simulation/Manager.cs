using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

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
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private long dt;

        public Manager(Canvas canvas)
        {
            this.canvas = canvas;
            this.state = new StateInitial();
            this.map = new Map(0.8, canvas);

            this.foods = new List<Food>();
            this.creatures = new List<Creature>();

            this.FoodNumber = 20;
            this.CreatureNumber = 10;

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(SimulationTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 17);

            stopwatch = new Stopwatch();
            stopwatch.Start();

            dt = stopwatch.ElapsedMilliseconds;

            timer.Start();
            
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
        public int FoodNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of creatures generated at start
        /// </summary>
        public int CreatureNumber { get; set; }

        /// <summary>
        /// Start the simulation
        /// </summary>
        public void StartSimulation()
        {
            // just for testing, simulate passing of states
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += new EventHandler((sender, e) => {
                SimulationStep();
            });
            timer2.Interval = new TimeSpan(0, 0, 0, 2);
            timer2.Start();
        }

        private void SimulationStep()
        {
            State.DoAction(this);
            State.GoNext(this);
        }
        
        private void SimulationLoop()
        {
            for (int i = 0 ; i < 10 ; i++)
            {
                SimulationStep();
            }
        }
        

        /// <summary>
        /// Simulate the passage of time
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="e">the arguments</param>
        private void SimulationTick(object sender, EventArgs e)
        {
            dt = stopwatch.ElapsedMilliseconds - dt;

            //Console.WriteLine(dt);

        }

        internal void GenerateFood()
        {
            foreach (Food food in foods)
            {
                food.Destroy();
            }
            foods.Clear();
            for (int i = 0 ; i < FoodNumber ; i++)
            {
                foods.Add(new Food(canvas, map));
            }
        }

        internal void CreateCreatures()
        {
            for (int i = 0; i < CreatureNumber; i++)
            {
                creatures.Add(new Creature(canvas, map));
            }
        }
    }
}
