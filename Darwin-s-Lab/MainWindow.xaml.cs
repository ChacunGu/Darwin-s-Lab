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

            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                RenderTargetBitmap buffer;
                DrawingVisual drawingVisual = new DrawingVisual();
                buffer = new RenderTargetBitmap((int)background.Width, (int)background.Height, 96, 96, PixelFormats.Pbgra32);
                background.Source = buffer;
                while (true)
                {
                    using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                    {
                        Random rand = new Random();
                        for (int i = 0; i < 100; i++)
                        {
                            drawingContext.DrawEllipse(
                                    new SolidColorBrush(Color.FromRgb(
                                        (byte)rand.Next(0, 255),
                                        (byte)rand.Next(0, 255),
                                        (byte)rand.Next(0, 255))),
                                    new Pen(),
                                    new Point(rand.Next(10, 990), rand.Next(10, 990)),
                                    30,
                                    30
                                );
                        }
                    }

                    buffer.Render(drawingVisual);
                    Thread.Sleep(17);
                };
            }
            ));
        }
    }
}
