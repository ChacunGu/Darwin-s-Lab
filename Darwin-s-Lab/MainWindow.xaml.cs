using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Darwin_s_Lab
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RenderTargetBitmap buffer;
        private DrawingVisual drawingVisual = new DrawingVisual();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private Point pos = new Point(500,500);
        private int dt = 17; //milliseconds
        private int time = 0;
        private Random rand = new Random();

        private SolidColorBrush colorCircle1 = new SolidColorBrush(Color.FromRgb(0, 128, 64));
        private SolidColorBrush colorCircle2 = new SolidColorBrush(Color.FromRgb(64, 128, 0));
        private SolidColorBrush colorCreature = new SolidColorBrush(Color.FromRgb(0, 0, 128));
        private Pen pen = new Pen();
        private Point center = new Point(500, 500);

        public MainWindow()
        {
            InitializeComponent();
            
            buffer = new RenderTargetBitmap((int)background.Width, (int)background.Height, 96, 96, PixelFormats.Pbgra32);
            background.Source = buffer;
            dispatcherTimer.Tick += new EventHandler(draw);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, dt);
            dispatcherTimer.Start();

        }

        private void draw(object sender, EventArgs e)
        {
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                buffer.Clear();
                drawingContext.DrawEllipse(
                    colorCircle1,
                    pen,
                    center,
                    450,
                    450
                );
                drawingContext.DrawEllipse(
                    colorCircle2,
                    pen,
                    center,
                    350,
                    350
                );
                drawingContext.DrawEllipse(
                    colorCreature,
                    pen,
                    pos,
                    30,
                    30
                );
                
            }

            buffer.Render(drawingVisual);
            pos.X = 400 * Math.Cos(2*Math.PI*time/360.0) + 500; 
            pos.Y = 400 * Math.Sin(2 * Math.PI * time / 360.0) + 500;
            time = (time + 1) % 360;
        }
        
    }

}
