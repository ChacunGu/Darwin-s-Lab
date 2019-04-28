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
        static Point sunmoonDefaultDimension = new Point(360, 360);
        static double sunmoonStartingAngle = 3 * Math.PI / 2; // 5 * Math.PI / 4;
        static double sunmoonEndingAngle = 3 * Math.PI / 2; // 5 * Math.PI / 3
        static double sunmoonMargin = 300;
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
            dayTime = 500 + 15000 + 10000;
            nightTime = 7500;
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
                slidersgrid.Visibility = Visibility.Collapsed;
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
            slidersgrid.Visibility = Visibility.Visible;
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
            counters.Content = manager.GetNumberOfCreatures() + " creatures\n" + manager.GetNumberOfFoods() + " foods";

            if (manager.State.GetType() != typeof(StateInitial))
            {
                float stateProgression = (float)(elapsed + manager.GetStateElapsedTime()) / (isDay ? (float)dayTime : (float)nightTime);
                double angle = Tools.Map(stateProgression, 0, 1, sunmoonStartingAngle, sunmoonEndingAngle - 2 * Math.PI);
                
                // canvas scale
                ContainerVisual child = VisualTreeHelper.GetChild(viewbox, 0) as ContainerVisual;
                ScaleTransform scale = child.Transform as ScaleTransform;

                // compute sun/moon width
                sunmoon.Width = sunmoonDefaultDimension.X * scale.ScaleX;
                sunmoon.Height = sunmoonDefaultDimension.Y * scale.ScaleY;

                // canvas dynamic position
                double canvasRadius = (scale.ScaleX * canvas.Width) / 2;
                Point canvasTopLeftPosition = canvas.TransformToAncestor(this).Transform(new Point(0, 0));
                Point canvasCenterPosition = new Point(canvasTopLeftPosition.X + canvasRadius,
                                                       canvasTopLeftPosition.Y + canvasRadius);
                
                double distanceFromCanvasCenter = sunmoonMargin * scale.ScaleX + canvasRadius;

                Point position = new Point(distanceFromCanvasCenter * Math.Cos(angle), distanceFromCanvasCenter * Math.Sin(angle));
                position = new Point(position.X + canvasCenterPosition.X - sunmoon.Width / 2,
                                     -position.Y + canvasCenterPosition.Y - sunmoon.Width / 2);
                sunmoon.Margin = new Thickness(position.X, position.Y, 0, 0);
            }
        }

        public void SimulationStateChanged()
        {
            
            if (manager.State.GetType() != typeof(StateInitial))
            {
                if (manager.State.GetNextState().GetType() == typeof(StateGrowFood))
                {
                    elapsed = 0;
                    isDay = true;
                    sunmoonDefaultDimension = new Point(420, 420);

                    // image source: https://www.goodfreephotos.com/vector-images/cartoon-sun-vector-art.png.php
                    sunmoon.Source = new BitmapImage(new Uri("pack://application:,,,/Darwin-s-Lab;component/Images/sun.png"));
                }
                else if(manager.State.GetNextState().GetType() == typeof(StateReproduce))
                { 
                    elapsed = 0;
                    isDay = false;
                    sunmoonDefaultDimension = new Point(320, 320);

                    // image source: http://www.publicdomainfiles.com/show_file.php?id=13939368015951
                    sunmoon.Source = new BitmapImage(new Uri("pack://application:,,,/Darwin-s-Lab;component/Images/moon.png"));
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
