using System;
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
        private List<Creature> goingBackHomeCreatures;
        private List<Creature> newbornCreatures;
        private List<Food> foods;
        private Map map;
        private Canvas canvas;
        private State state;
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private DispatcherTimer timerState;
        private long dt;

        public Manager(Canvas canvas)
        {
            this.canvas = canvas;

            initManager();
        }
        
        private void initManager()
        {
            state = new StateInitial();
            map = new Map(0.8, canvas);

            foods = new List<Food>();
            creatures = new List<Creature>();

            FoodNumber = 20;
            CreatureNumber = 5;

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, FramesPerSec)
            };

            stopwatch = new Stopwatch();
            stopwatch.Start();

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
        /// returns the length of the creatures List
        /// </summary>
        /// <returns>the length of the creatures List</returns>
        public int CreaturesListCount()
        {
            return creatures.Count;
        }
        
        /// <summary>
        /// returns the length of the foods List
        /// </summary>
        /// <returns>the length of the foods List</returns>
        public int FoodsListCount()
        {
            return foods.Count;
        }

        /// <summary>
        /// Start the simulation
        /// </summary>
        public void StartSimulation()
        {
            // initial state
            State.DoAction(this);

            // simulation's states loop
            timerState = new DispatcherTimer();
            timerState.Tick += new EventHandler((sender, e) => {
                SimulationStep();
                timerState.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            });
            timerState.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            timerState.Start();
        }

        public bool IsPaused { get; set; } = false;
        

        public void Pause()
        {
            timer.Stop();
            timerState.Stop();
            IsPaused = true;
        }

        public void Resume()
        {
            timer.Start();
            timerState.Start();
            IsPaused = false;
        }
        
        /// <summary>
        /// Changes simulation's state and performs its action.
        /// </summary>
        private void SimulationStep()
        {
            State.StopAction(this);
            State.GoNext(this);
            State.DoAction(this);
        }

        /// <summary>
        /// Creates initial creatures population.
        /// </summary>
        public void CreateInitialPopulation()
        {
            for (int i = 0; i < CreatureNumber; i++)
            {
                Point rdmPosition = Map.PolarToCartesian(Tools.rdm.NextDouble() * Math.PI * 2,
                                                         map.HomeRadius / 2 + map.MiddleAreaRadius);
                creatures.Add(new Creature(canvas, map)
                              .WithPosition(rdmPosition)
                              .WithEnergy(null, null)
                              .WithSpeed(null, null)
                              .WithDetectionRange(null, null)
                              .WithForce(null, null)
                              .WithColorH(null, null)
                              .WithColorS(null, null)
                              .WithColorV(null, null));
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
        /// Generates new food on the map. Spead them along the map.
        /// </summary>
        public void GenerateFood()
        {
            while (foods.Count < FoodNumber)
            {
                Point newPosisition = Map.PolarToCartesian(
                    Tools.rdm.NextDouble() * Math.PI * 2,
                    Tools.rdm.NextDouble() * map.MiddleAreaRadius - 100 // 100 -> margin
                );
                bool pointOK = true;
                foreach(Food food in foods)
                {
                    if (Map.DistanceBetweenTwoPointsOpti(food.Position, newPosisition) < 1200)
                    {
                        pointOK = false;
                        break;
                    }
                }
                if (pointOK)
                {
                    foods.Add(new Food(canvas, map, newPosisition));
                }
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
        internal void StartCross()
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
            matingCreatures = new List<Creature>();

            // for each creature
            for (int i = tmpMatingCreatures.Count - 1; i >= 0; i--)
            {
                double smallestDistance = Double.MaxValue;
                int nearestCreatureIndex = -1;
            
                // search the nearest creature
                for (int j = i-1; j >= 0; j--)
                {
                    double distance = Map.DistanceBetweenTwoPointsOpti(tmpMatingCreatures[i].Position, tmpMatingCreatures[j].Position);
                    if (distance <= Creature.MinimalDistanceToSearchMate && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestCreatureIndex = j;
                    }
                }

                // define the nearest creature as the new mate
                if (nearestCreatureIndex != -1)
                {
                    tmpMatingCreatures[i].SetMate(tmpMatingCreatures[nearestCreatureIndex]);

                    matingCreatures.Add(tmpMatingCreatures[i]);
                    matingCreatures.Add(tmpMatingCreatures[nearestCreatureIndex]);

                    tmpMatingCreatures.RemoveAt(i);
                    tmpMatingCreatures.RemoveAt(i < nearestCreatureIndex ? nearestCreatureIndex-1 : nearestCreatureIndex);
                    i--;
                }
            }
            
            newbornCreatures = new List<Creature>();
            dt = stopwatch.ElapsedMilliseconds;
            timer.Tick += new EventHandler(CreaturesMatingProcess);
        }

        /// <summary>
        /// Performs the creatures mating process.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void CreaturesMatingProcess(object sender, EventArgs e)
        {
            for (int i = matingCreatures.Count - 1; i >= 0; i--)
            {
                Creature newborn = matingCreatures[i].MatingProcess(GetTimeElapsedInSeconds());
                if (newborn != null)
                {
                    // remove creatures from matingCreatures as they already reproduced
                    int indexOfMate = matingCreatures.IndexOf(matingCreatures[i].Mate);
                    matingCreatures[i].ForgetMate();
                    matingCreatures.RemoveAt(i);
                    matingCreatures.RemoveAt(i < indexOfMate ? indexOfMate-1 : indexOfMate);
                    i--;

                    // add the newborn to the creatures
                    newbornCreatures.Add(newborn);
                }
            }
            dt = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Returns time elapsed in seconds.
        /// </summary>
        /// <returns>time elapsed</returns>
        internal float GetTimeElapsedInSeconds()
        {
            float localDt = (stopwatch.ElapsedMilliseconds - dt) / 1000f;
            return localDt > 0.15f ? 0.15f : localDt;
        }

        /// <summary>
        /// Removes creatures whith not enough energy to survive.
        /// </summary>
        internal void RemoveDeadCreatures()
        {
            for (int i = creatures.Count - 1; i >= 0; i--)
            {
                if (!creatures[i].IsAlive() || map.IsPointInsideDangerZone(creatures[i].Position))
                {
                    creatures[i].Destroy();
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

            for (int i = 0; i < matingCreatures.Count; i++)
                matingCreatures[i].ForgetMate();
            matingCreatures.Clear();
        }

        /// <summary>
        /// Starts the creatures hunting process.
        /// </summary>
        internal void StartHunt()
        {
            dt = stopwatch.ElapsedMilliseconds;
            timer.Tick += new EventHandler(CreaturesHuntingProcess);
        }

        /// <summary>
        /// Performs the creatures hunting process.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void CreaturesHuntingProcess(object sender, EventArgs e)
        {
            for (int i=0; i<creatures.Count; i++)
            {
                double smallestDistance = Double.MaxValue;
                int nearestFoodIndex = -1;

                for (int j=0; j<foods.Count; j++)
                {
                    double distance = Map.DistanceBetweenTwoPointsOpti(creatures[i].Position, foods[j].Position);
                    if (distance <= Math.Pow(creatures[i].GetDetectionRange(), 2) && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestFoodIndex = j;
                    }
                }

                // define the nearest food as the new target
                if (nearestFoodIndex != -1)
                {
                    creatures[i].ForgetTarget();

                    Vector foodDirection = foods[nearestFoodIndex].Position - creatures[i].Position;
                    foodDirection.Normalize();
                    creatures[i].Direction = foodDirection;
                    
                    creatures[i].TakeStep(GetTimeElapsedInSeconds());

                    // check if food has been reached
                    if (Map.DistanceBetweenTwoPointsOpti(creatures[i].Position, foods[nearestFoodIndex].Position) <= Creature.MinimalDistanceToEat)
                    {
                        creatures[i].Eat(foods[nearestFoodIndex]);
                        foods[nearestFoodIndex].Destroy();
                        foods.RemoveAt(nearestFoodIndex);
                    }
                } else
                {
                    creatures[i].MoveToTarget(GetTimeElapsedInSeconds());
                }
            }
            dt = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Ends creatures hunting process and resets their target.
        /// </summary>
        public void EndCreaturesHuntingProcess()
        {
            timer.Tick -= new EventHandler(CreaturesHuntingProcess);
            for (int i = 0; i < creatures.Count; i++)
                creatures[i].ForgetTarget();
        }

        /// <summary>
        /// Starts the creatures back home process.
        /// </summary>
        internal void StartBackHome()
        {
            goingBackHomeCreatures = new List<Creature>(creatures);

            dt = stopwatch.ElapsedMilliseconds;
            timer.Tick += new EventHandler(CreaturesBackHomeProcess);
        }

        /// <summary>
        /// Performs the creatures back home process.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void CreaturesBackHomeProcess(object sender, EventArgs e)
        {
            for (int i = goingBackHomeCreatures.Count - 1; i >= 0; i--)
            {
                bool isBackHome = goingBackHomeCreatures[i].MoveToHome(GetTimeElapsedInSeconds());
                if (isBackHome)
                    goingBackHomeCreatures.RemoveAt(i);
            }
            dt = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Ends creatures back home process.
        /// </summary>
        internal void EndCreaturesBackHomeProcess()
        {
            timer.Tick -= new EventHandler(CreaturesBackHomeProcess);
            for (int i = 0; i < creatures.Count; i++)
                creatures[i].ForgetTarget();
            goingBackHomeCreatures.Clear();

            RemoveDeadCreatures();
            RemoveRottenFood();
        }
    }
}
