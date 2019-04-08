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
                            .WithEnergy(128, null)
                            .WithSpeed(128, null)
                            .WithDetectionRange(128, null)
                            .WithForce(128, null)
                            .WithColorH(128, null)
                            .WithColorS(128, null)
                            .WithColorV(128, null);
            Creature bobby = new Creature()
                            .WithEnergy(1, null)
                            .WithSpeed(1, null)
                            .WithDetectionRange(1, null)
                            .WithForce(1, null)
                            .WithColorH(1, null)
                            .WithColorS(1, null)
                            .WithColorV(1, null);
            Console.WriteLine("Jack:\n" + jack);
            Console.WriteLine("Bobby:\n" + bobby);

            //jack.Mutate();
            //bobby.Mutate();

            Creature newborn = jack.Cross(bobby);

            //Console.WriteLine("\nMutations !");
            //Console.WriteLine("Jack:\n" + jack);
            //Console.WriteLine("Bobby:\n" + bobby);

            Console.WriteLine("\nCrossover !");
            Console.WriteLine("Newborn:\n" + newborn);
        }
    }
}
