﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;
using System.Threading.Tasks;

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
            timer.Interval = new TimeSpan(0, 0, 0, 0, FramesPerSec);

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
            // initial state
            State.DoAction(this);

            // simulation's states loop
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Tick += new EventHandler((sender, e) => {
                SimulationStep();
                timer2.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            });
            timer2.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            timer2.Start();
        }

        /// <summary>
        /// Changes simulation's state and performs its action.
        /// </summary>
        private void SimulationStep()
        {
            State.GoNext(this);
            State.DoAction(this);
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

        /// <summary>
        /// Creates initial creatures population.
        /// </summary>
        public void CreateInitialPopulation()
        {
            for (int i = 0; i < CreatureNumber; i++)
            {
                Point rdmPosition = Map.PolarToCartesian(Tools.rdm.NextDouble() * Math.PI * 2,
                                                         Tools.rdm.NextDouble() * 10 + (map.MiddleAreaRadius / 2));
                creatures.Add(new Creature(canvas, map)
                              .WithPosition(rdmPosition)
                              .WithEnergy(2, null)
                              .WithSpeed(1, null)
                              .WithDetectionRange(2, null)
                              .WithForce(2, null)
                              .WithColorH(2, null)
                              .WithColorS(2, null)
                              .WithColorV(2, null));
            }
        }

        /// <summary>
        /// Removes old food from the map.
        /// </summary>
        public void RemoveRottenFood()
        {
            foreach (Food food in foods)
            {
                food.Destroy();
            }
            foods.Clear();
        }

        /// <summary>
        /// Generates new food on the map.
        /// </summary>
        public void GenerateFood()
        {
            for (int i = 0 ; i < FoodNumber ; i++)
            {
                foods.Add(new Food(canvas, map));
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
            for (int i = tmpMatingCreatures.Count - 1; i >= 1; i-=2)
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

            newbornCreatures = new List<Creature>();
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
            Console.WriteLine(".. ", dt);
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

        /// <summary>
        /// Adds newborn creatures to the simulation.
        /// </summary>
        internal void AddNewbornCreatures()
        {
            creatures.AddRange(newbornCreatures);
            newbornCreatures.Clear();
        }

        /// <summary>
        /// Ends creatures mating process and adds the newborns to the simulation.
        /// </summary>
        internal void EndCreaturesMatingProcess()
        {
            timer.Tick -= new EventHandler(CreaturesMatingProcess);
            AddNewbornCreatures();
        }
    }
}
