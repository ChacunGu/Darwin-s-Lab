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

            Manager manager = new Manager(canvas);

            manager.StartSimulation();
        }
    }
}
