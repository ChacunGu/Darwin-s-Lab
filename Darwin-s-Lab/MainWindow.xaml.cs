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
                            .WithEnergy(10)
                            .WithSpeed(10)
                            .WithDetectionRange(10)
                            .WithForce(10)
                            .WithColorH(10)
                            .WithColorS(10)
                            .WithColorV(10);
            Creature bobby = new Creature();
            Console.WriteLine("Jack:\n" + jack);
            Console.WriteLine("Bobby:\n" + bobby);

            creature_infos.SetCreature(jack);
        }
    }
}
