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
        public static int FramesPerSec = 17;
        private List<Creature> creatures;
        private List<Creature> matingCreatures;
        private List<Creature> newbornCreatures;
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
            timer.Interval = new TimeSpan(0, 0, 0, 0, FramesPerSec);

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
            for (int i = 0 ; i < FoodNumber; i++)
            {
                foods.Add(new Food(map));
            }
        }

        /// <summary>
        /// Generates a random number for each creature and mutates it if needed.
        /// </summary>
        internal void Mutate()
        {
            for (int i = 0; i < creatures.Count; i++)
            {
                if (creatures[i].CanMutate())
                {
                    creatures[i].Mutate();
                }
            }
        }

        /// <summary>
        /// Reproduces creatures sufficiently fed and close enough to each other.
        /// </summary>
        internal void Cross()
        {
            List<Creature> tmpMatingCreatures = new List<Creature>();

            // search creature capable of mating
            for (int i = 0; i < creatures.Count; i++)
            {
                if (creatures[i].CanMate())
                {
                    tmpMatingCreatures.Add(creatures[i]);
                }
            }

            Tools.Shuffle(tmpMatingCreatures);
            matingCreatures = new List<Creature>(tmpMatingCreatures);

            // for each creature
            for (int i = tmpMatingCreatures.Count - 1; i >= 1; i--)
            {
                double smallestDistance = Map.DistanceBetweenTwoPointsOpti(tmpMatingCreatures[i].Position, tmpMatingCreatures[i-1].Position);
                int nearestCreatureIndex = i-1;
            
                // search the nearest creature
                for (int j = i-2; j >= 0; j--)
                {
                    double distance = Map.DistanceBetweenTwoPointsOpti(tmpMatingCreatures[i].Position, tmpMatingCreatures[j].Position);
                    if (distance <= Creature.MinimalDistanceToSearchMate && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestCreatureIndex = j;
                    }
                }

                // define the nearest creature as the new mate
                tmpMatingCreatures[i].SetMate(tmpMatingCreatures[nearestCreatureIndex]);
                tmpMatingCreatures.RemoveAt(i);
                tmpMatingCreatures.RemoveAt(i < nearestCreatureIndex ? nearestCreatureIndex-1 : nearestCreatureIndex);
            }

            timer.Tick += new EventHandler(CreaturesMatingProcess);
        }

        /// <summary>
        /// Performs the creatures mating process.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void CreaturesMatingProcess(object sender, EventArgs e)
        {
            dt = stopwatch.ElapsedMilliseconds - dt;
            for (int i = matingCreatures.Count - 1; i >= 0; i--)
            {
                Creature newborn = matingCreatures[i].MatingProcess(dt, map);
                if (newborn != null)
                {
                    // remove creatures from matingCreatures as they already reproduced
                    int indexOfMate = matingCreatures.IndexOf(matingCreatures[i].Mate);
                    matingCreatures.RemoveAt(i);
                    matingCreatures.RemoveAt(i < indexOfMate ? indexOfMate-1 : indexOfMate);

                    // add the newborn to the creatures
                    newbornCreatures.Add(newborn);
                }
            }
        }

        /// <summary>
        /// Removes creatures whith not enough energy to survive.
        /// </summary>
        internal void RemoveDeadCreatures()
        {
            for (int i = creatures.Count - 1; i >= 0; i--)
            {
                if (creatures[i].IsAlive())
                {
                    creatures[i].Kill();
                    creatures.RemoveAt(i);
                }
            }
        }
    }
}
