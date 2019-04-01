﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Represents a creature's gene.
    /// </summary>
    class Gene
    {
        public static double MutationProbability = 1.0;

        public String Name { get; set; }
        public uint Value { get; set; }
        public uint Mask { get; set; }
        public uint NumberOnesBitInMask { get; set; }

        public Gene(String name, uint value, uint mask)
        {
            this.Name = name;
            this.Value = value;
            this.Mask = mask;
            this.NumberOnesBitInMask = CountNumberOnesBit(this.Mask);
        }

        /// <summary>
        /// Returns gene's representation as a string.
        /// </summary>
        /// <returns>gene's representation</returns>
        public override string ToString()
        {
            return Name + ": " + Value.ToString();
        }

        /// <summary>
        /// Counts number of 1 bits in given uint.
        /// Code from: https://www.geeksforgeeks.org/csharp-program-for-count-set-bits-in-an-integer/
        /// </summary>
        /// <param name="n">uint to count 1 bits in</param>
        /// <returns>number of 1 bits in given uint</returns>
        public static uint CountNumberOnesBit(uint n)
        {
            // base case 
            if (n == 0)
                return 0;
            else
                // if last bit set 
                // add 1 else add 0 
                return (n & 1) + CountNumberOnesBit(n >> 1);
        }
    }
}
