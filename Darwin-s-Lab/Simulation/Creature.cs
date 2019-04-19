using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents a simulation's creature.
    /// </summary>
    public class Creature : IDrawable
    {
        public static uint DefaultEnergy = 1;
        public static uint DefaultSpeed = 1;
        public static uint DefaultDetectionRange = 1;
        public static uint DefaultForce = 1;
        public static uint DefaultColorH = 1;
        public static uint DefaultColorS = 1;
        public static uint DefaultColorV = 1;

        public System.Windows.Vector Direction { get; set; }
        public Dictionary<String, Gene> Genes { get; set; }

        #region Constructor
        public Creature()
        {
            this.Genes = new Dictionary<string, Gene>();
            this.AddGene("energy", Creature.DefaultEnergy, uint.MaxValue);
            this.AddGene("speed", Creature.DefaultSpeed, uint.MaxValue);
            this.AddGene("detectionRange", Creature.DefaultDetectionRange, uint.MaxValue);
            this.AddGene("force", Creature.DefaultForce, uint.MaxValue);
            this.AddGene("colorH", Creature.DefaultColorH, uint.MaxValue);
            this.AddGene("colorS", Creature.DefaultColorS, uint.MaxValue);
            this.AddGene("colorV", Creature.DefaultColorV, uint.MaxValue);
        }

        /// <summary>
        /// Sets creature's energy and returns the creature object.
        /// </summary>
        /// <param name="energy">creature's energy</param>
        /// <returns>creature with energy's gene set</returns>
        public Creature WithEnergy(uint energy)
        {
            this.AddGene("energy", energy, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's speed and returns the creature object.
        /// </summary>
        /// <param name="speed">creature's speed</param>
        /// <returns>creature with speed's gene set</returns>
        public Creature WithSpeed(uint speed)
        {
            this.AddGene("speed", speed, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's detection range and returns the creature object.
        /// </summary>
        /// <param name="detectionRange">creature's detection range</param>
        /// <returns>creature with detection range's gene set</returns>
        public Creature WithDetectionRange(uint detectionRange)
        {
            this.AddGene("detectionRange", detectionRange, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's force and returns the creature object.
        /// </summary>
        /// <param name="force">creature's force</param>
        /// <returns>creature with force's gene set</returns>
        public Creature WithForce(uint force)
        {
            this.AddGene("force", force, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's color H and returns the creature object.
        /// </summary>
        /// <param name="colorH">creature's color H</param>
        /// <returns>creature with color H gene set</returns>
        public Creature WithColorH(uint colorH)
        {
            this.AddGene("colorH", colorH, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's color S and returns the creature object.
        /// </summary>
        /// <param name="colorS">creature's color S</param>
        /// <returns>creature with color S gene set</returns>
        public Creature WithColorS(uint colorS)
        {
            this.AddGene("colorS", colorS, uint.MaxValue);
            return this;
        }

        /// <summary>
        /// Sets creature's color V and returns the creature object.
        /// </summary>
        /// <param name="colorV">creature's color V</param>
        /// <returns>creature with color V gene set</returns>
        public Creature WithColorV(uint colorV)
        {
            this.AddGene("colorV", colorV, uint.MaxValue);
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reproduces this creature with other. Creates a newborn creature with genes from its parents.
        /// </summary>
        /// <param name="other">significant other</param>
        public void Cross(Creature other)
        {
            throw new NotImplementedException();
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
