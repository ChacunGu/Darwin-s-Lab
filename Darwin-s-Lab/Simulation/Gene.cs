using System;

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
        public uint NumberOnesBitInMask { get; set; }

        public Gene(String name, uint value, uint mask)
        {
            this.Name = name;
            this.Value = value;
            this.Mask = mask;
            this.NumberOnesBitInMask = CountNumberOnesBit(this.Mask);
        }

        /// <summary>
        /// Mutates gene.
        /// </summary>
        public void Mutate()
        {
            int numberOnesBitInGene = (int)CountNumberOnesBit(Value);
            int indexBitOneToFlip = Tools.rdm.Next(0, numberOnesBitInGene);
            int indexBitZeroToFlip = Tools.rdm.Next(0, (int)NumberOnesBitInMask - numberOnesBitInGene);

            bool bitOneFlipped = false;
            bool bitZeroFlipped = false;

            int counterOnes = 0;
            int counterZeroes = 0;
            int counterTotal = 0;

            // go through gene to find the bits to flip
            while (!bitOneFlipped || !bitZeroFlipped)
            {
                bool isOne = (Value & (1 << counterTotal)) != 0;
                if (isOne)
                {
                    if (counterOnes == indexBitOneToFlip) // this bit should be flipped
                    {
                        Value ^= (uint)(1 << counterTotal);
                        bitOneFlipped = true;
                    }
                    counterOnes++;
                }
                else
                {
                    if (counterZeroes == indexBitZeroToFlip) // this bit should be flipped
                    {
                        Value ^= (uint)(1 << counterTotal);
                        bitZeroFlipped = true;
                    }
                    counterZeroes++;
                }
                counterTotal++;
            }
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
