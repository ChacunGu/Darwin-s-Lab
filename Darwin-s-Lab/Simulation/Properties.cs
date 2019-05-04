using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Darwin_s_Lab.Simulation
{
    /// <summary>
    /// Properties class to store all constantes of the simulation
    /// </summary>
    public class Properties
    {
        public static double EnergyGivenByFood = 0.2;

        public static int SpeedFactor = 10;
        public static Vector CreatureDim = new Vector(50, 50);
        public static double MinimalDistanceToSearchMate = Math.Pow(CreatureDim.X * 12, 2); // to the power of 2 as it is only used with optimized distance computation (no sqrt)
        public static double MinimalDistanceToJoinMate = Math.Pow(CreatureDim.X * 2, 2); // to the power of 2 as it is only used with optimized distance computation (no sqrt)
        public static double MinimalDistanceToMate = Math.Pow(CreatureDim.X / 2, 2); // to the power of 2 as it is only used with optimized distance computation (no sqrt)
        public static double MinimalDistanceToReachTarget = Math.Pow(CreatureDim.X / 2, 2); // to the power of 2 as it is only used with optimized distance computation (no sqrt)
        public static double MinimalOpacity = 0.2;

        public static double SleepEnergyGain = 1.0;
        public static double UsedEnergyToMove = 0.000001;
        public static double MinimalEnergyToMove = 0.000001;
        public static double MinimalEnergyToMate = 0.2;
        public static double CreatureMutationProbability = 0.3;
        public static double CrossoverKeepAverageProbability = 0.75;
        public static double CrossoverKeepOtherProbability = 0.5;

        public static double GeneMutationProbability = 0.35;

        public static Dictionary<string, uint[]> DefaultGenesValues = new Dictionary<string, uint[]>
        {
            {"energy", new uint[]{1, 511}},
            {"speed", new uint[]{1, 255}},
            {"detectionRange", new uint[]{1, 255}},
            {"force", new uint[]{1, 255}},
            {"colorH", new uint[]{1, 1023}},
            {"colorS", new uint[]{1, 1023}},
            {"colorV", new uint[]{1, 1023}}
        };

        public static int MapSize = 1000;

        public static int MaximumNumberCreatures = 500;
    }
}
