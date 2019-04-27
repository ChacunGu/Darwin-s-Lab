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
        Manager manager;
        public MainWindow()
        {
            InitializeComponent();

            manager = new Manager(canvas, this);

            manager.StartSimulation();
        }

        /// <summary>
        /// For debug, TODO remove!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (manager.IsPaused)
            {
                manager.Resume();
            }
            else
            {
                manager.Pause();
            }
        }
    }
}
