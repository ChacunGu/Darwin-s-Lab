using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents a creature's gene.
    /// </summary>
    public class Gene
    {
        public String Name { get; set; }
        public uint Value { get; set; }
        public uint Mask { get; set; }

        public Gene(String name, uint value, uint mask)
        {
            this.Name = name;
            this.Value = value;
            this.Mask = mask;
        }

        /// <summary>
        /// Returns gene's representation as a string.
        /// </summary>
        /// <returns>gene's representation</returns>
        public override string ToString()
        {
            return Name + ": " + Value.ToString();
        }
    }
}
