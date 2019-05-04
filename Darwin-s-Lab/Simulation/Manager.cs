using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Class managing the simulation. Calls draw on the Objects
    /// </summary>
    public class Manager
    {
        public static int FramesPerSec = 17;
        public static Creature SelectedCreature { get; set; } = null;
        private List<Creature> creatures;
        private List<Creature> matingCreatures;
        private List<Creature> huntingCreatures;
        private List<Creature> goingBackHomeCreatures;
        private List<Creature> newbornCreatures;
        private List<Creature> animationBirth;
        private List<Creature> animationDeath;
        private List<Creature> animationMutate;
        private List<Food> foods;
        private Map map;
        private Canvas canvas;
        private State state;
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private DispatcherTimer timerState;
        private EventHandler stateHandler;
        private long dt;

        private MainWindow mainWindow;

        #region simulation controls & parameters
        public Manager(Canvas canvas, MainWindow mainWindow)
        {
            this.canvas = canvas;
            this.mainWindow = mainWindow;

            FoodNumber = 200;
            CreatureNumber = 100;

            InitManager();
        }

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        private void InitManager()
        {
            SelectedCreature = null;

            mainWindow.UpdateCreatureInfo();

            IsSimulating = false;
            IsPaused = false;

            state = new StateInitial();
            map = new Map(0.8, canvas);

            foods = new List<Food>();
            creatures = new List<Creature>();
            newbornCreatures = new List<Creature>();
            animationBirth = new List<Creature>();
            animationDeath = new List<Creature>();
            animationMutate = new List<Creature>();

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, FramesPerSec)
            };

            stopwatch = new Stopwatch();
            stopwatch.Start();

            dt = stopwatch.ElapsedMilliseconds;
            timer.Start();
            timer.Tick += new EventHandler(Animate);
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
            }
        }

        /// <summary>
        /// Remember the time of the start of a State
        /// </summary>
        private long StateStartTime { get; set; }

        /// <summary>
        /// Get the state's progression in pourcent, between 0 and 1
        /// </summary>
        /// <returns>the state's progression between 0 and 1</returns>
        public double GetStateProgression()
        {
            return GetStateElapsedTime() / (double)State.Duration;
        }

        /// <summary>
        /// Get the current State's Elapsed time in milliseconds
        /// </summary>
        /// <returns>the current State's Elapsed time in milliseconds</returns>
        public long GetStateElapsedTime()
        {
            return stopwatch.ElapsedMilliseconds - StateStartTime;
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
        /// Gets or sets the simulation pause status.
        /// </summary>
        public bool IsPaused { get; set; } = false;

        /// <summary>
        /// check if a Simulation is ongoing.
        /// </summary>
        public bool IsSimulating { get; set; } = false;
        
        /// <summary>
        /// returns the length of the creatures List
        /// </summary>
        /// <returns>the length of the creatures List</returns>
        public int GetNumberOfCreatures()
        {
            return creatures.Count + newbornCreatures.Count;
        }
        
        /// <summary>
        /// returns the length of the foods List
        /// </summary>
        /// <returns>the length of the foods List</returns>
        public int GetNumberOfFoods()
        {
            return foods.Count;
        }

        /// <summary>
        /// Start the simulation
        /// </summary>
        public void StartSimulation()
        {
            IsSimulating = true;
            // initial state
            State.DoAction(this);

            // simulation's states loop
            timerState = new DispatcherTimer();
            stateHandler = new EventHandler((sender, e) => {
                SimulationStep();
                timerState.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            });
            timerState.Tick += stateHandler;
            timerState.Interval = new TimeSpan(0, 0, 0, 0, State.Duration);
            timerState.Start();

            AddEventHandlerToTimer(new EventHandler(mainWindow.Update));

        }
        
        /// <summary>
        /// Select a new Creature
        /// </summary>
        /// <param name="creature"></param>
        public void SelectCreature(Creature creature)
        {
            if (SelectedCreature != null)
            {
                SelectedCreature.IsSelected = false;
            }
            SelectedCreature = creature;
            SelectedCreature.Ellipse.StrokeThickness = 10;
            creature.IsSelected = true;
        }
        
        /// <summary>
        /// Pause the simulation.
        /// </summary>
        public void Pause()
        {
            stopwatch.Stop();
            timer.Stop();
            timerState.Stop();
            IsPaused = true;
        }

        /// <summary>
        /// Resume the simulation.
        /// </summary>
        public void Resume()
        {
            stopwatch.Start();
            timer.Start();
            timerState.Interval =  new TimeSpan(0, 0, 0, 0, State.Duration - (int)GetStateElapsedTime());
            timerState.Start();
            IsPaused = false;
        }
        
        /// <summary>
        /// Resets everthing
        /// </summary>
        public void Reset()
        {
            Pause();
            foreach (Creature creature in creatures) {
                creature.Destroy();
            }
            foreach (Food food in foods)
            {
                food.Destroy();
            }
            if (newbornCreatures != null)
            {
                foreach (Creature creature in newbornCreatures)
                {
                    creature.Destroy();
                }
            }

            map.Destroy();

            InitManager();
        }

        /// <summary>
        /// Changes simulation's state and performs its action.
        /// </summary>
        private void SimulationStep()
        {
            mainWindow.SimulationStateChanged();
            State.StopAction(this);
            State.GoNext(this);
            StateStartTime = stopwatch.ElapsedMilliseconds;
            State.DoAction(this);
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
        /// Performs simulation's animations.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void Animate(object sender, EventArgs e)
        {
            // birth animation
            for (int i=animationBirth.Count - 1; i>=0; i--)
            {
                if (animationBirth[i].Grow())
                {
                    animationBirth.RemoveAt(i);
                }
            }

            // death animation
            for (int i = animationDeath.Count - 1; i >= 0; i--)
            {
                if (animationDeath[i].Shrink())
                {
                    animationDeath[i].Destroy();
                    animationDeath.RemoveAt(i);
                }
            }

            // mutate animation
            for (int i = animationMutate.Count - 1; i >= 0; i--)
            {
                if (animationMutate[i].Blink())
                {
                    animationMutate.RemoveAt(i);
                }
            }

            dt = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Adds a new event handler to the simulation timer.
        /// </summary>
        /// <param name="eventHandler">new event handler to add</param>
        private void AddEventHandlerToTimer(EventHandler eventHandler)
        {
            timer.Tick -= new EventHandler(Animate);
            timer.Tick += eventHandler;
            timer.Tick += new EventHandler(Animate);
        }

        /// <summary>
        /// Removes an event handler from the simulation timer.
        /// </summary>
        /// <param name="eventHandler">event handler to remove</param>
        private void RemoveEventHandlerFromTimer(EventHandler eventHandler)
        {
            timer.Tick -= eventHandler;
        }
        #endregion

        #region population & food controls
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
        /// Removes creatures whith not enough energy to survive.
        /// </summary>
        internal void RemoveDeadCreatures()
        {
            for (int i = creatures.Count - 1; i >= 0; i--)
            {
                if (!creatures[i].IsAlive() || map.IsPointInsideDangerZone(creatures[i].Position))
                {
                    if (creatures[i].IsSelected)
                    {
                        SelectedCreature = null;
                        mainWindow.UpdateCreatureInfo();
                    }
                    animationDeath.Add(creatures[i]);
                    //creatures[i].Destroy();
                    creatures.RemoveAt(i);
                }
            }

            if (GetNumberOfCreatures() <= 0)
            {
                // TODO display message in MainWindow
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
                    Math.Sqrt(Tools.rdm.NextDouble()) * (map.MiddleAreaRadius - 50) // 50 -> margin
                );
                foods.Add(new Food(canvas, map, newPosisition));
            }
        }
        #endregion

        #region state reproduce
        /// <summary>
        /// Generates a random number for each creature and mutates it if needed.
        /// </summary>
        internal void Mutate()
        {
            for (int i = 0; i < creatures.Count; i++)
            {
                if (creatures[i].CanMutate())
                {
                    bool didMutate = creatures[i].Mutate();

                    if (didMutate)
                    {
                        animationMutate.Add(creatures[i]);
                    }
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
                    if (distance <= Properties.MinimalDistanceToSearchMate && distance < smallestDistance)
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
            AddEventHandlerToTimer(new EventHandler(CreaturesMatingProcess));
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
                    if (GetNumberOfCreatures() < Properties.MaximumNumberCreatures)
                    {
                        newbornCreatures.Add(newborn);
                        animationBirth.Add(newborn);
                    } else
                    {
                        newborn.Destroy();
                    }
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
            RemoveEventHandlerFromTimer(new EventHandler(CreaturesMatingProcess));
            AddNewbornCreatures();

            for (int i = 0; i < matingCreatures.Count; i++)
                matingCreatures[i].ForgetMate();
            matingCreatures.Clear();
        }
        #endregion

        #region state hunt
        /// <summary>
        /// Starts the creatures hunting process.
        /// </summary>
        internal void StartHunt()
        {
            huntingCreatures = new List<Creature>(creatures);
            
            AddEventHandlerToTimer(new EventHandler(CreaturesHuntingProcess));
        }

        /// <summary>
        /// Performs the creatures hunting process.
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        private void CreaturesHuntingProcess(object sender, EventArgs e)
        {
            for (int i=huntingCreatures.Count - 1; i>=0; i--)
            {
                double smallestDistance = Double.MaxValue;
                int nearestFoodIndex = -1;

                // search for the nearest food
                for (int j=0; j<foods.Count; j++)
                {
                    double distance = Map.DistanceBetweenTwoPointsOpti(huntingCreatures[i].Position, foods[j].Position);
                    if (distance <= Math.Pow(huntingCreatures[i].GetDetectionRange(), 2) && distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        nearestFoodIndex = j;
                    }
                }

                // define the nearest food as the new target
                if (nearestFoodIndex != -1)
                {
                    bool foodEaten = huntingCreatures[i].MoveToFood(foods[nearestFoodIndex], GetTimeElapsedInSeconds());
                    if (foodEaten)
                    {
                        foods.RemoveAt(nearestFoodIndex);

                        if (huntingCreatures[i].HasEatenEnough(GetStateProgression()))
                        {
                            goingBackHomeCreatures.Add(huntingCreatures[i]);
                            huntingCreatures.RemoveAt(i);
                        }
                    }
                } else
                {
                    huntingCreatures[i].MoveToHuntingZone(GetTimeElapsedInSeconds());
                }
            }
        }

        /// <summary>
        /// Ends creatures hunting process and resets their target.
        /// </summary>
        public void EndCreaturesHuntingProcess()
        {
            RemoveEventHandlerFromTimer(new EventHandler(CreaturesHuntingProcess));
            for (int i = huntingCreatures.Count - 1; i >= 0; i--)
            {
                huntingCreatures[i].ForgetTarget();
                goingBackHomeCreatures.Add(huntingCreatures[i]);
            }
            huntingCreatures.Clear();
        }
        #endregion

        #region back home
        /// <summary>
        /// Starts the creatures back home process.
        /// </summary>
        internal void StartBackHome()
        {
            goingBackHomeCreatures = new List<Creature>();
            
            AddEventHandlerToTimer(new EventHandler(CreaturesBackHomeProcess));
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
        }

        /// <summary>
        /// Ends creatures back home process.
        /// </summary>
        internal void EndCreaturesBackHomeProcess()
        {
            RemoveEventHandlerFromTimer(new EventHandler(CreaturesBackHomeProcess));

            RemoveDeadCreatures();
            RemoveRottenFood();

            for (int i = 0; i < creatures.Count; i++)
            {
                creatures[i].ForgetTarget();
                creatures[i].Sleep();
            }
            goingBackHomeCreatures.Clear();
        }
        #endregion
    }
}
