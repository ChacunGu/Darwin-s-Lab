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
                            .WithEnergy(6, 15)
                            .WithSpeed(6, 15)
                            .WithDetectionRange(6, 15)
                            .WithForce(6, 15)
                            .WithColorH(6, 15)
                            .WithColorS(6, 15)
                            .WithColorV(6, 15);
            Creature bobby = new Creature();
            Console.WriteLine("Jack:\n" + jack);
            //Console.WriteLine("Bobby:\n" + bobby);

            jack.Mutate();
            //bobby.Mutate();

            Console.WriteLine("\nMutations !");
            Console.WriteLine("Jack:\n" + jack);
            //Console.WriteLine("Bobby:\n" + bobby);
        }
    }
}
