using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Darwin_s_Lab
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ellipse> objects;
        Random rand;
        int id;

        public MainWindow()
        {
            InitializeComponent();

            rand = new Random();
            int nb_ellipse = 200;

            objects = new List<Ellipse>();

            id = 0;

            for (int i = 0; i < nb_ellipse; i++)
            {
                Ellipse e = new Ellipse();

                int size = rand.Next(10, 71);

                objects.Add(e);
                canvas.Children.Add(e);

                e.Stroke = System.Windows.Media.Brushes.Blue;
                e.Fill = System.Windows.Media.Brushes.DarkBlue;

                e.Width = size;
                e.Height = size;
            }
            
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object Sender, EventArgs e)
        {
            //Console.WriteLine("---> " + id);
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < objects.Count; i++)
            {                
                Canvas.SetLeft(objects.ElementAt(i), rand.Next(971));
                Canvas.SetTop(objects.ElementAt(i), rand.Next(971));

            }
            id++;

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);


            
        }
    }
}
