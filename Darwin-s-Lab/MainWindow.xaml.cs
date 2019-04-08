using Darwin_s_Lab.Simulation;
using System;
using System.Windows;

namespace Darwin_s_Lab
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Creature jack = new Creature()
                            .WithEnergy(2, null)
                            .WithSpeed(2, null)
                            .WithDetectionRange(2, null)
                            .WithForce(2, null)
                            .WithColorH(2, null)
                            .WithColorS(2, null)
                            .WithColorV(2, null);
            Creature bobby = new Creature()
                            .WithEnergy(8, null)
                            .WithSpeed(8, null)
                            .WithDetectionRange(8, null)
                            .WithForce(8, null)
                            .WithColorH(8, null)
                            .WithColorS(8, null)
                            .WithColorV(8, null);
            Console.WriteLine("\nJack:\n" + jack);
            Console.WriteLine("\nBobby:\n" + bobby);

            jack.Mutate();
            bobby.Mutate();

            Console.WriteLine();
            Console.WriteLine("\nMutations !");
            Console.WriteLine("\nJack:\n" + jack);
            Console.WriteLine("\nBobby:\n" + bobby);

            Creature newborn = jack.Cross(bobby);
            
            Console.WriteLine();
            Console.WriteLine("\nCrossover !");
            Console.WriteLine("\nNewborn:\n" + newborn);
        }
    }
}
