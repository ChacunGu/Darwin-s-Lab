using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents a simulation's creature.
    /// </summary>
    class Creature : Drawable
    {
        public static double MinimalDistanceToSearchMate = Map.MapSize / 8;
        static double MinimalDistanceToJoinMate = 30;
        static double MinimalDistanceToMate = 5;
        static double MinimalEnergyToMate = 0.5;
        static double MutationProbability = 0.5;
        static double CrossoverKeepAverageProbability = 0.75;
        static double CrossoverKeepOtherProbability = 0.5;
        static Dictionary<string, uint[]> DefaultGenesValues = new Dictionary<string, uint[]>
        {
            {"energy", new uint[]{1, 255}},
            {"speed", new uint[]{1, 255}},
            {"detectionRange", new uint[]{1, 255}},
            {"force", new uint[]{1, 255}},
            {"colorH", new uint[]{1, 255}},
            {"colorS", new uint[]{1, 255}},
            {"colorV", new uint[]{1, 255}}
        };

        public System.Windows.Vector Direction { get; set; }
        public Dictionary<String, Gene> Genes { get; set; }
        public Creature Mate { get; set; }

        #region Constructor
        public Creature()
        {
            Position = new Point(0, 0);
            this.Genes = new Dictionary<string, Gene>();
            this.AddGene("energy", Creature.DefaultGenesValues["energy"][0], Creature.DefaultGenesValues["energy"][1]);
            this.AddGene("speed", Creature.DefaultGenesValues["speed"][0], Creature.DefaultGenesValues["speed"][1]);
            this.AddGene("detectionRange", Creature.DefaultGenesValues["detectionRange"][0], Creature.DefaultGenesValues["detectionRange"][1]);
            this.AddGene("force", Creature.DefaultGenesValues["force"][0], Creature.DefaultGenesValues["force"][1]);
            this.AddGene("colorH", Creature.DefaultGenesValues["colorH"][0], Creature.DefaultGenesValues["colorH"][1]);
            this.AddGene("colorS", Creature.DefaultGenesValues["colorS"][0], Creature.DefaultGenesValues["colorS"][1]);
            this.AddGene("colorV", Creature.DefaultGenesValues["colorV"][0], Creature.DefaultGenesValues["colorV"][1]);
        }

        /// <summary>
        /// Sets creature's energy and returns the creature object.
        /// </summary>
        /// <param name="energy">creature's energy</param>
        /// <param name="mask">energy mask (^2 - 1)</param>
        /// <returns>creature with energy's gene set</returns>
        public Creature WithEnergy(uint energy, uint? mask)
        {

            this.AddGene("energy", energy, mask==null ? DefaultGenesValues["energy"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's speed and returns the creature object.
        /// </summary>
        /// <param name="speed">creature's speed</param>
        /// <param name="mask">speed mask (^2 - 1)</param>
        /// <returns>creature with speed's gene set</returns>
        public Creature WithSpeed(uint speed, uint? mask)
        {
            this.AddGene("speed", speed, mask == null ? DefaultGenesValues["speed"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's detection range and returns the creature object.
        /// </summary>
        /// <param name="detectionRange">creature's detection range</param>
        /// <param name="mask">detection range mask (^2 - 1)</param>
        /// <returns>creature with detection range's gene set</returns>
        public Creature WithDetectionRange(uint detectionRange, uint? mask)
        {
            this.AddGene("detectionRange", detectionRange, mask == null ? DefaultGenesValues["detectionRange"][1] : (uint)mask);
            return this;
        }
        
        /// <summary>
        /// Sets creature's force and returns the creature object.
        /// </summary>
        /// <param name="force">creature's force</param>
        /// <param name="mask">force mask (^2 - 1)</param>
        /// <returns>creature with force's gene set</returns>
        public Creature WithForce(uint force, uint? mask)
        {
            this.AddGene("force", force, mask == null ? DefaultGenesValues["force"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's color H and returns the creature object.
        /// </summary>
        /// <param name="colorH">creature's color H</param>
        /// <param name="mask">color H mask (^2 - 1)</param>
        /// <returns>creature with color H gene set</returns>
        public Creature WithColorH(uint colorH, uint? mask)
        {
            this.AddGene("colorH", colorH, mask == null ? DefaultGenesValues["colorH"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's color S and returns the creature object.
        /// </summary>
        /// <param name="colorS">creature's color S</param>
        /// <param name="mask">color S mask (^2 - 1)</param>
        /// <returns>creature with color S gene set</returns>
        public Creature WithColorS(uint colorS, uint? mask)
        {
            this.AddGene("colorS", colorS, mask == null ? DefaultGenesValues["colorS"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's color V and returns the creature object.
        /// </summary>
        /// <param name="colorV">creature's color V</param>
        /// <param name="mask">color V mask (^2 - 1)</param>
        /// <returns>creature with color V gene set</returns>
        public Creature WithColorV(uint colorV, uint? mask)
        {
            this.AddGene("colorV", colorV, mask == null ? DefaultGenesValues["colorV"][1] : (uint)mask);
            return this;
        }

        /// <summary>
        /// Sets creature's position and returns creature object.
        /// </summary>
        /// <param name="position">creature's position</param>
        /// <returns>creature with position set</returns>
        public Creature WithPosition(Point position)
        {
            this.Position = position;
            return this;
        }
        #endregion

        /// <summary>
        /// Adds a new gene to the creature or modifies current one if it already exists.
        /// </summary>
        /// <param name="name">gene's name</param>
        /// <param name="value">gene's value</param>
        /// <param name="mask">gene's mask</param>
        private void AddGene(String name, uint value, uint mask)
        {
            if (this.Genes.ContainsKey(name))
                this.Genes[name] = new Gene(name, value, mask);
            else
                this.Genes.Add(name, new Gene(name, value, mask));
        }

        /// <summary>
        /// Takes a step in a direction.
        /// </summary>
        /// <param name="dt">time in milliseconds</param>
        public void TakeStep(long dt)
        {
            Position += Direction * Genes["speed"].Value * (dt / Manager.FramesPerSec);
        }

        /// <summary>
        /// Completes the whole mating process. Moves towards the mate and if in range reproduces and returns the newborn.
        /// </summary>
        /// <param name="dt">time elapsed since last call in milliseconds</param>
        /// <param name="map">simulation's map</param>
        /// <returns>newborn creature or null if the creatures didn't found each other</returns>
        public Creature MatingProcess(long dt, Map map)
        {
            // search best direction to move towards the creature's mate
            FindDirectionTowardsMate(map);

            // move
            TakeStep(dt);

            // mate if in range
            if (Map.DistanceBetweenTwoPointsOpti(Position, Mate.Position) <= MinimalDistanceToMate)
            {
                Mate.Mate = null;
                Mate = null;
                return Cross(Mate);
            }
            return null;
        }

        /// <summary>
        /// Finds the direction towards the creature's mate.
        /// </summary>
        /// <param name="map">simulation's map</param>
        public void FindDirectionTowardsMate(Map map)
        {
            if (Map.DistanceBetweenTwoPointsOpti(Position, Mate.Position) > MinimalDistanceToJoinMate*MinimalDistanceToJoinMate)
            {
                // find safe zone circle's tangent
                Vector myPosToCenter = map.Position - Position;
                Vector tangentToSafeZoneCircle = new Vector(myPosToCenter.Y, -myPosToCenter.X);

                // negate if shorter path is counter clockwise
                // formula: https://stackoverflow.com/a/25927775
                if (((map.Position.X - Position.X) * (Mate.Position.Y - map.Position.Y)) - 
                    ((map.Position.Y - Position.Y) * (Mate.Position.X - map.Position.X)) >= 0.0)
                {
                    tangentToSafeZoneCircle.Negate();
                    tangentToSafeZoneCircle.Normalize();
                    Direction = tangentToSafeZoneCircle;
                }
            } else
            {
                // move directly towards mate
                Direction = Mate.Position - Position;
                Direction.Normalize();
            }
        }

        /// <summary>
        /// Fights this creature with other.
        /// </summary>
        /// <param name="other">creature to fight with</param>
        public void Fight(Creature other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Eats given food.
        /// </summary>
        /// <param name="food">food to eat</param>
        public void Eat(Food food)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutates creature's genes.
        /// </summary>
        public void Mutate()
        {
            for (int i=0; i<Genes.Count(); i++) // for each gene
            {
                if (Tools.rdm.NextDouble() < Gene.MutationProbability) // if gene has to mutate
                {
                    Genes.ElementAt(i).Value.Mutate();
                }
            }
        }

        /// <summary>
        /// Reproduces this creature with other. Creates a newborn creature with genes from its parents.
        /// </summary>
        /// <param name="other">significant other</param>
        public Creature Cross(Creature other)
        {
            Creature newborn = new Creature();
            for (int i = 0; i < Genes.Count(); i++) // for each gene
            {
                Gene selfGene = Genes.ElementAt(i).Value;
                Gene otherGene = other.Genes.ElementAt(i).Value;

                String name = selfGene.Name;
                uint mask = selfGene.Mask;
                uint value = 0;

                if (Tools.rdm.NextDouble() < CrossoverKeepAverageProbability) // average between self and other
                {
                    uint avg = selfGene.Value / 2 + otherGene.Value / 2;
                    double rdmOffset = Tools.rdm.NextDouble() - 0.5; // random between -0.5 and 0.5
                    rdmOffset *= Math.Floor((double) Math.Abs((int)selfGene.Value - (int)otherGene.Value) / 2); // multiply random by difference to get the offset from avg's value
                    value = avg + (uint) Math.Floor(rdmOffset); // apply offset to average
                } else
                {
                    if (Tools.rdm.NextDouble() < CrossoverKeepOtherProbability) // keep other
                    {
                        value = otherGene.Value;
                    } else // keep self
                    {
                        value = selfGene.Value;
                    }
                }

                newborn.AddGene(name, value, mask);
            }
            return newborn;
        }

        /// <summary>
        /// Returns true if the creature is still alive false otherwise.
        /// </summary>
        /// <returns>creature's state alive (true) or dead (false)</returns>
        public bool IsAlive()
        {
            return GetEnergy() > 0;
        }

        /// <summary>
        /// Kills creature.
        /// </summary>
        public void Kill()
        {
            // TODO
        }

        /// <summary>
        /// Returns true if the creature can mutate false otherwise.
        /// </summary>
        /// <returns>true if the creature can mutate false otherwise</returns>
        public bool CanMutate()
        {
            return Tools.rdm.NextDouble() < MutationProbability;
        }

        /// <summary>
        /// Returns true if the creature has enough energy to mate false otherwise.
        /// </summary>
        /// <returns>true if the creature can mate false otherwise</returns>
        public bool CanMate()
        {
            return GetEnergy() >= MinimalEnergyToMate;
        }
        
        /// <summary>
        /// Sets each other's mate.
        /// </summary>
        /// <param name="mate">creature's new mate</param>
        public void SetMate(Creature mate)
        {
            this.Mate = mate;
            mate.Mate = this;
        }

        /// <summary>
        /// Returns the creature's speed.
        /// </summary>
        /// <returns>creature's speed</returns>
        public int GetSpeed()
        {
            return (int)Genes["speed"].Value;
        }

        /// <summary>
        /// Returns the creature's energy.
        /// </summary>
        /// <returns>creature's energy</returns>
        public int GetEnergy()
        {
            return (int)Genes["energy"].Value;
        }

        /// <summary>
        /// Updates element's graphical state before drawing.
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns creature's representation as a string.
        /// </summary>
        /// <returns>creature's representation</returns>
        public override string ToString()
        {
            return this.Genes["energy"].ToString()          + "\n" +
                    this.Genes["speed"].ToString()          + "\n" +
                    this.Genes["detectionRange"].ToString() + "\n" +
                    this.Genes["force"].ToString()          + "\n" +
                    this.Genes["colorH"].ToString()         + "\n" +
                    this.Genes["colorS"].ToString()         + "\n" +
                    this.Genes["colorV"].ToString();
        }
    }
}
