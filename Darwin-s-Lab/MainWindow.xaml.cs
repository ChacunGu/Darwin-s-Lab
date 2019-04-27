using Darwin_s_Lab.Simulation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Darwin_s_Lab
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager manager;
        ColorAnimation animation;

        public MainWindow()
        {
            InitializeComponent();

            manager = new Manager(canvas, this);

            sldNbCreature.Value = manager.CreatureNumber;
            sldNbFood.Value = manager.FoodNumber;
        }

        private void Canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UpdateCreatureInfo();
        }
        
        /// <summary>
        /// Update the values in the Creatures info
        /// </summary>
        public void UpdateCreatureInfo()
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
                if (manager.IsSimulating)
                {
                    manager.Resume();
                }
                else
                {
                    manager.StartSimulation();
                }
            }
            else
            {
                btnStartPause.Content = "Start";
                manager.Pause();
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStartPause.Content = "Start";
            sldNbCreature.IsEnabled = true;
            sldNbFood.IsEnabled = true;
            btnStopReset.IsEnabled = false;
            btnStartPause.IsEnabled = true;
            manager.Reset();
            
        }

        /// <summary>
        /// Update the interface
        /// </summary>
        /// <param name="sender">event's sender</param>
        /// <param name="e">event's arguments</param>
        public void Update(object sender, EventArgs e)
        {

            
        }

        public void SimulationStateChanged()
        {
            
            animation = new ColorAnimation();
            animation.From = manager.State.FilterColor.Color;
            animation.To = manager.State.GetNextState().FilterColor.Color;
            animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            filter.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);

        }

        private void SldNbCreature_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double value = slider.Value;
            if (manager != null) {
                manager.CreatureNumber = (int)value;
            }
        }

        private void SldNbFood_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double value = slider.Value;
            if (manager != null)
            {
                manager.FoodNumber = (int)value;
            }
        }
    }
}
