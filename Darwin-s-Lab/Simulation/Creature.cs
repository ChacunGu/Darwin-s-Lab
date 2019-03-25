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
    class Creature : IDrawable
    {
        public System.Windows.Vector Direction { get; set; }
        public Dictionary<String, Gene> Genes { get; set; }

        public Creature(uint energy, uint speed, uint detectionRange, uint force, uint colorH, uint colorS, uint colorV)
        {
            this.AddGene("energy", energy, uint.MaxValue);
            this.AddGene("speed", speed, uint.MaxValue);
            this.AddGene("detectionRange", detectionRange, uint.MaxValue);
            this.AddGene("force", force, uint.MaxValue);
            this.AddGene("colorH", colorH, uint.MaxValue);
            this.AddGene("colorS", colorS, uint.MaxValue);
            this.AddGene("colorV", colorV, uint.MaxValue);
        }

        /// <summary>
        /// Adds a new gene to the creature.
        /// </summary>
        /// <param name="name">gene's name</param>
        /// <param name="value">gene's value</param>
        /// <param name="mask">gene's mask</param>
        private void AddGene(String name, uint value, uint mask)
        {
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
    }
}
