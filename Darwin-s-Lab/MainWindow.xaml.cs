using Darwin_s_Lab.Simulation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Darwin_s_Lab
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager manager;
        ColorAnimation animation;

        int dayTime;
        int nightTime;
        bool isDay;

        int elapsed;

        public MainWindow()
        {
            InitializeComponent();

            manager = new Manager(canvas, this);
            dayTime = 500 + 15000;
            nightTime = 10000 + 7500;
            isDay = true;
            elapsed = 0;

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
            float position = -60;

            if (manager.State.GetType() != typeof(StateInitial))
            {
                float blabla = (float)(elapsed + manager.GetStateElapsedTime()) / (isDay ? (float)dayTime : (float)nightTime); 
                position = (blabla * ((float)ActualWidth+60)) - 60;
            }
            sunmoon.Margin = new Thickness(position, 0, 0, 0);
        }

        public void SimulationStateChanged()
        {
            
            if (manager.State.GetType() != typeof(StateInitial))
            {
                if (manager.State.GetNextState().GetType() == typeof(StateGrowFood))
                {
                    elapsed = 0;
                    isDay = true;
                    sunmoon.Source = new BitmapImage(new Uri("pack://application:,,,/Icons/sun.png"));
                }
                else if(manager.State.GetNextState().GetType() == typeof(StateBackHome))
                { 
                    elapsed = 0;
                    isDay = false;
                    sunmoon.Source = new BitmapImage(new Uri("pack://application:,,,/Darwin-s-Lab;component/Icons/moon.png"));
                }
                else
                {
                    elapsed += manager.State.Duration;
                }                
                animation = new ColorAnimation();
                animation.From = manager.State.FilterColor.Color;
                animation.To = manager.State.GetNextState().FilterColor.Color;
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
                filter.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
            }
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
