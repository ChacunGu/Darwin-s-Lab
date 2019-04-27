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

            manager = new Manager(canvas);
            manager.StartSimulation();
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            creature_infos.Init();
        }
        
        private void BtnStartPause_Click(object sender, RoutedEventArgs e)
        {
            if (btnStartPause.Content.Equals("Start"))
            {
                btnStartPause.Content = "Pause";
                sldNbCreature.IsEnabled = false;
                sldNbFood.IsEnabled = false;
                btnStopReset.IsEnabled = true;
                manager.Resume();
            }
            else
            {
                btnStartPause.Content = "Start";
                manager.Pause();
            }
        }

        private void BtnStopReset_Click(object sender, RoutedEventArgs e)
        {
            if (btnStopReset.Content.Equals("Stop"))
            {
                btnStopReset.Content = "Reset";
                btnStartPause.IsEnabled = false;

            }
            else
            {
                btnStopReset.Content = "Stop";
                btnStartPause.Content = "Start";
                sldNbCreature.IsEnabled = true;
                sldNbFood.IsEnabled = true;
                btnStopReset.IsEnabled = false;
                btnStartPause.IsEnabled = true;
                manager = new Manager(canvas);
            }
        }
    }
}
