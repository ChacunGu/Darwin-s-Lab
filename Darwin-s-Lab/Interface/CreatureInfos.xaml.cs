using Darwin_s_Lab.Simulation;
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
using System.Windows.Threading;

namespace Darwin_s_Lab.Interface
{
    /// <summary>
    /// Logique d'interaction pour CreatureInfos.xaml
    /// </summary>
    public partial class CreatureInfos : UserControl
    {

        DispatcherTimer timer;

        public CreatureInfos()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;

            timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 100)
            };
            timer.Tick += UpdateInfos;
            timer.Start();
        }

        private void UpdateInfos(object sender, EventArgs e)
        {
            if (Manager.SelectedCreature != null)
            {
                progress_energy.Value = Manager.SelectedCreature.Genes["energy"].Value;
                energy_value.Content = Manager.SelectedCreature.Genes["energy"].Value;
                progress_speed.Value = Manager.SelectedCreature.Genes["speed"].Value;
                progress_detection.Value = Manager.SelectedCreature.Genes["detectionRange"].Value;
                progress_strength.Value = Manager.SelectedCreature.Genes["force"].Value;
            }
        }

        public void Init()
        {
            if (Manager.SelectedCreature != null)
            {
                progress_energy.Minimum = 0;
                progress_energy.Maximum = Manager.SelectedCreature.Genes["energy"].Mask;

                progress_speed.Minimum = 0;
                progress_speed.Maximum = Manager.SelectedCreature.Genes["speed"].Mask;

                progress_detection.Minimum = 0;
                progress_detection.Maximum = Manager.SelectedCreature.Genes["detectionRange"].Mask;

                progress_strength.Minimum = 0;
                progress_strength.Maximum = Manager.SelectedCreature.Genes["force"].Mask;

                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom((Manager.SelectedCreature.GetHexColor())));
                preview.Fill = colorBrush;
                
                this.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Hidden;
            }
        }
        
    }
}